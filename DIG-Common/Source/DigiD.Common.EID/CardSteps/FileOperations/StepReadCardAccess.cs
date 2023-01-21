using System.Threading.Tasks;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;
using DigiD.Common.EID.Models.CardFiles;

namespace DigiD.Common.EID.CardSteps.FileOperations
{
    public class StepReadCardAccess : IStep
    {
        private readonly Gap _gap;

        public StepReadCardAccess(Gap gap)
        {
            _gap = gap;
        }
        public async Task<bool> Execute()
        {
            var file = await SelectAndReadFile.Execute<EFCardAccess>(CardFile.EFCardAccess);

            if (file == null)
                return false;

            _gap.Card.EF_CardAccess = file;
            return true;
        }
    }
}
