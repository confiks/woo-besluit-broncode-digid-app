using DigiD.Common;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.NFC.Interfaces;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class AboutAppViewModel : BaseViewModel
    {
        public AboutAppViewModel()
        {
            PageId = "AP064";
            HeaderText = AppResources.AppMenuOverDeApp;
            HasBackButton = true;
            NavCloseCommand = new AsyncCommand(async () => await NavigationService.PopToRoot());
        }

        public bool IsNFCEnabled => DependencyService.Get<INfcService>().IsNFCEnabled;
    }
}
