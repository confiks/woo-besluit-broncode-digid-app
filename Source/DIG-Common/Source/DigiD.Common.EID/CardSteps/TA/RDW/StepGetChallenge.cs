using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.TA.RDW
{
    /// <summary>
    /// Request a random challenge of the PCA.
    /// This challenge is to be send to the server.
    /// See page 8, page 25 of the PCA implementation guidelines.
    /// </summary>
    internal class StepGetChallenge : BaseSecureStep, IStep
    {
        public StepGetChallenge(ISecureCardOperation operation) : base(operation)
        {
        }

        public async Task<bool> Execute()
        {
            var command = CommandApduBuilder.GetChallengeCommand(Operation.GAP.SMContext);
            var response = await CommandApduBuilder.SendAPDU("TA GetChallenge", command, Operation.GAP.SMContext);

            if (response.SW == 0x9000)
            {
                ((TerminalAuthenticationRdw)Operation).Ricc = response.Data;
                return true;
            }

            return false;
        }
    }
}
