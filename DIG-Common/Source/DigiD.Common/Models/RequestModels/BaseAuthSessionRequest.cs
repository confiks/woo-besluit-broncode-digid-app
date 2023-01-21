using DigiD.Common.SessionModels;
using DigiD.Common.Settings;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DigiD.Common.Models.RequestModels
{
    public class BaseAuthSessionRequest : BaseRequest
    {
        public BaseAuthSessionRequest()
        {
            AuthenticationSessionId = AppSession.AuthenticationSessionId;
            InstanceId = DependencyService.Get<IGeneralPreferences>().InstanceId;
            AppId = DependencyService.Get<IMobileSettings>().AppId;
        }

        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }

        [JsonProperty("user_app_id")]
        public string AppId { get; set; }
    }
}
