#if ENVIRONMENT_SELECTABLE
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DigiD.Common.Constants;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Settings;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class DebugSettingsViewModel : BaseViewModel
    {

        public List<string> AvailableHosts { get; set; }
        public string SelectedHost { get; set; }
        public AsyncCommand WhatsNewCommand => new AsyncCommand(async () => await WhatsNewHelper.Show());

        public DebugSettingsViewModel()
        {
            HasBackButton = true;
            PageId = "DBGS";
            HasBackButton = true;

            AvailableHosts = AppConfigConstants.HostWhiteList.Select(h => h.ToString()).OrderBy(x => x).ToList();
            SelectedHost = DependencyService.Get<IGeneralPreferences>().SelectedHost;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(SelectedHost))
            {
                DependencyService.Get<IGeneralPreferences>().SelectedHost = SelectedHost;
            }
        }
    }
}
#endif
