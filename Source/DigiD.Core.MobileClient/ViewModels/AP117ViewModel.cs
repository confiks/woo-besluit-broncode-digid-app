using System.Collections.Generic;
using DigiD.Common;
using DigiD.Common.Mobile.BaseClasses;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using MenuItem = DigiD.Models.MenuItem;

namespace DigiD.ViewModels
{
    public class AP117ViewModel : BaseViewModel
    {
        public List<MenuItem> MenuItems { get; set; }

        public AP117ViewModel()
        {
            PageId = "AP117";
            NavCloseCommand = new AsyncCommand(async () => { await NavigationService.PopToRoot(); });
            HeaderText = AppResources.AP117_Header;
            HasBackButton = true;

            MenuItems = new List<MenuItem>
            {
                new MenuItem
                {
                    Title = AppResources.AP117_MenuItem1_Name,
                    Icon = "resource://DigiD.Resources.icon_menu_externe_link.svg",
                    Action = async () => { await Browser.OpenAsync(AppResources.AP117_MenuItem1_Value); },
                    IsExternalLink = true
                },
                new MenuItem
                {
                    Title = AppResources.AP117_MenuItem2_Name,
                    Icon = "resource://DigiD.Resources.icon_menu_externe_link.svg",
                    Action = async () => { await Browser.OpenAsync(AppResources.AP117_MenuItem2_Value); },
                    IsExternalLink = true
                }
            };
        }
    }
}
