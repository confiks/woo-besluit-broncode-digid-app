using DigiD.Common.Mobile.BaseClasses;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class AP099ViewModel : BaseViewModel
    {
        public AP099ViewModel()
        {
            PageId = "AP099";
            HasBackButton = true;
            ButtonCommand = new AsyncCommand(async () =>
            {
                if (!CanExecute)
                    return;

                CanExecute = false;
                DialogService.ShowProgressDialog();
                await DeactivationHelper.DeactivateApp();
                await NavigationService.ResetMainPage(new ActivationIntro1ViewModel(false));
                DialogService.HideProgressDialog();
            }, () => CanExecute);
        }
    }
}

