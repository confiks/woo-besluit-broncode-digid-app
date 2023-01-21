using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class EmailVerifyResponse : BaseResponse
    {
        [JsonProperty("remaining_attempts")]
        public int RemainingAttempts { get; set; }
    }
}
