using System.Linq;
using System.Threading.Tasks;
using BerTlv;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Cards;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.NFC.Enums;
using Org.BouncyCastle.Crypto.Parameters;

namespace DigiD.Common.EID.CardSteps.CAv2
{
    internal class StepGeneralAuthenticate : BaseSecureStep, IStep
    {
        private ECPublicKeyParameters _pk;

        public StepGeneralAuthenticate(ISecureCardOperation operation, ECPublicKeyParameters pk = null) : base(operation)
        {
            _pk = pk;
        }

        public async Task<bool> Execute()
        {
            if (_pk == null)
                _pk = ((eNIK)Operation.GAP.Card).CardAuthenticationPublicKey;

            var command = CommandApduBuilder.GetGeneralAuthenticate(_pk.Q.GetEncoded(), Operation.GAP.SMContext);
            var response = await CommandApduBuilder.SendAPDU("CA General Authenticate", command, Operation.GAP.SMContext);

            if (response.SW != 0x9000)
                return false;

            if (Operation.GAP.Card.DocumentType == DocumentType.DrivingLicense)
            {
                var tlvs = Tlv.ParseTlv(response.Data);
                var data = tlvs.First().Children.ToList();

                var operation = (ChipAuthenticationRdw)Operation;

                operation.Ricc = data[0].Value;
                operation.Ticc = data[1].Value;
            }

            return true;
        }
    }
}
