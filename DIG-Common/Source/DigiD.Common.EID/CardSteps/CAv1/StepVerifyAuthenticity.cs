using System.Linq;
using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.NFC.Helpers;

namespace DigiD.Common.EID.CardSteps.CAv1
{
    internal class StepVerifyAuthenticity : BaseSecureStep, IStep
    {
        public StepVerifyAuthenticity(ISecureCardOperation operation) : base(operation)
        {
        }

        public Task<bool> Execute()
        {
            foreach (var dg in Operation.GAP.Card.DataGroups.OrderBy(x => x.Key))
            {
                var hash = AesHelper.CalculateHash(dg.Value.Bytes, Operation.GAP.Card.MessageDigestAlgorithm, Operation.GAP.Card.KeyLength);

                if (!Operation.GAP.Card.EF_SOd.Hashes[dg.Key].SequenceEqual(hash))
                    return Task.FromResult(false);

                Debugger.WriteLine($"DG{dg.Key}\r\n" +
                                   $"Stored:     {Operation.GAP.Card.EF_SOd.Hashes[dg.Key].ToHexString()}\r\n" +
                                   $"Calculated: {hash.ToHexString()}");
            }

            return Task.FromResult(true);
        }
    }
}
