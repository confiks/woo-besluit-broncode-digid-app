using DigiD.Common;
using DigiD.Common.Mobile.BaseClasses;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;

namespace DigiD.ViewModels
{
    public class MoreLoginInformationViewModel : BaseViewModel
    {
        public AsyncCommand GotoVideo => new AsyncCommand(async () => await Launcher.OpenAsync(AppResources.MoreLoginInformationMessageLink));

        public MoreLoginInformationViewModel()
        {
            PageId = "AP019";
            HasBackButton = false;
            HeaderText = AppResources.MoreLoginInformationHeader;
            ButtonCommand = new AsyncCommand(async () =>
            {
                CanExecute = false;
                await NavigationService.PopCurrentModalPage();
                CanExecute = true;
            }, () => CanExecute);

            NavCloseCommand = new AsyncCommand(async () =>
            {
                await NavigationService.PopToRoot();
            });
        }

        public override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}
