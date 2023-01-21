using Newtonsoft.Json;

namespace DigiD.Common.EID.Models.Network.Responses
{
    internal class PcaInfoServerResponse: EIDBaseResponse
    {
        [JsonProperty("responseData")]
        internal InfoResponseData InfoResponse { get; set; }
    }

    internal class InfoResponseData
    {
        [JsonProperty("dvCert")]
        public string DvCertificate { get; set; }

        [JsonProperty("ephemeralKey")]
        public string EphemeralKey { get; set; }
    }
}
