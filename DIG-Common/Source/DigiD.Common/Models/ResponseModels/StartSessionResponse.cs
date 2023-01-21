using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class StartSessionResponse : StartSessionBaseResponse
    {
        [JsonProperty("koppelcode")]
        public string VerificationCode { get; set; }
    }
}
