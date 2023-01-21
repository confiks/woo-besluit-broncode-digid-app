using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;
using DigiD.Common.EID.Models.CardFiles;
using Org.BouncyCastle.Crypto.Parameters;

namespace DigiD.Common.EID.CardSteps.CAv1
{
    internal class StepGeneralAuthenticate : BaseSecureStep, IStep
    {
        public StepGeneralAuthenticate(ISecureCardOperation operation) : base(operation)
        {
        }

        public async Task<bool> Execute()
        {
            // var pk = Operation.GAP.Pace.Credentials.RDEData.EphemeralPublicKey.GetEncoded(false);
            var pk = new byte[]{};
            var command = CommandApduBuilder.GetGeneralAuthenticateCAv1(pk, Operation.GAP.SMContext);
            var response = await CommandApduBuilder.SendAPDU("CAv1 General Authenticate", command, Operation.GAP.SMContext);

            if (response.SW == 0x9000)
            {
                var skPCD = (ECPrivateKeyParameters)Operation.GAP.Pace.EphemeralKeyPair.Private;
                var pkPICC = SecurityHelper.DecodeKey(((DG14)Operation.GAP.Card.DataGroups[14]).ChipAuthenticationPublicKeyInfo.Pk, ChipAuthenticationPublicKeyInfo.Algorithm);

                //Generate new SharedSecret by card private key, and EF_DG14 public key
                //Get X coordinate to user for kEnc enkMac generation
                var sharedSecretX = SecurityHelper.GenerateSharedSecret(skPCD, pkPICC).AffineXCoord.GetEncoded();

                var kEnc = SecurityHelper.CalculateKEnc(sharedSecretX, Operation.GAP.Card);
                var kMac = SecurityHelper.CalculateKMac(sharedSecretX, Operation.GAP.Card);

                Operation.GAP.SMContext.Init(kEnc, kMac);

                return true;
            }

            return false;
        }
    }
}
