using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
	public class AuthenticationStatusResponse : BaseResponse
    {
        [JsonProperty("session_received")]
        public bool SessionReceived { get; set; }
    }
}
