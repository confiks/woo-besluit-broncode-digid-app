using System.Collections.Generic;
using Newtonsoft.Json;

namespace DigiD.Common.EID.Models.Network.Responses
{
    internal class PcaCommandsResponse : EIDBaseResponse
    {
        [JsonProperty("responseData")]
        internal ResponseData Data { get; set; }
    }

    internal class ResponseData
    {
        [JsonProperty("apdus")]
        internal List<string> APDUCommandos { get; set; }
    }
}
