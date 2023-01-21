using DigiD.Common.Enums;
using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class EmailStatusResponse : BaseResponse
    {
        [JsonProperty("email_status")]
        public EmailStatus EmailStatus { get; set; }

        [JsonProperty("current_email_address")]
        public string EmailAddress { get; set; }

        [JsonProperty("no_verified_email_address")]
        public string NoVerifiedEmailAddress { get; set; }

        [JsonProperty("user_action_needed")]
        public bool UserActionNeeded { get; set; }
    }
}
