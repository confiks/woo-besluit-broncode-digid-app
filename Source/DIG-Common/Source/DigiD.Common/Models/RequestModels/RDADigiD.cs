using System.Collections.Generic;
using DigiD.Common.Http.Models;
using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class RdaCreateSessionResponse : BaseResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("confirmSecret")]
        public string ConfirmSecret { get; set; }

        [JsonProperty("expiration")]
        public int Expiration { get; set; }
    }

    public class RdaStartResponse : BaseResponse
    {
        public string Challenge { get; set; }
        
        [JsonProperty("pace")]
        public byte[] Pace { get; set; }

        [JsonProperty("nonce")]
        public byte[] Nonce { get; set; }

        [JsonProperty("select")]
        public byte[] Select { get; set; }
    }

    public class SelectResponse : BaseResponse
    {
        public string Authenticate { get; set; }
    }

    public class CommandResponse : BaseResponse
    {
        public string Authenticate { get; set; }
        public List<string> Commands { get; set; }
    }
}
