using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class SessionResponse : ActivationResponse
    {
        [JsonProperty("user_app_id")]
        public string AppId { get; set; }

        [JsonProperty("koppelcode")]
        public string VerificationToken { get; set; }
    }
}
