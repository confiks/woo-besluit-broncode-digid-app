using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class WidSessionResponse : AuthenticationSessionResponse
    {
        [JsonProperty("url")]
        public string Host { get; set; }

        [JsonProperty("session_id")]
        public string SessionId { get; set; }
    }
}
