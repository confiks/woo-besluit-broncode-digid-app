using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class ResendSmsRequest : BaseRequest
    {
        public ResendSmsRequest(bool spoken)
        {
            SpokenSMS = spoken.ToString().ToLowerInvariant();
        }

        [JsonProperty("spoken")]
        public string SpokenSMS { get; set; }
    }
}
