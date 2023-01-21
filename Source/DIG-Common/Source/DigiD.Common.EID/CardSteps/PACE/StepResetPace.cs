using System.Threading.Tasks;
using DigiD.Common.EID.Enums;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.PACE
{
    public class StepResetPace : IStep
    {
        private readonly PasswordType _passwordType;
        private readonly PaceOperation _pace;

        public StepResetPace(PasswordType passwordType, PaceOperation pace)
        {
            _passwordType = passwordType;
            _pace = pace;
        }
        public Task<bool> Execute()
        {
            _pace.PasswordType = _passwordType;
            return Task.FromResult(true);
        }
    }
}
