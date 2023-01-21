using DigiD.Common.SessionModels;
using Newtonsoft.Json;

namespace DigiD.Common.Models.RequestModels
{
    public class BaseRequest 
    {
        [JsonProperty("auth_session_id")] public string AuthenticationSessionId { get; protected set; } = AppSession.AuthenticationSessionId;

        /// <summary>
        /// In geval van bevestiging van inloggen bij een
        /// webdienst, van inloggen in een afnemer app of
        /// van een Mijn DigiD actie: De app heeft naast het
        /// app_sessie_id ook een (nog te authenticeren)
        /// web_sessie_id (ontvangen via QR-code, via
        /// universal of app link, of als antwoord op het
        /// SAML_authenticatieverzoek bij app2app). De
        /// app vraagt de gebruiker om bevestiging van het
        /// inloggen en stuurt die bevestiging met beide
        /// sessie_id's door naar Kern.
        /// </summary>
        [JsonProperty("app_session_id")] public string AppSessionId { get; } = HttpSession.AppSessionId;
    }
}
