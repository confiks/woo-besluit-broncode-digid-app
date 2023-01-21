using System.Threading.Tasks;
using DigiD.Common.EID.CardSteps.PACE;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps
{
    /// <summary>
    /// Generate a public/private key pair for PACE and TA.
    /// </summary>
    internal class StepGenerateKeyPair : IStep
    {
        private readonly ICardOperation _operation;
        private readonly Gap _gap;

        public StepGenerateKeyPair(ICardOperation operation, Gap gap)
        {
            _operation = operation;
            _gap = gap;
        }

        public Task<bool> Execute()
        {
            switch (_operation)
            {
                case PaceOperation p:
                    p.EphemeralKeyPair = SecurityHelper.GenerateKeyPair(_gap.Card.EF_CardAccess.PaceInfo.Algorithm);
                    break;

            }

            return Task.FromResult(true);
        }
    }
}
