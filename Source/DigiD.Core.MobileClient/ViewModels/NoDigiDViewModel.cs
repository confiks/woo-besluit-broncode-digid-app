using DigiD.Common;
using DigiD.Common.Mobile.BaseClasses;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class NoDigiDViewModel : BaseViewModel
    {
        public NoDigiDViewModel()
        {
            PageId = "AP046";
            HeaderText = AppResources.NoDigiD;
            HasBackButton = true;

            CancelCommand = new AsyncCommand(async () =>
            {
                CanExecute = false;
                await NavigationService.PushAsync(new AP082ViewModel());
                CanExecute = true;
            }, () => CanExecute);

            ButtonCommand = new AsyncCommand(async () =>
            {
                CanExecute = false;
                await NavigationService.PushAsync(new AP079ViewModel());
                CanExecute = true;
            }, () => CanExecute);

            NavCloseCommand = new AsyncCommand(async () => await NavigationService.PopToRoot());
        }

        public AsyncCommand CancelCommand { get; }

        public override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}
