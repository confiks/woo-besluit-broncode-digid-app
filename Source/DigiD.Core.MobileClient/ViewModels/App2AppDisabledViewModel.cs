using DigiD.Common;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class App2AppDisabledViewModel : BaseViewModel
    {
        public App2AppDisabledViewModel()
        {
            PageId = "AP225";
            HeaderText = AppResources.AppDisabledHeader;
            ButtonText = AppResources.BackToApp;

            ButtonCommand = new AsyncCommand(async () =>
            {
                await App2AppHelper.ReturnToClientApp();
            });
        }
    }
}
