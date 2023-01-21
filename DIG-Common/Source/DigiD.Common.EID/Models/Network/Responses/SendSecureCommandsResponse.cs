using Newtonsoft.Json;

namespace DigiD.Common.EID.Models.Network.Responses
{
    internal class SendSecureCommandsResponse: EIDBaseResponse
    {
        [JsonProperty("responseData")]
        internal SendCommandsResult Data { get; set; }
    }

    internal class SendCommandsResult
    {
        [JsonProperty("result")]
        internal string Result { get; set; }
    }
}
