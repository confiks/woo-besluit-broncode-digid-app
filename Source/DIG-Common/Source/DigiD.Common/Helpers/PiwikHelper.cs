using System.Threading.Tasks;
using DigiD.Common.Constants;
using DigiD.Common.Services.Piwik;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Common.Helpers
{
    public static class PiwikHelper
    {
        private static bool _init;
        public static async Task Init()
        {
            if (_init)
                return;

            _init = true;

            await PiwikTracker.Configure(AppConfigConstants.PiwikBaseUrl.ToString());
        }

        public static void TrackView(string viewName, string viewModel)
        {
            if (DependencyService.Get<IGeneralPreferences>().PiwikTrackEnabled)
            {
                PiwikTracker.Track(viewName, viewModel);
            }
        }

        public static void Track(string category, string action, string name)
        {
            if (DependencyService.Get<IGeneralPreferences>().PiwikTrackEnabled)
            {
                PiwikTracker.Track(category, action, name);
            }
        }
    }
}
