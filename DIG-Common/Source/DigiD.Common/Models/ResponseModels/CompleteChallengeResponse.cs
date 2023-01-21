using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class CompleteChallengeResponse : BaseResponse
    {
        [JsonProperty("symmetric_key")]
        public string SymmetricKey { get; set; }

        [JsonProperty("iv")]
        public string IV { get; set; }

        public bool Success => !string.IsNullOrEmpty(Status) && Status.ToLowerInvariant() == "ok";
    }

    public class CompleteActivationResponse : BaseResponse
    {
        [JsonProperty("authentication_level")]
        public int AuthenticationLevel { get; set; }
    }
}
