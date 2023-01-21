using DigiD.Common.Http.Enums;
using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class TwoFactorResponseModel : BaseResponse
    {
        [JsonProperty("setting_2_factor")]
        public bool Enabled { get; set; }

        public bool IsAppOnlyAccount => ApiResult == ApiResult.Nok && ErrorMessage == "no_username_password";
    }
}
