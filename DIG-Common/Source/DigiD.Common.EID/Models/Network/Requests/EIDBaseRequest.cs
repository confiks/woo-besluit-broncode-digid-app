using Newtonsoft.Json;

namespace DigiD.Common.EID.Models.Network.Requests
{
    internal class EIDBaseRequest
    {
        [JsonProperty("header")]
        internal Header Header { get; private set; }

        [JsonProperty("messageData")]
        internal object MessageData { get; private set; }

        internal EIDBaseRequest(object data, string sessionId)
        {
            Header = new Header
            {
                SessionId = sessionId,
                SupportedAPIVersion = new SupportedApiVersion
                {
                    Major = "1",
                    Minor = "1"
                }
            };

            MessageData = data;
        }
    }
}
