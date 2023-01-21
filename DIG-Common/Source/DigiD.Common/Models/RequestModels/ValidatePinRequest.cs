using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class ValidatePinRequest : BaseRequest
    {
        public ValidatePinRequest(string sessionId)
        {
            AuthenticationSessionId = sessionId;
        }

        [JsonProperty("user_app_id")]
        public string AppId { get; set; }

        [JsonProperty("app_public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("signed_challenge")]
        public string Signature { get; set; }

        [JsonProperty("masked_pincode")]
        public string PIN { get; set; }
    }
}
