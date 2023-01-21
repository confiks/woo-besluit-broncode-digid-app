using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Cards;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;
using DigiD.Common.EID.Models.CardFiles;

namespace DigiD.Common.EID.CardSteps.FileOperations
{
    /// <summary>
    /// The client reads EfCardSecurity
    /// See step 11, page 25 of the PCA implementation guidelines.
    /// </summary>
    internal class StepReadEFCardSecurity : BaseSecureStep, IStep
    {
        public StepReadEFCardSecurity(ISecureCardOperation operation) : base(operation)
        {
        }

        public async Task<bool> Execute()
        {
            var file = await SelectAndReadFile.Execute<EFCardSecurity>(CardFile.EFCardSecurity, Operation.GAP.SMContext);

            if (file == null)
                return false;

            ((DrivingLicense) Operation.GAP.Card).CardSecurity = file;
            return true;
        }
    }
}
