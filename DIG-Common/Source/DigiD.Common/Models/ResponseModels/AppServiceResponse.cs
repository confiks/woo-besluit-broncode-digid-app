using System.Collections.Generic;
using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class AppServiceResponse : BaseResponse
    {
        [JsonProperty("services")]
        public List<App> Services { get; set; } = new List<App>();
    }
}
