using System;
using System.Security;
using DigiD.Common.EID.BaseClasses;
using DigiD.Common.EID.Enums;
using DigiD.Common.EID.Models;

namespace DigiD.Common.EID.Interfaces
{
    public interface IEIDOperationService
    {
        public BaseSecureCardOperation StartRandomize(UserConsent userConsent, string sessionId, string serverAddress, CardCredentials credentials, Action<float> progressChangedAction);

        public BaseSecureCardOperation StartChangePin(SecureString newPin, CardCredentials credentials, PasswordType passwordType, Action<float> progressChangedAction);

        public BaseSecureCardOperation StartResumePin(GAPType type, CardCredentials credentials, Action<float> progressChangedAction);

        public BaseSecureCardOperation StartStatus(PasswordType passwordType, Action<float> progressChangedAction);
    }
}
