using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class ActivationResponse : BaseResponse
    {
        [JsonProperty("max_amount")]
        public int MaxAmount { get; set; }

        [JsonProperty("device_name")]
        public string DeviceName { get; set; }

        [JsonProperty("latest_date")]
        public string LastUsed { get; set; }
    }
}
