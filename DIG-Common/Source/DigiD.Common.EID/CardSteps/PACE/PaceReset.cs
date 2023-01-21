using DigiD.Common.EID.Enums;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;

namespace DigiD.Common.EID.CardSteps.PACE
{
    /// <inheritdoc />
    /// <summary>
    /// This Step will reset the pace session with new credentials
    /// </summary>
    public class PaceReset : PaceOperation
    {
        private readonly PasswordType _resetType;

        internal PaceReset(ISecureCardOperation operation, CardCredentials credentials, PasswordType passwordType, PasswordType resetType) 
            : base(operation, credentials, passwordType)
        {
            _resetType = resetType;
        }

        /// <inheritdoc />
        /// <summary>
        /// Set all steps
        /// </summary>
        internal override void Init()
        {
            base.Init();

            Steps.Add(new StepResetPace(_resetType, this));
            Steps.Add(new StepSetAT(Operation));
            Steps.Add(new StepGetEncryptedNonce(Operation));
            Steps.Add(new StepGenerateKeyPair(this, Operation));
            Steps.Add(new StepMapNonce(Operation));
            Steps.Add(new StepGenerateAgreementKeyPair(Operation));
            Steps.Add(new StepGetCardAgreementPublicKey(Operation));
            Steps.Add(new StepCalculateMacKeys(Operation));
            Steps.Add(new StepMutualAuthentication(Operation));
            Steps.Add(new StepValidateOperation(Operation));
        }
    }
}
