using System.Collections.Generic;
using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Helpers;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Settings;
using Microsoft.AppCenter;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using MenuItem = DigiD.Models.MenuItem;

namespace DigiD.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public bool PiwikEnabled { get; set; }

        private readonly IGeneralPreferences _generalSettings = DependencyService.Get<IGeneralPreferences>();

        public SettingsViewModel()
        {
            PageId = "AP068";
            HeaderText = AppResources.AppMenuSettings;
            PiwikEnabled = _generalSettings.PiwikTrackEnabled;
            HasBackButton = true;

            LoadItems();
        }

        public AsyncCommand<bool> SwitchChangedCommand => new AsyncCommand<bool>(async (value) =>
        {
            if (!value)
                PiwikHelper.Track("Preferences", "Piwik Analytics disabled", NavigationService.CurrentPageId);

            _generalSettings.PiwikTrackEnabled = value;

            await AppCenter.SetEnabledAsync(_generalSettings.PiwikTrackEnabled);
        });

        private void LoadItems()
        {
            MenuItems.Add(new MenuItem
            {
                Title = AppResources.Language,
                TargetViewModel = typeof(SettingsLanguageViewModel),
                Icon = "resource://DigiD.Common.Resources.icon_taal_selectie.svg?assembly=DigiD.Common"
            });

            MenuItems.Add(new MenuItem
            {
                Title = AppResources.AppThemeMenuText,
                TargetViewModel = typeof(SettingsAppThemeViewModel),
                Icon = "resource://DigiD.Resources.icon_instellingen_switch_theme.svg"
            });

            if (DependencyService.Get<IMobileSettings>().ActivationStatus != ActivationStatus.NotActivated)
            {
                MenuItems.Add(new MenuItem
                {
                    Title = AppResources.AppMenuDeactiveren,
                    Icon = "resource://DigiD.Resources.digid_icon_menu_app_deactiveren.svg",
                    TargetViewModel = typeof(DeactivationViewModel)
                });
            }
        }
    }
}
