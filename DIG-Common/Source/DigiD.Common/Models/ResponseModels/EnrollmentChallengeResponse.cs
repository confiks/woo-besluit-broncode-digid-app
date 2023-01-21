using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class EnrollmentChallengeResponse : BaseResponse
    {
        [JsonProperty("challenge")]
        public string Challenge { get; set; }
    }
}
