using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class BaseEIDRequest : BaseRequest
    {
        [JsonProperty("auth_session_id")] public new string AuthenticationSessionId => null;
    }
}
