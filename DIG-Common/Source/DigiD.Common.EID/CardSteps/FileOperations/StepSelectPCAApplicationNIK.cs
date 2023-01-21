using System.Threading.Tasks;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.FileOperations
{
    internal class StepSelectPcaApplicationNik : IStep
    {
        private readonly Gap _gap;
        public StepSelectPcaApplicationNik(Gap gap)
        {
            _gap = gap;
        }

        public async Task<bool> Execute()
        {
            var command = CommandApduBuilder.GetSelectApplicationCommand(_gap.Card.PolymorphicAID, _gap.SMContext);
            var response = await CommandApduBuilder.SendAPDU("SelectApplication", command, _gap.SMContext);

            return response.SW1 == 0x90;
        }
    }
}
