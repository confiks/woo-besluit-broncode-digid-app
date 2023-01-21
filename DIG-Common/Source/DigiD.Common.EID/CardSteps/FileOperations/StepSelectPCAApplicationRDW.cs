using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Exceptions;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.FileOperations
{
    internal class StepSelectPcaApplicationRdw : BaseSecureStep, IStep
    {
        public StepSelectPcaApplicationRdw(ISecureCardOperation operation) : base(operation)
        {
        }

        public async Task<bool> Execute()
        {
            if (Operation.GAP == null)
                Operation.GAP = (Gap)Operation;

            var command = CommandApduBuilder.GetSelectApplicationCommand(Operation.GAP.Card.PolymorphicAID, Operation.GAP.SMContext);
            var response = await CommandApduBuilder.SendAPDU("SelectPCAApplication", command, Operation.GAP.SMContext);

            switch (response.SW1)
            {
                case 0x90:
                    return true;
                case 0x69:
                    return true;
                case 0x63:
                    {
                        Operation.GAP.ChangePinRequired = true;
                        Operation.GAP.PinTriesLeft = (byte)response.SW2 & 0xf;

                        throw new TransportPinException();
                    }
                default:
                    return false;
            }
        }
    }
}
