using Newtonsoft.Json;

namespace DigiD.Common.EID.Models.Network.Responses
{
    internal class SetupSecureMessagingResponse: EIDBaseResponse
    {
        [JsonProperty("responseData")]
        internal SecureMessagingResponse Data { get; set; }
    }

    internal class SecureMessagingResponse
    {
        [JsonProperty("apdus")]
        internal string[] Commands { get; set; }
    }
}
