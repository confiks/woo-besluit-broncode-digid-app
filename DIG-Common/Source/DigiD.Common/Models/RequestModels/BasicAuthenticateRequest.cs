using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public sealed class BasicAuthenticateRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("device_name")]
        public string DeviceName { get; set; }

        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }

        [JsonProperty("remove_old_app")]
        public bool RemoveOldApp { get; set; }
    }
}
