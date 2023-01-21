using System.Linq;
using System.Threading.Tasks;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;
using Org.BouncyCastle.Crypto.Parameters;

namespace DigiD.Common.EID.CardSteps.PACE
{
    /// <summary>
    /// Generate the shared secret, KEnc and kMac.
    /// </summary>
    internal class StepCalculateMacKeys : IStep
    {
        private readonly Gap _gap;

        public StepCalculateMacKeys(Gap gap)
        {
            _gap = gap;
        }
        public Task<bool> Execute()
        {
            var skAgreement = (ECPrivateKeyParameters)_gap.Pace.AgreementKeyPair.Private;
            var pkAgreement = (ECPublicKeyParameters)_gap.Pace.AgreementKeyPair.Public;

            var cardAgreedPublicKey = _gap.Pace.CardAgreedPublicKey;

            var sharedSecret = SecurityHelper.GenerateSharedSecret(skAgreement, cardAgreedPublicKey);
            var sharedSecretX = sharedSecret.AffineXCoord.GetEncoded();

            var terminalPublicKey = SecurityHelper.DecodeECPublicKeyX509(pkAgreement);
            var cardPublicKey = SecurityHelper.DecodeECPublicKeyX509(cardAgreedPublicKey);

            var terminalEllipticCoordinates = new DataObject(new byte[] { 0x86 }, terminalPublicKey).GetEncoded();
            var cardEllipticCoordinates = new DataObject(new byte[] { 0x86 }, cardPublicKey).GetEncoded();

            var terminalAuthTokenInputData = GetAuthTokenInputData(terminalEllipticCoordinates);
            var cardAuthTokenInputData = GetAuthTokenInputData(cardEllipticCoordinates);

            _gap.Pace.KEnc = SecurityHelper.CalculateKEnc(sharedSecretX, _gap.Card);
            _gap.Pace.KMac = SecurityHelper.CalculateKMac(sharedSecretX, _gap.Card);
            _gap.Pace.TerminalAuthToken = AesHelper.PerformCBC8(terminalAuthTokenInputData, _gap.Pace.KMac); //p_pcd
            _gap.Pace.TerminalToken = AesHelper.PerformCBC8(cardAuthTokenInputData, _gap.Pace.KMac);

            return Task.FromResult(true);
        }

        private byte[] GetAuthTokenInputData(byte[] coordinatesEncrypted)
        {
            var data = _gap.Card.EF_CardAccess.PaceInfo.OID.GetEncoded().Concat(coordinatesEncrypted).ToArray();
            return new DataObject(new byte[]{0x7F, 0x49}, data).GetEncoded();
        }
    }
}
