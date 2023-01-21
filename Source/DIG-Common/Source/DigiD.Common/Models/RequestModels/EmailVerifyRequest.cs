using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class EmailVerifyRequest : BaseMijnDigiDRequest
    {
        [JsonProperty("verification_code")]
        public string Code { get; set; }
    }

    public class EmailConfirmRequest : BaseMijnDigiDRequest
    {
        [JsonProperty("email_address_confirmed")]
        public bool IsConfirmed { get; set; }
    }
}
