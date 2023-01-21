using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class RequestAccountResponse : BaseResponse
    {
        [JsonProperty("delay")]
        public int Delay { get; set; }

        [JsonProperty("app_session_id")]
        public string AppSessionId { get; set; }
    }
}
