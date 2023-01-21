using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class LetterActivationRequest : BaseRequest
    {
        [JsonProperty("activationcode")]
        public string ActivationCode { get; set; }
    }
}
