using DigiD.Common.Http.Enums;
using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class VersionCheckResponse : BaseResponse
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("update_url")]
        public string UpdateURL { get; set; }

        [JsonIgnore]
        public ApiResult VersionAction
        {
            get
            {
                switch (Action?.ToLowerInvariant())
                {
                    case "active":
                        return ApiResult.Ok;
                    case "update_warning":
                        return ApiResult.UpdateWarning;
                    case "force_update":
                        return ApiResult.ForceUpdate;
                    case "kill_app":
                        return ApiResult.Kill;
                    default:
                        return ApiResult.Unknown;
                }
            }
        }
    }
}

