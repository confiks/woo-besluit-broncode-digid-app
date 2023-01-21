using System;
using DigiD.Common.Models.ResponseModels;

namespace DigiD.Common.SessionModels
{
    public class RdaSession
    {
        public RdaSession(RdaSessionResponse response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            Url = response.Url;
            SessionId = response.SessionId;
        }

        public string Url { get; }
        public string SessionId { get; }
    }
}
