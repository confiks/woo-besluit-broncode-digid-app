using DigiD.Common.Enums;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class BasicAuthenticationResponse : ActivationResponse
    {
        [JsonProperty("app_session_id")]
        public string SessionId { get; set; }

        public string activation_method { get; set; }

        [JsonProperty("app_authenticator_pending")]
        public bool PendingActivation { get; set; }

        [JsonProperty("sms_check_requested")]
        public bool IsSmsCheckRequested { get; set; }

        public ActivationMethod ActivationMethod
        {
            get
            {
                switch (activation_method)
                {
                    case "standalone":
                        return ActivationMethod.SMS;
                    case "request_for_account":
                        return ActivationMethod.AccountAndApp;
                    case "password":
                        return ActivationMethod.Password;
                }

                return ActivationMethod.Unsupported;
            }
        }
    }
}
