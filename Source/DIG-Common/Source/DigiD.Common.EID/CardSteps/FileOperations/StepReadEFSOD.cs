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
    internal class StepReadEFSOD : BaseSecureStep, IStep
    {
        internal StepReadEFSOD(ISecureCardOperation operation) : base(operation)
        {
            
        }

        public async Task<bool> Execute()
        {
            Gap gap;

            if (Operation is Gap _gap)
                gap = _gap;
            else
                gap = Operation.GAP;

            var fileId = gap.Card.DocumentType == DocumentType.DrivingLicense ? "00-1D" : "01-1D";
            var file = await SelectAndReadFile.Execute<EFSOd>(new CardFile(P1.CHILD_EF, fileId.ConvertHexToBytes()), gap.SMContext);

            if (file == null)
                return false;

            gap.Card.EF_SOd = file;
            
            return true;
        }
    }
}
