using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class RdaSessionResponse : BaseResponse
    {
        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        public string Url { get; set; }
    }
}
