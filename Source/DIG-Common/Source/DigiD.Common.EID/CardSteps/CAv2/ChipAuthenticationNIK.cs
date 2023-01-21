using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.CardSteps.FileOperations;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.CAv2
{
    public class ChipAuthenticationNik : BaseSecureCardOperation, IStep
    {
        internal ChipAuthenticationNik(ISecureCardOperation operation) : base((Gap)operation)
        {
        }

        internal override void Init()
        {
            base.Init();

            Steps.Add(new StepReadDG14(this));
            Steps.Add(new StepReadEFSOD(this));
            Steps.Add(new StepReadEFCVCA(this));
            Steps.Add(new StepSendFilesToServer(this));
            Steps.Add(new StepMseSetAT(this));
            Steps.Add(new StepGeneralAuthenticate(this));
        }

        public override void StepCompleted(IStep stepNumber)
        {
            GAP.StepCompleted(stepNumber);
        }
    }
}
