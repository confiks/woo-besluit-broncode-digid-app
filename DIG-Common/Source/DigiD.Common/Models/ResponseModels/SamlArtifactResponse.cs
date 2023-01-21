using DigiD.Common.Http.Models;

namespace DigiD.Common.Models.ResponseModels
{
    public class SamlArtifactResponse : BaseResponse
    {
        public string SAMLart { get; set; }
        public string RelayState { get; set; }
    }
}
