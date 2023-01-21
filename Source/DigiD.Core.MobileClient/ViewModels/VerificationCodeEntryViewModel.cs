using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.SessionModels;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class VerificationCodeEntryViewModel : BaseViewModel
    {
        public bool IsValid => VerificationCode.Length == 4;
        public VerificationCodeEntryViewModel()
        {
            HasBackButton = true;

            PageId = "AP052";
            VerificationCode = string.Empty;
            HeaderText = AppResources.AP82Header;
            FooterText = AppResources.AP82Footer;
            NavCloseCommand = CloseCommand;
        }

        public string VerificationCode { get; set; }

        public AsyncCommand StartCommand
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    CanExecute = false;

                    if (!await DependencyService.Get<IGeneralServices>().SslPinningCheck())
                    {
                        await NavigationService.ShowMessagePage(MessagePageType.SSLException);
                        return;
                    }

                    App.RegisterServices(VerificationCode == "DEMO" ? AppMode.Demo : AppMode.Normal);

                    DialogService.ShowProgressDialog();

                    var sessionResponse =
                        await DependencyService.Get<IEnrollmentServices>().InitSessionActivationByApp();

                    if (sessionResponse.ApiResult == ApiResult.Ok)
                    {
                        sessionResponse.VerificationCode = VerificationCode;
                        AppSession.Process = Process.AppActivationWithApp;

                        HttpSession.ActivationSessionData = new ActivationSessionData
                        {
                            ActivationMethod = ActivationMethod.App,
                        };

                        await NavigationService.PushAsync(new ActivationByAppViewModel(sessionResponse));
                    }
                    else
                    {
                        await NavigationService.ShowMessagePage(MessagePageType.ErrorOccoured);
                    }

                    DialogService.HideProgressDialog();
                    CanExecute = true;
                }, () => CanExecute);
            }
        }

        public AsyncCommand CloseCommand
        {
            get
            {
                return new AsyncCommand(async () =>
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
        }

        public override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}
