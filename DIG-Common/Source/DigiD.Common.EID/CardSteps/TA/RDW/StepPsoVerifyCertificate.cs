using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Cards;
using DigiD.Common.EID.Enums;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;

namespace DigiD.Common.EID.CardSteps.TA.RDW
{
    /// <summary>
    /// Verify either the DVCA or TA Certificate
    /// See step 6, page 24 of the PCA implementation guidelines.
    /// </summary>
    internal class StepPsoVerifyCertificate : BaseSecureStep, IStep
    {
        private readonly CertificateType _certType;

        public StepPsoVerifyCertificate(ISecureCardOperation operation, CertificateType certificateType) : base(operation)
        {
            _certType = certificateType;
        }

        public async Task<bool> Execute()
        {
            var cert = _certType == CertificateType.DVCACertificate ? ((TerminalAuthenticationRdw)Operation).DvCert : ((DrivingLicense)Operation.GAP.Card).ATCertificate;
            
            var commands = CommandApduBuilder.GetPSOVerifyCertificateCommand(cert, Operation.GAP.SMContext);

            for (var x = 0; x <= commands.Length - 1; x++)
            {
                var command = commands[x];
                var response = await CommandApduBuilder.SendAPDU("TA PSOVerifyCertificate", command, Operation.GAP.SMContext, false);

                if (response.SW != 0x9000)
                    return false;

                if (x != commands.Length - 1) continue;

                response = response.DecryptAES(Operation.GAP.SMContext);
                return response.SW == 0x9000;
            }

            return false;
        }
    }
}
