using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class ConfigResponse : BaseResponse
    {
        [JsonProperty("digid_rda_enabled")]
        public bool RDAEnabled { get; set; } = true;

        [JsonProperty("request_station_enabled")]
        public bool RequestStationEnabled { get; set; }

        [JsonProperty("letter_request_delay")]
        public int LetterRequestDelay { get; set; } = 3;

        [JsonProperty("max_pin_change_per_day")]
        public int MaxPinChangePerDay { get; set; } = 1;
    }
}
