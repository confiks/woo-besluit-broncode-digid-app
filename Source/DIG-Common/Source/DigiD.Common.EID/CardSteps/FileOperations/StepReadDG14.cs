using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;
using DigiD.Common.EID.Models.CardFiles;
using DigiD.Common.NFC.Enums;
using DigiD.Common.NFC.Helpers;

namespace DigiD.Common.EID.CardSteps.FileOperations
{
    internal class StepReadDG14 : BaseSecureStep, IStep
    {
        public StepReadDG14(ISecureCardOperation operation) : base(operation)
        {

        }

        public async Task<bool> Execute()
        {
            Gap gap;

            if (Operation is Gap operation)
                gap = operation;
            else
                gap = Operation.GAP;

            if (gap.Card.DataGroups.ContainsKey(14))
                return true;

            var f = new CardFile(P1.CHILD_EF, (gap.Card.DocumentType == DocumentType.DrivingLicense ? "00-0E" : "01-0E").ConvertHexToBytes());
            var file = await SelectAndReadFile.Execute<DG14>(f, gap.SMContext);

            if (file == null)
                return false;

            gap.Card.DataGroups.Add(14, file);
            return true;
        }
    }
}
