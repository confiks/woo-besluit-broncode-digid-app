using System.Threading.Tasks;
using DigiD.Common.EID.Enums;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.NFC.Enums;
using Org.BouncyCastle.Asn1;

namespace DigiD.Common.EID.CardSteps.PACE
{
    internal class StepMutualAuthentication : IStep
    {
        private readonly Gap _gap;

        public StepMutualAuthentication(Gap gap)
        {
            _gap = gap;
        }
        public async Task<bool> Execute()
        {
            var command = CommandApduBuilder.GetAuthCommand(_gap.Pace.TerminalToken, _gap.SMContext);
            var response = await CommandApduBuilder.SendAPDU("PACE MutualAuthentication", command, _gap.SMContext);

            if (response.SW1 == 0x63)
            {
                if (_gap.PinTriesLeft == null)
                {
                    switch (_gap.Pace.PasswordType)
                    {
                        case PasswordType.PIN:
                            _gap.PinTriesLeft = 5;
                            break;
                        case PasswordType.PUK:
                            _gap.PinTriesLeft = 10;
                            break;
                    }
                }
                else if (_gap.PinTriesLeft == 1)
                    _gap.IsBlocked = true;

                _gap.AuthenticationResult = AuthenticationResult.Failed;
            }
            else if (response.SW == 0x9000)
                _gap.AuthenticationResult = AuthenticationResult.Success;
            else if (_gap.Card.DocumentType == DocumentType.IDCard && response.SW == 0x6200)
            {
                _gap.ChangePinRequired = true;
                return true;
            }

            return response.SW == 0x9000 && ParseData(response.Data);
        }

        private bool ParseData(byte[] data)
        {
            using (var s = new Asn1InputStream(data))
            {
                var dynamicAuthData = (DerApplicationSpecific)s.ReadObject();
                var authRespSet = Asn1Set.GetInstance(dynamicAuthData.GetObject(0x11));

                foreach (Asn1TaggedObject element in authRespSet)
                {
                    var val = Asn1OctetString.GetInstance(element).GetOctets();

                    switch (element.TagNo)
                    {
                        case 6:
                            _gap.Pace.CardAuthToken = val;
                            break;
                        case 7:
                            _gap.Pace.Car = val;
                            break;
                    }
                }

                return true;
            }
        }
    }
}
