using System.Threading.Tasks;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.NFC.Enums;
using DigiD.Common.NFC.Models;

namespace DigiD.Common.EID.CardSteps.BAC
{
    public class StepRequestRandomNumber : IStep
    {
        private readonly BacOperation _operation;

        public StepRequestRandomNumber(BacOperation operation)
        {
            _operation = operation;
        }

        public async Task<bool> Execute()
        {   
            var cmd = new CommandApdu(CLA.PLAIN, INS.CHALLENGE, (int)P1.SELECT_MF, (int)P2.DEFAULT_CHANNEL, null, 8);
            var response = await CommandApduBuilder.SendAPDU("random", cmd, null);

            if (response.SW == 0x9000)
            {
                _operation.RND_IC = response.Data;
                return true;
            }

            return false;
        }
    }
}
