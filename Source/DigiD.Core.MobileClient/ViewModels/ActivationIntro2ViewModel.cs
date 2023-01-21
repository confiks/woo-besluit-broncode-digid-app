using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.SessionModels;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class ActivationIntro2ViewModel : BaseViewModel
    {
        public string ImageSource { get; set; }
        public string CancelText { get; set; }

        public AsyncCommand CancelCommand { get; set; }
        public AsyncCommand CloseCommand { get; set; }

        public ActivationIntro2ViewModel()
        {
            HasBackButton = true;
            PageId = "AP051";

            ButtonCommand = new AsyncCommand(async () =>
            {
                await NavigationService.PushAsync(new VerificationCodeEntryViewModel());
            });

            CancelCommand = new AsyncCommand(async () =>
            {
                await NavigationService.PushAsync(new ActivationLoginViewModel());
            });

            CloseCommand = new AsyncCommand(async () =>
            {
                if (HttpSession.TempSessionData != null)
                {
                    HttpSession.TempSessionData.SwitchSession();
                    HttpSession.TempSessionData = null;
                    await DependencyService.Get<IGeneralServices>().Cancel(true);
                }

                await App.CancelSession(true, async () => await NavigationService.PopToRoot(true));
            });
            NavCloseCommand = CloseCommand;
        }

        public override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}

