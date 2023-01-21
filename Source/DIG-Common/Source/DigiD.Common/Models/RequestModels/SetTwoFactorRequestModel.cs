using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class SetTwoFactorRequestModel : BaseMijnDigiDRequest
    {
        public SetTwoFactorRequestModel(bool enabled)
        {
            Enabled = enabled;
        }

        [JsonProperty("setting_2_factor")]
        public bool Enabled { get; set; }
    }
}
