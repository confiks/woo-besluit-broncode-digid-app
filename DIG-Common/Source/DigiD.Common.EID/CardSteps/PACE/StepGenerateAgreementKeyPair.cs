using System.Threading.Tasks;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using Org.BouncyCastle.Crypto.Parameters;

namespace DigiD.Common.EID.CardSteps.PACE
{
    /// <summary>
    /// The client generates a keypair based on the nonce of the previous step.
    /// </summary>
    internal class StepGenerateAgreementKeyPair : IStep
    {
        private readonly Gap _gap;

        public StepGenerateAgreementKeyPair(Gap gap)
        {
            _gap = gap;
        }

        public async Task<bool> Execute()
        {
            var agreementPair = await SecurityHelper.GenerateKeyPairAgreement(
                _gap.Pace.CardPublicKey,
                (ECPrivateKeyParameters)_gap.Pace.EphemeralKeyPair.Private,
                _gap.Pace.DecryptedNonce,
                _gap.Card.EF_CardAccess.PaceInfo.Algorithm);

            _gap.Pace.AgreementKeyPair = agreementPair;

            return true;
        }
    }
}
