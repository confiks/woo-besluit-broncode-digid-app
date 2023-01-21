using System.Collections.Generic;
using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class StartApp2AppResponse : BaseResponse
    {
        [JsonProperty("app_session_id")]
        public string SessionId { get; set; }

        [JsonProperty("authentication_level")]
        public int AuthenticationLevel { get; set; }

        [JsonProperty("image_domain")]
        public string ImagedDomain { get; set; }

        [JsonProperty("apps")] public List<App> Apps { get; set; } = new List<App>();

        public string SAMLart { get; set; }

        public string RelayState { get; set; }
    }
}
