using System.Threading.Tasks;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models.Apdu;
using Org.BouncyCastle.Crypto.Parameters;

namespace DigiD.Common.EID.CardSteps.PACE
{
    /// <summary>
    /// The client sends the MAP nonce command to the PCA.
    /// </summary>
    internal class StepMapNonce : IStep
    {
        private readonly Gap _gap;

        public StepMapNonce(Gap gap)
        {
            _gap = gap;
        }

        public async Task<bool> Execute()
        {
            var xy = SecurityHelper.GetXandYPoints((ECPublicKeyParameters)_gap.Pace.EphemeralKeyPair.Public);

            var command = CommandApduBuilder.GetMapCommand(xy.x, xy.y, _gap.SMContext);
            var response = await CommandApduBuilder.SendAPDU("PACE MapNonce", command, _gap.SMContext);

            if (response.SW == 0x9000)
            {
                var mapResponse = new XYResponseAPDU(response);
                _gap.Pace.CardPublicKey = SecurityHelper.DecodeKey(mapResponse, _gap.Card.EF_CardAccess.PaceInfo.Algorithm);

                return true;
            }

            return false;
        }
    }
}
