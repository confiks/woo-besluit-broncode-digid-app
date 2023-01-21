using DigiD.Common;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.SessionModels;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class ActivationIntro1ViewModel : BaseViewModel
    {
        public ActivationIntro1ViewModel(bool hasBackButton = true)
        {
            PageId = "AP049";
            HasBackButton = hasBackButton;
            HeaderText = AppResources.ActivationHeader;

            ButtonCommand = new AsyncCommand(async () =>
            {
                await NavigationService.PushAsync(new ActivationIntro2ViewModel());
            });

            NavCloseCommand = new AsyncCommand(async () =>
            {
                if (HttpSession.TempSessionData != null)
                {
                    HttpSession.TempSessionData.SwitchSession();
                    HttpSession.TempSessionData = null;
                    await DependencyService.Get<IGeneralServices>().Cancel(true);
                }

                await App.CancelSession(true, async () => await NavigationService.PopToRoot(true));
            });
        }

        public override bool OnBackButtonPressed()
        {
            return true;
        }

        public AsyncCommand CancelCommand
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    await NavigationService.PushAsync(new ActivationLoginViewModel());
                });
            }
        }
    }
}
