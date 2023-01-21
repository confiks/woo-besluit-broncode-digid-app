using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class SessionRequest : BaseRequest
    {
        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }
    }
}
