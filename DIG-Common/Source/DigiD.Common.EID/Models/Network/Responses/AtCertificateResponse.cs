using Newtonsoft.Json;

namespace DigiD.Common.EID.Models.Network.Responses
{
    internal class AtCertificateResponse: EIDBaseResponse
    {
        [JsonProperty("responseData")]
        internal CertificateResponseData Data { get; set; }
    }

    internal class CertificateResponseData
    {
        [JsonProperty("atCert")]
        internal string AtCertificate { get; set; }
    }
}
