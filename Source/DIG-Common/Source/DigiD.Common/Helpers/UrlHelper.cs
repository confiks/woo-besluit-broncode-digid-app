using System;

namespace DigiD.Common.Helpers
{
    public static class UrlHelper
    {
        public static bool IsUrlLinkToMyDigid(this string url)
        {
            return (url.StartsWith("https://mijn.", StringComparison.CurrentCultureIgnoreCase)
                && url.EndsWith("digid.nl", StringComparison.CurrentCultureIgnoreCase))
                || url.Equals("https://digid.nl/inloggen", StringComparison.CurrentCultureIgnoreCase)
                || url.Equals("https://digid.nl/en/inloggen", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
