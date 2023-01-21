using Newtonsoft.Json;

namespace DigiD.Common.EID.Models.Network
{
    public class Header
    {
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("supportedAPIVersion")]
        public SupportedApiVersion SupportedAPIVersion { get; set; }
    }
}
