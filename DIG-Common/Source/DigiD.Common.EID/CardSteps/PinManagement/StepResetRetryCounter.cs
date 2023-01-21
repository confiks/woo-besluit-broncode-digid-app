using System.Security;
using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.PinManagement
{
    internal class StepResetRetryCounter : BaseSecureStep, IStep
    {
        private readonly SecureString _newPin;

        public StepResetRetryCounter(ISecureCardOperation operation, SecureString newPin) : base(operation)
        {
            _newPin = newPin;
        }

        public async Task<bool> Execute()
        {
            var command = CommandApduBuilder.GetResetRetryCounterCommand(_newPin, Operation.GAP.SMContext);
            var response = await CommandApduBuilder.SendAPDU("ResetRetryCounter", command, Operation.GAP.SMContext);

            return response.SW == 0x9000;
        }
    }
}
