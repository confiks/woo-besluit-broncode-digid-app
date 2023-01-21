using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class AbortAuthenticationRequest : BaseEIDRequest
    {
        public AbortAuthenticationRequest(string code)
        {
            Code = code;
        }

        [JsonProperty("code")]
        public string Code { get; }
    }
}
