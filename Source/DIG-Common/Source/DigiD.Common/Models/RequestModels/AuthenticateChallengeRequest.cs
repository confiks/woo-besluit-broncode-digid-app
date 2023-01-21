using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class AuthenticateChallengeRequest : BaseRequest
    {
        [JsonProperty("user_app_id")]
        public string AppId { get; set; }

        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }
    }
}
