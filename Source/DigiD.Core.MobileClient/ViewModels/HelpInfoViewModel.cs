using System.Collections.Generic;
using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Helpers;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Settings;
using DigiD.Models;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using MenuItem = DigiD.Models.MenuItem;

namespace DigiD.ViewModels
{
    public class HelpInfoViewModel : BaseViewModel
    {
        public List<MenuBlock> MenuBlocks { get; set; } = new List<MenuBlock>();

        public HelpInfoViewModel()
        {
            PageId = "AP092";
            HeaderText = AppResources.AppMenuHelpAndInfo;
            HasBackButton = true;

            LoadItems();
        }

        public void LoadItems()
        {
            MenuBlocks.Add
                (
                new MenuBlock(AppResources.AppMenuFirstGroup,
                    new List<MenuItem>
                    {
                        new MenuItem
                        {
                            Title = AppResources.AppMenuFAQ,
                            Icon = "resource://DigiD.Resources.icon_menu_faq.svg",
                            TargetViewModel = typeof(FaqViewModel)
                        },
                        new MenuItem
                        {
                            Title = AppResources.AppMenuContact,
                            Icon = "resource://DigiD.Resources.digid_icon_menu_contact.svg",
                            TargetViewModel = typeof(ContactViewModel)
                        },
                        new MenuItem
                        {
                            Title = AppResources.AppMenuOverDeApp,
                            Icon = "resource://DigiD.Resources.icon_menu_info_over_de_app.svg",
                            TargetViewModel = typeof(AboutAppViewModel)
                        },
                        new MenuItem
                        {
                            //Support
                            Title = AppResources.AppMenuSupport,
                            Icon = "resource://DigiD.Resources.digid_icon_menu_ondersteuning.svg",
                            TargetViewModel = typeof(AP116ViewModel)
                        }
                    })
                );

            MenuBlocks.Add(new MenuBlock(
                    AppResources.AppMenuSecondGroup,
                    new List<MenuItem>
                    {
                        new MenuItem
                        {
                            Title = AppResources.AppMenuBeoordeelDezeApp,
                            Icon = "resource://DigiD.Resources.digid_icon_menu_rate.svg",
                            Action = async () => { await RatingHelper.RequestRating(true); },
                            IsExternalLink = true
                        },
                        new MenuItem
                        {
                            Title = AppResources.AppMenuAccessibility,
                            Icon = "resource://DigiD.Common.Resources.digid_icon_menu_toegankelijkheid.svg?assembly=DigiD.Common",
                            TargetViewModel = typeof(AP117ViewModel)
                        },
                        new MenuItem
                        {
                            Title = AppResources.AppMenuPrivacyBepaling,
                            Icon = "resource://DigiD.Resources.icon_menu_externe_link.svg",
                            Action = async () =>
                            {
                                if (OpenWebsite.CanExecute("PrivacyPolicy"))
                                    await OpenWebsite.ExecuteAsync("PrivacyPolicy");
                            },
                            IsExternalLink = true
                        },
                        new MenuItem
                        {
                            Title = AppResources.OpenSourceLibraries,
                            Icon =
                                "resource://DigiD.Common.Resources.digid_icon_opensource_libraries.svg?assembly=DigiD.Common",
                            TargetViewModel = typeof(OpenSourceLibraryViewModel)
                        }
                    }
                    )
                );

            // Rating verwijderen indien app nog niet geactiveerd.
            if (DependencyService.Get<IMobileSettings>().ActivationStatus != ActivationStatus.Activated)
                MenuBlocks[1].MenuItems.RemoveAt(0);
        }

        public AsyncCommand<object> OpenWebsite
        {
            get
            {
                return new AsyncCommand<object>(async (url) =>
                {
                    if (!CanExecute)
                        return;

                    CanExecute = false;

                    switch (url.ToString())
                    {
                        case "FAQ":
                            await Launcher.OpenAsync(AppResources.FAQLink);
                            break;
                        case "TermsOfUse":
                            await Launcher.OpenAsync(AppResources.TermsOfUseLink);
                            break;
                        case "PrivacyPolicy":
                            await Launcher.OpenAsync(AppResources.PrivacyPolicyLink);
                            break;
                    }
                    CanExecute = true;

                }, (u) => CanExecute);
            }
        }
    }
}
