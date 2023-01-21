using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DigiD.Common.BaseClasses;
using DigiD.Common.Constants;
using DigiD.Common.Enums;
using DigiD.Common.Helpers;
using DigiD.Common.SessionModels;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Services
{
    public static class DeviceLockService
    {
        public static event EventHandler<EventArgs> DeviceLocked;
        private static DateTimeOffset? _lastKeyPress;

        private static readonly Timer Timer = new Timer(TimeSpan.FromSeconds(1), async () =>
        {
            if (_lastKeyPress == null)
                return;

            if (DateTimeOffset.UtcNow - _lastKeyPress.Value > AppConfigConstants.DisplayLockTimeout)
            {
                LockApp();
            }

            await Task.Delay(0);
        });

        static DeviceLockService()
        {
            Timer.Start();
        }

        public static void LockApp()
        {
            AppSession.AuthenticationSessionId = null;
            _lastKeyPress = null;
            DeviceLocked?.Invoke(null, EventArgs.Empty);
            Debug.WriteLine($"DeviceLockService elapsed - {DateTime.Now}");
        }

        public static void SetTimer()
        {
            if (DependencyService.Get<IMobileSettings>().ActivationStatus == ActivationStatus.NotActivated)
            {
                _lastKeyPress = null;
                return;
            }

            if (Application.Current.MainPage is CustomNavigationPage navPage && navPage.CurrentPage.BindingContext is CommonBaseViewModel {PreventLock: true})
            {
                _lastKeyPress = null;
                return;
            }
            
            _lastKeyPress = DateTimeOffset.Now;
            Debug.WriteLine($"DeviceLockService reset - {_lastKeyPress}");
        }
    }
}
