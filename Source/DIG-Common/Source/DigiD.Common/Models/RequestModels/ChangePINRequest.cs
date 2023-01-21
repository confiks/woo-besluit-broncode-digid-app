using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class ChangePinRequest : BaseRequest
    {
        public ChangePinRequest(string encryptedPin)
        {
            NewPIN = encryptedPin;
        }

        [JsonProperty("masked_pincode")]
        public string NewPIN { get; set; }
    }
}
