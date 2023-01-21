using DigiD.Common.Http.Enums;
using Newtonsoft.Json;

namespace DigiD.Common.Http.Models
{
    public class BaseResponse
    {
        [JsonIgnore]
        public ApiResult ApiResult { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("error")]
        public string ErrorMessage { get; set; }

        [JsonProperty("payload")]
        public dynamic Payload { get; set; } 
    }
}
