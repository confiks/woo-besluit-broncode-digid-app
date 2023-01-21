using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.EID.Models.Network.Responses
{
    internal class EIDBaseResponse : BaseResponse
    {
        [JsonProperty("sessionId")]
        internal string SessionId { get; set; }
    }
}
