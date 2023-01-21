using DigiD.Common.SessionModels;
using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class BaseMijnDigiDRequest : BaseRequest
    {
        [JsonProperty("mijn_digid_session_id")]
        public string SessionId => AppSession.AuthenticationSessionId;
        
        [JsonProperty("auth_session_id")]
        public new string AuthenticationSessionId => null;
    }
}
