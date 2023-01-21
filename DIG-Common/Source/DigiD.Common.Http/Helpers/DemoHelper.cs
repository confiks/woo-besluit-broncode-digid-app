using System;
using System.Collections.Generic;

namespace DigiD.Common.Http.Helpers
{
    public static class DemoHelper
    {
        public static Dictionary<string, DemoSession> Sessions { get; } = new Dictionary<string, DemoSession>();

        public static DemoSession GetSession(string sessionId)
        {
            return string.IsNullOrEmpty(sessionId) ? null : Sessions[sessionId];
        }
    }

    public class DemoSession
    {
        public string IV { get; set; }
        public string Action { get; set; }
        public string Challenge { get; set; }
        public DateTime? DateTime { get; set; }
        public bool IsAuthenticated { get; set; }
        public int LoginLevel { get; set; }
    }
}
