using System;
using System.Security;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Enums;
using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Interfaces;
using DigiD.Common.EID.Models;

namespace DigiD.Common.EID.Demo
{
    public class EIDOperationService : IEIDOperationService
    {
        public BaseSecureCardOperation StartRandomize(UserConsent userConsent, string sessionId, string serverAddress,
            CardCredentials credentials, Action<float> progressChangedAction)
        {
            return new DemoCardOperation(CardOperationType.Authentication, progressChangedAction, credentials, PasswordType.PIN, DemoHelper.CardState.Value);
        }

        public BaseSecureCardOperation StartChangePin(SecureString newPin, CardCredentials credentials, PasswordType passwordType,
            Action<float> progressChangedAction)
        {
            DemoHelper.CardState.Value.NewPIN = newPin.ToPlain();
            return new DemoCardOperation(CardOperationType.ChangePin, progressChangedAction, credentials, passwordType, DemoHelper.CardState.Value);
        }

        public BaseSecureCardOperation StartResumePin(GAPType type, CardCredentials credentials, Action<float> progressChangedAction)
        {
            return new DemoCardOperation(CardOperationType.ResumePin, progressChangedAction, credentials, PasswordType.CAN, DemoHelper.CardState.Value);
        }

        public BaseSecureCardOperation StartStatus(PasswordType passwordType, Action<float> progressChangedAction)
        {
            return new DemoCardOperation(CardOperationType.Status, progressChangedAction, null, passwordType, DemoHelper.CardState.Value);
        }
    }
}
