using DigiD.Common.Models.RequestModels;
using Newtonsoft.Json;

namespace DigiD.Common.Models
{
    public class InitForeignDocumentRequestModel : BaseRequest
    {
        [JsonProperty("document_type")]
        public string DocumentType { get; set; }
        [JsonProperty("document_number")]
        public string DocumentNumber { get; set; }
        [JsonProperty("date_of_birth")]
        public string DateOfBirth { get; set; }
        [JsonProperty("date_of_expiry")]
        public string DateOfExpiry { get; set; }
    }
}
