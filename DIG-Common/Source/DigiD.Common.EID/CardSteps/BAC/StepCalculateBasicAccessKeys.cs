using System.Threading.Tasks;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.BAC
{
    /// <summary>
    /// COMPUTE KEYS FROM KEY SEED
    /// </summary>
    public class StepCalculateBasicAccessKeys : IStep
    {
        private readonly BacOperation _operation;

        public StepCalculateBasicAccessKeys(BacOperation operation)
        {
            _operation = operation;
        }
        public Task<bool> Execute()
        {
            _operation.KEnc = CryptoDesHelper.ComputeKey(_operation.KSeed, BacOperation.ENC_C);
            _operation.KMac = CryptoDesHelper.ComputeKey(_operation.KSeed, BacOperation.MAC_C);

            return Task.FromResult(true);
        }
    }
}
