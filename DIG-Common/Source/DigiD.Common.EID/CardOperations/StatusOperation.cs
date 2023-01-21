using System;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.CardSteps;
using DigiD.Common.EID.Enums;

namespace DigiD.Common.EID.CardOperations
{
    public class StatusOperation : BaseSecureCardOperation
    {
        private readonly PasswordType _passwordType;
        public StatusOperation(PasswordType passwordType, Action<float> progressChangedAction) : base(progressChangedAction)
        {
            _passwordType = passwordType;
        }

        internal override void Init()
        {
            base.Init();
            Steps.Add(new StepGap(null, _passwordType, GAPType.PaceStatus, this, ChangeProgress));
        }
    }
}
