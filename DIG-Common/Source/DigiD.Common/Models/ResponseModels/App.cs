using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class App
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
