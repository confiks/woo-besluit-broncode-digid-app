using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class ConfirmResponse : BaseResponse
    {
        [JsonProperty("deactivate")] 
        public bool NeedDeactivation { get; set; }
    }
}
