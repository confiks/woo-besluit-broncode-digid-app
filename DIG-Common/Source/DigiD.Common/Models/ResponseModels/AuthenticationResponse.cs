using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class AuthenticationResponse : BaseResponse
    {
        [JsonProperty("authentication_level")] public int AuthenticationLevel { get; set; }

        [JsonProperty("remaining_attempts")] public int RemainingAttempts { get; set; }

        [JsonProperty("deactivate")] public bool NeedDeactivation { get; set; }
    }
}
