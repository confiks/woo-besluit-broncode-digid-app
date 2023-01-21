using System.Threading.Tasks;
using System.Windows.Input;
using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Services;
using DigiD.Common.Settings;
using DigiD.Helpers;
using DigiD.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class WidUpgradeStatusViewModel : BaseViewModel
    {
        private bool _init;
        public string ImageSource { get; set; }

        public WidUpgradeStatusViewModel() : base(preventLock: true)
        {
            PageId = "AP063";
            HeaderText = AppResources.RDAIntroHeader;
            FooterText = AppResources.SwitchScreenForUpdate;
            ImageSource = "resource://DigiD.Resources.afbeelding_schermswitch_digid_naar_idchecker.svg";
            NavCloseCommand = CancelCommand;

            DeviceLockService.SetTimer();
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();

            if (_init)
                return;

            _init = true;

            await Task.Delay(2000); //Temporary fix for issue which will be fixed on release 5.20 of DigiD kern
            var status = await DependencyService.Get<IRdaServices>().Verified();

            while (status == ApiResult.Pending)
            {
                await Task.Delay(1000);
                status = await DependencyService.Get<IRdaServices>().Verified();
            }

            switch (status)
            {
                case ApiResult.Ok:
                    if (DeviceInfo.Platform == DevicePlatform.Android &&
                        DeviceInfo.Version.Major >= 10)
                    {
                        await DependencyService.Get<IA11YService>().Speak(AppResources.UpgradeAccountWithIDCheckerSuccessHeader);
                    }
                    DependencyService.Get<IMobileSettings>().LoginLevel = LoginLevel.Substantieel;
                    DependencyService.Get<IMobileSettings>().Save();
                    LocalNotificationHelper.CancelNotification(NotificationType.IDcheck);
                    await NavigationService.ShowMessagePage(MessagePageType.UpgradeAccountWithIDCheckerSuccess);
                    break;
                case ApiResult.Cancelled:
                    await NavigationService.ShowMessagePage(MessagePageType.UpgradeAccountWithNFCCancelled);
                    break;
                default:
                    await NavigationService.ShowMessagePage(MessagePageType.UpgradeAccountWithNFCFailed);
                    break;
            }

            App.ClearSession();
        }

        public ICommand CancelCommand
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    CanExecute = false;

                    var stop = await DependencyService.Get<IAlertService>().DisplayAlert(
                        AppResources.LoginCancelConfirmHeader
                        , AppResources.LoginCancelConfirmMessage
                    , AppResources.Yes
                    , AppResources.No);

                    if (stop)
                    {
                        if (Device.Idiom != TargetIdiom.Desktop)
                            await App.CancelSession(true);

                        await DependencyService.Get<IEIDServices>().CancelAuthenticate();
                        await NavigationService.PopToRoot();
                    }
                    CanExecute = true;
                }, () => CanExecute);
            }
        }
    }
}
