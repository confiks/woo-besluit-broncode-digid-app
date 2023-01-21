using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class EmailRegisterResponse : BaseResponse
    {
        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }

        [JsonProperty("max_amount_emails")]
        public int MaxCount { get; set; }
    }
}
