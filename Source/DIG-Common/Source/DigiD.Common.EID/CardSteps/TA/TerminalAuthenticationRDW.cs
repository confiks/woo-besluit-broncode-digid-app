using System.Threading.Tasks;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Cards;
using DigiD.Common.EID.CardSteps.FileOperations;
using DigiD.Common.EID.CardSteps.TA.RDW;
using DigiD.Common.EID.Enums;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;
using Org.BouncyCastle.Crypto.Parameters;

namespace DigiD.Common.EID.CardSteps.TA
{
    public class TerminalAuthenticationRdw : BaseSecureCardOperation, IStep
    {
        internal TerminalAuthenticationRdw(ISecureCardOperation operation) : base((Gap)operation)
        {
        }

        public Certificate DvCert { get; set; }
        public ECPublicKeyParameters PublicKey { get; set; }
        public byte[] Ricc { get; set; }

        internal override void Init()
        {
            base.Init();

            Steps.Clear();
            Steps.Add(new StepSelectMF(this));
            Steps.Add(new StepSendPcaInfo(this));
            Steps.Add(new StepMseSetDst(this, false));
            // Verify the DVCA Certificate
            Steps.Add(new StepPsoVerifyCertificate(this, CertificateType.DVCACertificate));
            Steps.Add(new StepMseSetDst(this, true));
            // Verify the AT Certificate.
            Steps.Add(new StepPsoVerifyCertificate(this, CertificateType.ATCertificate));
            Steps.Add(new StepMseSetAT(this));
            Steps.Add(new StepGetChallenge(this));
            Steps.Add(new StepExternalAuthentication(this));
        }

        public override async Task<bool> Execute()
        {
            var result = await base.Execute();

            if (result)
                ((DrivingLicense)GAP.Card).TA = this;

            return result;
        }

        public override void StepCompleted(IStep stepNumber)
        {
            GAP.StepCompleted(stepNumber);
        }
    }
}
