using System;

namespace DigiD.Common.SessionModels
{
    public class TempSessionData
    {
        public TempSessionData(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException(nameof(sessionId));

            WebSessionId = sessionId;
        }

        public void SwitchSession()
        {
            HttpSession.AppSessionId = WebSessionId;
        }

        public string WebSessionId { get; }
    }
}
