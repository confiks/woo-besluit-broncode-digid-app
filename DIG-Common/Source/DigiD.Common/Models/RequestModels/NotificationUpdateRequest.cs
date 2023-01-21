using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class NotificationUpdateRequest : BaseRequest
    {
        [JsonProperty("notification_id")]
        public string NotificationToken { get; set; }
        
        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }

        [JsonProperty("user_app_id")]
        public string UserAppId { get; set; }
    }
}
