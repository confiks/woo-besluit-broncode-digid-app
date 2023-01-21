using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class WidSessionRequest : BaseEIDRequest
    {
        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }

        [JsonProperty("device_name")]
        public string DeviceName { get; set; }
    }
}
