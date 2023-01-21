using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class EmailRegisterRequest : BaseMijnDigiDRequest
    {
        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }
    }
}
