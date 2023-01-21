using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class ValidatePinResponse : BaseResponse
    {
        [JsonProperty("remaining_attempts")] 
        public int RemainingAttempts { get; set; }

        [JsonProperty("authentication_level")]
        public int AuthenticationLevel { get; set; }
    }
}
