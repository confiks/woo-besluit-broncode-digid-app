using DigiD.Common.EID.Enums;

namespace DigiD.Common.EID.CardSteps.Randomize
{
    public class EidSessionData
    {
        public string SessionId { get; set; }
        public string ServerAddress { get; set; }
        public UserConsent Consent { get; set; }
    }
}
