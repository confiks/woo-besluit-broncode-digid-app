using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.TA.RDW
{
    /// <summary>
    /// Send the DVCA or the TA Certificate to the PCA
    /// in preparation of the TA protocol
    /// See step 6, page 24 of the PCA implementation guidelines.
    /// </summary>
    internal class StepMseSetDst : BaseSecureStep, IStep
    {
        private readonly bool _needCertificate;

        public StepMseSetDst(ISecureCardOperation operation, bool needCertificate) : base(operation)
        {
            _needCertificate = needCertificate;
        }

        public async Task<bool> Execute()
        {
            var data = _needCertificate ? ((TerminalAuthenticationRdw)Operation).DvCert.HolderReference.Value : Operation.GAP.Pace.Car;
            var command = CommandApduBuilder.GetSetDSTCommand(data, Operation.GAP.SMContext);
            var response = await CommandApduBuilder.SendAPDU("TA MSESetDST", command, Operation.GAP.SMContext);
            
            return response.SW == 0x9000;
        }
    }
}
