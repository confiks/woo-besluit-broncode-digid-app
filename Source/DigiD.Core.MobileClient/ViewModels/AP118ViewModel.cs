using DigiD.Common;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Models;
using DigiD.Common.Services;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class BaseTwoFactorViewModel : BaseViewModel
    {
        public bool TwoFactorEnabled { get; set; } = true;
        private bool _undo;

        public AsyncCommand<bool> TwoFactorChangedCommand => new AsyncCommand<bool>(async value =>
        {
            if (_undo)
                return;

            if (!value)
            {
                var result = await DependencyService.Get<IAlertService>().DisplayAlert(
                    AppResources.M18_Header
                    , AppResources.M18_Message
                    , AppResources.Yes
                    , AppResources.No, true);

                if (!result)
                {
                    _undo = true;
                    TwoFactorEnabled = true;
                    _undo = false;

                    return;
                }
            }

            await DependencyService.Get<IAccountInformationServices>().SetTwoFactor(TwoFactorEnabled);
        });
    }

    public class AP118ViewModel : BaseTwoFactorViewModel
    {
        private readonly RegisterEmailModel _model;

        public bool NotificationsEnabled { get; set; } = true;
        public bool TwoFactorVisible { get; }
        public bool PushNotificationsVisible { get; }
        
        public AP118ViewModel(bool twoFactorEnabled, RegisterEmailModel model)
        {
            PageId = "AP118";
            
            _model = model;
            ButtonCommand = NextCommand;
            TwoFactorVisible = !twoFactorEnabled;
            PushNotificationsVisible = DependencyService.Get<IPushNotificationService>().NotificationsAvailable;
        }

        private AsyncCommand NextCommand => new AsyncCommand(async () =>
        {
            if (PushNotificationsVisible)
                await DependencyService.Get<IAccountInformationServices>().SetTwoFactor(TwoFactorEnabled);

            await RemoteNotificationHelper.RequestToken(NotificationsEnabled);
            await ActivationHelper.RegisterEmailTaskAsync(_model);
        });
    }
}
