using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class ConfirmRequest : BaseRequest
    {
        [JsonProperty("user_app_id")]
        public string AppId { get; set; }

        /// <summary>
        /// Indien de app in het koppelvlak aanvragen_gegevens_web_sessie
        /// een hashed_pip ontvangen heeft, stuurt de app hier de met de
        /// private key gemaakte handtekening van de hashed_pip terug naar DigiD Kern.
        /// Van toepassing bij webdiensten die een vi en vp verwachten (o.a. eIDAS-Uit).
        /// </summary>
        [JsonProperty("signature_of_pip")]
        public string SignatureOfPIP { get; set; }
    }
}
