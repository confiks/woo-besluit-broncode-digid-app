using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class AuthenticationSessionResponse : BaseResponse
    {
        [JsonProperty("webservice")]
        public string WebService { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("return_url")]
        public string ReturnURL { get; set; }
    }
}
