using System.Linq;
using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Cards;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.TA.RDW
{
    /// <summary>
    /// Send a MSE command to the PCA to set up TA.
    /// We send the ephemeral key from StepSendPcaInfo to the PCA.
    /// See step 7, page 25 of the PCA implementation guidelines.
    /// </summary>
    internal class StepMseSetAT : BaseSecureStep, IStep
    {
        public StepMseSetAT(ISecureCardOperation operation) : base(operation)
        {
        }

        public async Task<bool> Execute()
        {
            var pkPCD = ((TerminalAuthenticationRdw)Operation).PublicKey.Q.GetEncoded(true).Skip(1).ToArray();
            var taOID = ((DrivingLicense)Operation.GAP.Card).ATCertificate.PublicKeyOID.Value;

            var command = CommandApduBuilder.GetMSESetATCommand(pkPCD, taOID, ((DrivingLicense)Operation.GAP.Card).ATCertificate.HolderReference.Value, Operation.GAP.SMContext);
            var encResp = await CommandApduBuilder.SendAPDU("MSESetAT", command, Operation.GAP.SMContext);

            return encResp.SW == 0x9000;
        }
    }
}
