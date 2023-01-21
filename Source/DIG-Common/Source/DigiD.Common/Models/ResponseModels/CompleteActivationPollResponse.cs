using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class CompleteActivationPollResponse : ActivationResponse
    {
        [JsonProperty("user_app_id")]
        public string AppId { get; set; }
    }
}
