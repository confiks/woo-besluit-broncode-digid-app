using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class ReplaceExistingApplicationRequest : BaseRequest
    {
        public ReplaceExistingApplicationRequest(bool replace)
        {
            ReplaceExistingApplication = replace;
        }

        [JsonProperty("replace_application")]
        public bool? ReplaceExistingApplication { get; private set; }
    }
}
