using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.FileOperations
{
    internal class StepSelecteDLApplication : BaseSecureStep, IStep
    {
        internal StepSelecteDLApplication(ISecureCardOperation operation) : base(operation)
        {
        }

        public async Task<bool> Execute()
        {
            Gap gap;

            if (Operation is Gap _gap)
                gap = _gap;
            else
                gap = Operation.GAP;

            var command = CommandApduBuilder.GetSelectApplicationCommand(gap.Card.CardAID, gap.SMContext);
            var response = await CommandApduBuilder.SendAPDU("SelecteDLApplication", command, gap.SMContext);

            switch (response.SW1)
            {
                case 0x90:
                    return true;
                default:
                    return false;
            }
        }
    }
}
