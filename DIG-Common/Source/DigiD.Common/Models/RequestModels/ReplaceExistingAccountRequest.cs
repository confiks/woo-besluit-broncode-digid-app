using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class ReplaceExistingAccountRequest : BaseRequest
    {
        public ReplaceExistingAccountRequest(bool replace)
        {
            ReplaceExistingAccount = replace;
        }

        [JsonProperty("replace_account")]
        public bool? ReplaceExistingAccount { get; private set; }
    }
}
