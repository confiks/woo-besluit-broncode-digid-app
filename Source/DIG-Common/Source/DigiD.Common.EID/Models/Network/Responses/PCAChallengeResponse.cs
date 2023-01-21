using Newtonsoft.Json;

namespace DigiD.Common.EID.Models.Network.Responses
{
    internal class PcaChallengeResponse : EIDBaseResponse
    {
        [JsonProperty("responseData")]
        internal ChallengeResponseData ChallengeResponse {get; set; }
    }

    internal class ChallengeResponseData
    {
        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}
