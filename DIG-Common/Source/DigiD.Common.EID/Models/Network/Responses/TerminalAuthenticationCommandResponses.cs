using System.Collections.Generic;
using Newtonsoft.Json;

namespace DigiD.Common.EID.Models.Network.Responses
{
    internal class TerminalAuthenticationCommandResponses : EIDBaseResponse
    {
        [JsonProperty("responseData")]
        internal TerminalAuthenticationCommandResponsesData Data { get; set; }
    }

    internal class TerminalAuthenticationCommandResponsesData
    {
        [JsonProperty("apdus")]
        internal List<string> Commands { get; set; }

        [JsonProperty("ephemeralPKey")]
        internal string EphemeralKey { get; set; }
    }
}
