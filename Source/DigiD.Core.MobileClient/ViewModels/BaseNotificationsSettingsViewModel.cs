using System.Windows.Input;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Services;
using DigiD.Common.Settings;
using DigiD.UI.Popups;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class BaseNotificationsSettingsViewModel : BaseViewModel
    {
        private readonly bool _authShowPopupOnLoad;
        public ICommand OpenSettingsCommand { get; set; }
        public bool OpenSettingsCommandVisible { get; set; }

        private RemoteNotificationDisabledPopup _popup;

        public BaseNotificationsSettingsViewModel(bool authShowPopupOnLoad)
        {
            _authShowPopupOnLoad = authShowPopupOnLoad;
            OpenSettingsCommand = new Command(() =>
            {
                DependencyService.Get<IDevice>().OpenSettings();
            });

            if (Device.RuntimePlatform == Device.iOS) //Only subscribe for iOS, because OnAppearing will not be fired when app is resuming from background
            {
                MessagingCenter.Subscribe<App>(this, "OnResume", (a) =>
                {
                    var notificationsEnabled = DependencyService.Get<IPushNotificationService>().NotificationsEnabled();

                    if (notificationsEnabled && _popup.IsOpened)
                    {
                        _popup.Dismiss(null);
                    }

                    OpenSettingsCommandVisible = !notificationsEnabled && !_popup.IsOpened;
                });
            }
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            if (_popup == null)
                _popup = new RemoteNotificationDisabledPopup();

            var notificationsDisabled = !DependencyService.Get<IPushNotificationService>().NotificationsEnabled();

            if (_authShowPopupOnLoad && !DependencyService.Get<IPreferences>().M17PopupShown && notificationsDisabled && !_popup.IsOpened)
            {
                DependencyService.Get<IPreferences>().M17PopupShown = true;
                NavigationService.OpenPopup(_popup);
            }
            else if (!notificationsDisabled && _popup.IsOpened)
                _popup.Dismiss(null);

            OpenSettingsCommandVisible = notificationsDisabled && !_popup.IsOpened;
        }
    }
}
