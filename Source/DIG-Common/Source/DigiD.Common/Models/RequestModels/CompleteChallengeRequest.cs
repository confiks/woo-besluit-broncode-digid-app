using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class CompleteChallengeRequest : BaseRequest
    {
        [JsonProperty("signed_challenge")]
        public string Signature { get; set; }

        [JsonProperty("app_public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("hardware_support")]
        public bool HardwareSupport { get; set; }

        [JsonProperty("nfc_support")]
        public bool? NFCSupport { get; set; }
    }
}
