using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class NotificationRegisterRequest : BaseRequest
    {
        [JsonProperty("notification_id")]
        public string NotificationToken { get; set; }
        
        [JsonProperty("receive_notifications")]
        public bool Enabled { get; set; }
    }
}
