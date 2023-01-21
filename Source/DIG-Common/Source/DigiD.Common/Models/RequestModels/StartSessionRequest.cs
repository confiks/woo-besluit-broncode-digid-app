using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class StartSessionRequest : BaseRequest
    {
        [JsonProperty("koppelcode")]
        public string VerificationToken { get; set; }

        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }
    }
}
