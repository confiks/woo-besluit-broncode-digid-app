using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Cards;
using DigiD.Common.EID.CardSteps.FileOperations;
using DigiD.Common.EID.Interfaces;

namespace DigiD.Common.EID.CardSteps.CAv2
{
    internal class ChipAuthenticationRdw : BaseSecureCardOperation, IStep
    {
        public byte[] Ricc { get; set; }
        public byte[] Ticc { get; set; }
        
        internal ChipAuthenticationRdw(ISecureCardOperation operation) : base((Gap)operation)
        {

        }

        internal override void Init()
        {
            base.Init();
            Steps.Add(new StepReadEFCardSecurity(this));
            Steps.Add(new StepSelectMF(this));
            Steps.Add(new StepMseSetAT(this));
            Steps.Add(new StepGeneralAuthenticate(this, ((DrivingLicense)GAP.Card).TA.PublicKey));
        }

        public override async Task<bool> Execute()
        {
            var result = await base.Execute();
            if (result)
                ((DrivingLicense)GAP.Card).CA = this;

            return result;
        }

        public override void StepCompleted(IStep stepNumber)
        {
            GAP.StepCompleted(stepNumber);
        }
    }
}
