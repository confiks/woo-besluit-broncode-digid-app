using System.Linq;
using System.Threading.Tasks;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.BAC
{
    public class StepCalculateKSeed : IStep
    {
        private readonly BacOperation _operation;
        
        public StepCalculateKSeed(BacOperation operation)
        {
            _operation = operation;
        }

        public Task<bool> Execute()
        {
            var mrzHash = CryptoDesHelper.ComputeSHA1Hash(_operation.Mrz.ToBytes());

            // return 16 most significant bytes of hash
            var kSeed = mrzHash.Take(16).ToArray();
            _operation.KSeed = kSeed;

            return Task.FromResult(kSeed.Length > 0);
        }
    }
}
