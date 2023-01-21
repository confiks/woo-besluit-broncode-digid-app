using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.FileOperations
{
    internal class StepReadFiles : BaseSecureStep, IStep
    {
        private readonly bool _readPhoto;

        internal StepReadFiles(ISecureCardOperation operation, bool readPhoto) : base(operation)
        {
            _readPhoto = readPhoto;
        }

        public async Task<bool> Execute()
        {
            Gap gap;
            if (Operation is Gap operation)
                gap = operation;
            else
                gap = Operation.GAP;

            return await gap.Card.ReadFiles(_readPhoto, gap.SMContext);
        }
    }
}
