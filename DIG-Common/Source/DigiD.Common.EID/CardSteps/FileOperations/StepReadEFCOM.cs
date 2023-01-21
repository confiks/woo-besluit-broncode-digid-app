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
    internal class StepReadEFCOM : BaseSecureStep, IStep
    {
        private readonly Gap _operation;

        internal StepReadEFCOM(Gap operation) : base(operation)
        {
            _operation = operation;
        }

        public async Task<bool> Execute()
        {
            if (_operation.Card.EFCOM != null)
                return true;

            var fileId = _operation.Card.DocumentType == DocumentType.DrivingLicense ? "00-1E" : "01-1E";
            var file = await SelectAndReadFile.Execute<EFCOM>(new CardFile(P1.CHILD_EF, fileId.ConvertHexToBytes()), _operation.SMContext);
            Debugger.DumpInfo("EFCOM", file.Bytes);
            
            _operation.Card.EFCOM = file;
            return true;
        }
    }
}
