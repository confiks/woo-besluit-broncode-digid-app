using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class RequestSmsResponse : BaseResponse
    {
        [JsonProperty("phonenumber")]
        public string PhoneNumber { get; set; }
    }
}
