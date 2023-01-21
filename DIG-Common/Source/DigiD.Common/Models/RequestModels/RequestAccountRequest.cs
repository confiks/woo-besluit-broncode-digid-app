using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class RequestAccountRequest : BaseRequest
    {
        [JsonProperty("BSN")]
        public string Bsn { get; set; }

        [JsonProperty("date_of_birth")] // Format: "JJJMMDD"
        public string DateOfBirth { get; set; }

        [JsonProperty("postal_code")]  //Format: 9999AA
        public string Postalcode { get; set; }

        [JsonProperty("house_number")]
        public string HouseNumber { get; set; }

        [JsonProperty("house_number_additions")]
        public string HouseNumberSuffix { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("nfc_support")]
        public bool HasNfcSupport { get; set; }
    }
}
