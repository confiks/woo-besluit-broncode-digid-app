using System.Threading.Tasks;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.NFC.Enums;

namespace DigiD.Common.EID.CardSteps.FileOperations
{
    /// <summary>
    /// Select the Master File on the PCA
    /// </summary>
    internal class StepSelectMF : IStep
    {
        private Gap _gap;
        private readonly ISecureCardOperation _operation;
        
        public StepSelectMF(ISecureCardOperation operation)
        {
            _operation = operation;
        }

        public StepSelectMF(Gap gap)
        {
            _gap = gap;
        }

        public async Task<bool> Execute()
        {
            if (_gap == null)
                _gap = _operation.GAP;

            var command = CommandApduBuilder.GetMFSelectCommand(_gap.SMContext, _gap.Card.DocumentType == DocumentType.IDCard);
            
            var response = await CommandApduBuilder.SendAPDU("Select MasterFile", command, _gap.SMContext);

            return response.SW == 0x9000 || response.SW == 0x6982;
        }
    }
}
