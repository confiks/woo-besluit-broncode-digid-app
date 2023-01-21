using System;
using System.Collections.Generic;
using DigiD.Common.Constants;

namespace DigiD.Common.Models
{
    public class QRScanResult
    {
        public string Identifier { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }

    public class SessionData
    {
        public string Identifier { get; set; }
        public string Host { get; set; }
        public string WebSessionId { get; set; }
        public string verification_code { get; set; }
        public string source { get; set; }
        public string action { get; set; }
    }

    public class App2AppSessionData : SessionData
    {
        public App2AppRequest Data { get; }

        public App2AppSessionData(App2AppRequest data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Host = data.Host;
            Identifier = QRCodeIdentifierConstants.Authentication;
        }
    }
}

