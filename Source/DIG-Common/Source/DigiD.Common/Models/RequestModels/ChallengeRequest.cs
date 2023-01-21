using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class ChallengeRequest : BaseRequest
    {
        public ChallengeRequest(string sessionId)
        {
            AuthenticationSessionId = sessionId;
        }

        [JsonProperty("user_app_id")]
        public string AppId { get; set; }

        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }
    }
}
