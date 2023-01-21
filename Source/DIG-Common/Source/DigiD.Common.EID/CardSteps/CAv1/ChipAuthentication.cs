using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.CardSteps.FileOperations;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.CAv1
{
    internal class ChipAuthentication : BaseSecureCardOperation, IStep
    {
        internal ChipAuthentication(ISecureCardOperation operation) : base((Gap)operation)
        {

        }
        internal override void Init()
        {
            base.Init();

            Steps.Add(new StepReadEFSOD(this));
            Steps.Add(new StepReadDG14(this));
            Steps.Add(new StepVerifyAuthenticity(this));
            Steps.Add(new StepSetAT(this));
            Steps.Add(new StepGeneralAuthenticate(this));
        }
    }
}
