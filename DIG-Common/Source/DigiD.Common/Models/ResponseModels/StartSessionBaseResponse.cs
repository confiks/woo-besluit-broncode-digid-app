using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class StartSessionBaseResponse : BaseResponse
    {
        [JsonProperty("app_session_id")]
        public string AppSessionId { get; set; }

        [JsonProperty("at")]
        public int At { get; set; }
    }
}
