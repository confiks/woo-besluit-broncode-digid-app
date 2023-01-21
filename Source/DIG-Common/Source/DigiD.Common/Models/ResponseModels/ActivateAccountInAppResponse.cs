using System;
using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class ActivationLetterResponse : BaseResponse
    {
        [JsonProperty("remaining_attempts")]
        public int RemainingAttempts { get; set; }

        [JsonProperty("date_letter_sent")]
        public DateTime DateLetterSent { get; set; }
    }
}
