using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class RequestStationSessionRequest : BaseRequest
    {
        [JsonProperty("authenticate")]
        public string Authenticate { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }

        [JsonProperty("device_name")]
        public string DeviceName { get; set; }
    }
}
