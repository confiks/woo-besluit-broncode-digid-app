using System.Globalization;
using System.Threading;

namespace DigiD.Common
{
    public static class Localization
    {
        public static void Init(CultureInfo ci)
        {
            CultureInfo.DefaultThreadCurrentCulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;
            CultureInfo.CurrentUICulture = ci;
            CultureInfo.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = ci;

            AppResources.Culture = ci;
            MobileAppResources.Culture = ci;
        }
    }
}
