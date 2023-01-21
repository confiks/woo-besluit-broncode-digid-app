using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Cards;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;
using DigiD.Common.EID.Models.CardFiles;

namespace DigiD.Common.EID.CardSteps.FileOperations
{
    internal class StepReadEFCVCA : BaseSecureStep, IStep
    {
        internal StepReadEFCVCA(ISecureCardOperation operation) : base(operation)
        {
            
        }

        public async Task<bool> Execute()
        {
            var file = await SelectAndReadFile.Execute<EFCVCA>(CardFile.EFCVCA, Operation.GAP.SMContext);

            if (file == null)
                return false;

            ((eNIK) Operation.GAP.Card).CVCA = file;
            return true;
        }
    }
}
