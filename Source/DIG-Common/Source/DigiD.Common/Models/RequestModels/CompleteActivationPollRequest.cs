using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class CompleteActivationPollRequest : BaseRequest
    {
        [JsonProperty("activation_code")]
        public string ActivationCode { get; set; }

        [JsonProperty("remove_old_app")]
        public bool RemoveOldApp { get; set; }
    }
}
