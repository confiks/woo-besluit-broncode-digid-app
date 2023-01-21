using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.CardSteps.PA;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.BAC
{
    public class StepPassiveAuthentication : BaseSecureCardOperation, IStep
    {
        internal StepPassiveAuthentication(ISecureCardOperation operation) : base((Gap)operation)
        {
            
        }
        internal override void Init()
        {
            base.Init();

            Steps.Add(new StepValidateHashes(this));
        }
    }
}
