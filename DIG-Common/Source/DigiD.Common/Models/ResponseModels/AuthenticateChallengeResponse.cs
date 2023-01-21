using System;
using Newtonsoft.Json;

namespace DigiD.Common.Models.ResponseModels
{
    public class AuthenticateChallengeResponse : AuthenticationSessionResponse
    {
        [JsonProperty("challenge")]
        public string Challenge { get; set; }

        [JsonProperty("iv")]
        public string IV { get; set; }

        [JsonProperty("authentication_level")]
        public int AuthenticationLevel { get; set; }

        [JsonProperty("new_authentication_level")]
        public int NewAuthenticationLevel { get; set; }

        [JsonProperty("new_level_start_date")] 
        public DateTimeOffset? NewAuthenticationLevelStartDate { get; set; }

        [JsonProperty("hashed_pip")]
        public string HashedPIP { get; set; }

        public bool IsWebservice => !string.IsNullOrEmpty(WebService);
        public bool IsConfirmAction => !string.IsNullOrEmpty(Action);
    }
}
