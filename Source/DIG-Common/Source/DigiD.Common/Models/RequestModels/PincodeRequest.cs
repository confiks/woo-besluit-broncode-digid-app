using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class PincodeRequest : BaseRequest
    {
        [JsonProperty("masked_pincode")]
        public string MaskedPincode { get; set; }

        [JsonProperty("user_app_id")]
        public string AppId { get; set; }
    }
}
