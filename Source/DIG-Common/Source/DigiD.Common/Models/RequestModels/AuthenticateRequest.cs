using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class AuthenticateRequest : BaseRequest
	{
        [JsonProperty("user_app_id")]
		public string AppId {get;set;}

        [JsonProperty("app_public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("signed_challenge")]
        public string Signature { get; set; }

        [JsonProperty("masked_pincode")]
        public string PIN { get; set; }

        [JsonProperty("upgrade_app")]
        public bool UpgradeApp { get; set; }

        [JsonProperty("signature_of_pip")]
        public string SignatureOfPIP { get; set; }
    }
}

