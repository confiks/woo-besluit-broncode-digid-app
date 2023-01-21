using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class InitSessionResponse : BaseResponse
    {
        [JsonProperty("app_session_id")]
        public string SessionId { get; set; }
    }
}
