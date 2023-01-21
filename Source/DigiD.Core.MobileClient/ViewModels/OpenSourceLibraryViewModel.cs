using System.Collections.Generic;
using System.Linq;
using DigiD.Common;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Models;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class OpenSourceLibraryViewModel : BaseViewModel
    {
        public List<OpenSourceLibraryModel> Items { get; set; }

        public OpenSourceLibraryViewModel()
        {
            PageId = "AP085";
            Items = OpenSourceLibraryHelper.Libraries.OrderBy(x => x.Title).ToList();
            HasBackButton = true;
            NavCloseCommand = new AsyncCommand(async() => await NavigationService.PopToRoot());
            HeaderText = AppResources.OpenSourceLibraries;
        }
    }
}
