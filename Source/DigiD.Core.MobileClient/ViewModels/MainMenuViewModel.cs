#if READ_PHOTO
using DigiD.Common.SessionModels;
using DigiD.Common.Interfaces;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using DigiD.Common;
using DigiD.Common.Constants;
using DigiD.Common.Enums;
using DigiD.Common.Helpers;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Mobile.Helpers;
using DigiD.Common.Models;
using DigiD.Common.SessionModels;
using DigiD.Common.Settings;
using DigiD.Helpers;
using Xamarin.Forms;
using MenuItem = DigiD.Models.MenuItem;

namespace DigiD.ViewModels
{
    internal class MainMenuViewModel : BaseViewModel
    {
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        public Thickness MenuPadding => new Thickness(0, 0, OrientationHelper.IsInLandscapeMode ? DisplayHelper.Width / 2 : DisplayHelper.Width * .3, 0);

        public MainMenuViewModel() : base(true)
        {
            ShowDemoBar = false;

            PageId = "AP036";
            HeaderText = "Menu";

            AppSession.MenuItemChanged += UnreadMessagesChanged;

            LoadItems();
        }

        private void UnreadMessagesChanged(object sender, EventArgs e)
        {
            var menuItem = MenuItems.FirstOrDefault(x => x.Title.StartsWith(AppResources.AppMenuMessages));

            if (menuItem != null)
                ChangeIconAndA11YText(menuItem);
        }

        private static void ChangeIconAndA11YText(MenuItem menuItem)
        {
            var newMessages = AppSession.AccountStatus?.HasUnreadMessages == true;

            if (newMessages)
            {
                menuItem.Icon = "resource://DigiD.Resources.digid_icon_menu_berichten_nieuwe_bericht.svg";
                menuItem.AccessibilityText = $"{AppResources.AppMenuMessages}, {AppResources.NewNotificationsHeader}";
            }
            else
            {
                menuItem.Icon = "resource://DigiD.Resources.digid_icon_menu_berichten.svg";
                menuItem.AccessibilityText = AppResources.AppMenuMessages;
            }
        }

        private void LoadItems()
        {
            var items = new List<MenuItem>();

            var loginLevel = DependencyService.Get<IMobileSettings>().LoginLevel;

            if (DependencyService.Get<IMobileSettings>().ActivationStatus == ActivationStatus.NotActivated && (App.Configuration?.RequestStationEnabled ?? false))
            {
                items.Add(new MenuItem
                {
                    Title = AppResources.AppMenuActivateViaRequestStation,
                    Icon = "resource://DigiD.Resources.digid_icon_menu_balie.svg",
                    TargetViewModel = typeof(ConfirmViewModel),
                    Arguments = new object[] { new ConfirmModel(ConfirmActions.ActivateViaRequestStationStart), false }
                });
            }

            if (loginLevel == LoginLevel.Midden || loginLevel == LoginLevel.Substantieel)
            {
                var menuItem = new MenuItem
                {
                    Title = AppResources.AppMenuUpgradeToSub,
                    Icon = loginLevel == LoginLevel.Midden ? "resource://DigiD.Resources.digid_icon_document_check_alert.svg" : "resource://DigiD.Resources.digid_icon_document_check_ok.svg",
                    AccessibilityText = loginLevel == LoginLevel.Midden ?
                        $"{AppResources.AppMenuUpgradeToSub}, {AppResources.AppMenuUpgradeToSubNotPerformed}" :
                        $"{AppResources.AppMenuUpgradeToSub}, {AppResources.AppMenuUpgradeToSubPerformed}"
                };

                if (!App.HasNfc)
                {
                    menuItem.TargetViewModel = typeof(IdCheckNoNfcViewModel);
                }
                else
                {
                    menuItem.Action = async () => await SessionHelper.StartSession(async () =>
                    {
                        var model = new ConfirmModel(loginLevel == LoginLevel.Midden
                            ? ConfirmActions.IDcheckStartFromMenu
                            : ConfirmActions.UpgradeToSubWhileSub);
                        await NavigationService.PushAsync(new ConfirmViewModel(model, false));
                    });
                }

                items.Add(menuItem);
            }

            if (DependencyService.Get<IMobileSettings>().ActivationStatus == ActivationStatus.Activated)
            {
#if READ_PHOTO
                items.Add(new MenuItem
                {
                    Title = "Mijn Foto",
                    Icon = "resource://DigiD.Resources.digid_icon_menu_mijn_digid.svg",
                    Action = async () =>
                    {
                        string appId = DependencyService.Get<IMobileSettings>().AppId;
                        var response = await DependencyService.Get<Common.Interfaces.IGeneralServices>().GetMrzCodes(appId);

                        Common.EID.SessionModels.EIDSession.MrzDrivingLicense = response.MrzDrivingLicense.ToSecureString();
                        Common.EID.SessionModels.EIDSession.MrzIdentityCard = response.MrzIdentityCard.ToSecureString();

                        await NavigationService.PushAsync(new WidScannerViewModel(new NfcScannerModel
                        {
                            Action = PinEntryType.ReadPhoto,
                        }));
                    }
                });
#endif
                var item = new MenuItem
                {
                    Title = AppResources.AppMenuMessages,
                    Icon = AppSession.AccountStatus?.HasUnreadMessages == true ? "resource://DigiD.Resources.digid_icon_menu_berichten_nieuwe_bericht.svg" : "resource://DigiD.Resources.digid_icon_menu_berichten.svg",
                    Action = async () => await RemoteNotificationHelper.ConfirmShowNotifications(),
                };
                if (AppSession.AccountStatus?.HasUnreadMessages == true)
                    item.AccessibilityText = $"{AppResources.AppMenuMessages}, {AppResources.NewNotificationsHeader}";

                items.Add(item);
            }

            if (DependencyService.Get<IMobileSettings>().ActivationStatus == ActivationStatus.Activated)
            {
                items.Add(new MenuItem
                {
                    Title = AppResources.AppMenuMijnDigiD,
                    Icon = "resource://DigiD.Resources.digid_icon_menu_mijn_digid.svg",
                    TargetViewModel = typeof(AP106ViewModel)
                });
            }

            items.Add(new MenuItem
            {
                Title = AppResources.AppMenuSettings,
                Icon = "resource://DigiD.Resources.icon_menu_instellingen.svg",
                TargetViewModel = typeof(SettingsViewModel)
            });

            // Hulp & Info
            items.Add(new MenuItem
            {
                Title = AppResources.AppMenuHelpInfo,
                Icon = "resource://DigiD.Resources.icon_menu_faq.svg",
                TargetViewModel = typeof(HelpInfoViewModel)
            });

#if ENVIRONMENT_SELECTABLE
            items.Add(new MenuItem
            {
                Title = "Debug",
                Icon = "resource://DigiD.Resources.icon_menu_instellingen.svg",
                TargetViewModel = typeof(DebugSettingsViewModel)
            });
#endif

            MenuItems = items;
        }
    }
}
