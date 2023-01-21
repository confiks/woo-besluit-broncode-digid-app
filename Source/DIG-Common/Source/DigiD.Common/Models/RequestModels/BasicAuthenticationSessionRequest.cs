using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class BasicAuthenticationSessionRequest : SessionRequest
    {
        [JsonProperty("smscode")]
        public string SMSCode { get; set; }

        [JsonProperty("device_name")]
        public string DeviceName { get; set; }

        [JsonProperty("remove_old_app")]
        public bool RemoveOldApp { get; set; }
    }
}
