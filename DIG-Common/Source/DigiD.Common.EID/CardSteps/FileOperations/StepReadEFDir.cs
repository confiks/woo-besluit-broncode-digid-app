using System.Threading.Tasks;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;
using DigiD.Common.EID.Models.CardFiles;

namespace DigiD.Common.EID.CardSteps.FileOperations
{
    internal class StepReadEFDir : IStep
    {
        private readonly Gap _gap;

        public StepReadEFDir(Gap gap)
        {
            _gap = gap;
        }

        public async Task<bool> Execute()
        {
            _gap.Card.EF_Dir = await SelectAndReadFile.Execute<EFDir>(CardFile.EFDir);
            return true;
        }
    }
}
