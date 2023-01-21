using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class RequestStationSessionResponse : StartSessionBaseResponse
    {
        [JsonProperty("activation_code")]
        public string ActivationCode { get; set; }
    }
}
