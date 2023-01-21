using Newtonsoft.Json;

namespace DigiD.Common.EID.Models.Network
{
    public class SupportedApiVersion
    {
        [JsonProperty("major")]
        public string Major { get; set; }

        [JsonProperty("minor")]
        public string Minor { get; set; }
    }
}
