using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class ActivationCodeSessionRequest : BaseAuthSessionRequest
    {
        [JsonProperty("re_request_letter")]
        public bool RequestNewLetter { get; set; }
    }
}
