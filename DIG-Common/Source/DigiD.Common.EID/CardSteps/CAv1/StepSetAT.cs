using System.Linq;
using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models.CardFiles;

namespace DigiD.Common.EID.CardSteps.CAv1
{
    /// <summary>
    /// Send a MSE command to the PCA to set up CA
    /// See step 12, page 25 of the PCA implementation guidelines.
    /// </summary>
    internal class StepSetAT : BaseSecureStep, IStep
    {
        public StepSetAT(ISecureCardOperation operation) : base(operation)
        {
            
        }

        public async Task<bool> Execute()
        {
            var oid = ((DG14)Operation.GAP.Card.DataGroups[14]).ChipAuthenticationInfo.OID.GetEncoded().Skip(2).ToArray();
            var command = CommandApduBuilder.GetMSESetATCAv1Command(oid, Operation.GAP.SMContext);
            var response = await CommandApduBuilder.SendAPDU("CAv1 Set AT", command, Operation.GAP.SMContext);

            return response.SW == 0x9000;
        }
    }
}
