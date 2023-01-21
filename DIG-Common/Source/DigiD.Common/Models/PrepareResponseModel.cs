using DigiD.Common.Http.Models;
using DigiD.Common.Models.RequestModels;
using Newtonsoft.Json;

namespace DigiD.Common.Models
{
    public class PrepareResponseModel : BaseResponse
    {
        [JsonProperty("mappedNonce")]
        public byte[] MappedNonce { get; set; }
    }

    public class MapResponseModel : BaseResponse
    {
        [JsonProperty("keyAgree")]
        public byte[] KeyAgree { get; set; }
    }

    public class AgreeKeyResponseModel : BaseResponse
    {
        [JsonProperty("token")]
        public byte[] Token { get; set; }
    }

    public class MutualAuthResponseModel : CommandResponse
    {
        [JsonProperty("mappedNonce")]
        public byte[] MappedNonce { get; set; }
    }
}
