using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class EnrollmentChallengeRequest : BaseRequest
    {
        [JsonProperty("user_app_id")]
        public string AppId { get; set; }

        [JsonProperty("app_public_key")]
        public string PublicKey { get; set; }
    }
}
