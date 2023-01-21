using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class ChallengeResponse : BaseResponse
    {
        [JsonProperty("challenge")]
        public string Challenge { get; set; }

        [JsonProperty("iv")]
        public string IV { get; set; }
    }
}
