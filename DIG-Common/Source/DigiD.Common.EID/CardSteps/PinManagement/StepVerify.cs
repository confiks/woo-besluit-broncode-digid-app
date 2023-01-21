using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Enums;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;

namespace DigiD.Common.EID.CardSteps.PinManagement
{
    internal class StepVerify : BaseSecureStep, IStep
    {
        private readonly CardCredentials _credentials;
        private readonly PasswordType _passwordType;

        public StepVerify(ISecureCardOperation operation, CardCredentials credentials, PasswordType passwordType) : base(operation)
        {
            _credentials = credentials;
            _passwordType = passwordType;
        }

        public async Task<bool> Execute()
        {
            var command = CommandApduBuilder.GetVerifyCommand(_credentials.Password(_passwordType), _passwordType, Operation.GAP.SMContext);
            var response = await CommandApduBuilder.SendAPDU("Verify", command, Operation.GAP.SMContext);

            switch (response.SW)
            {
                case 0x9000:
                    return true;
                case 0x63c0:
                    Operation.GAP.IsBlocked = true;
                    Operation.GAP.AuthenticationResult = AuthenticationResult.Failed;
                    break;
            }

            return false;
        }
    }
}
