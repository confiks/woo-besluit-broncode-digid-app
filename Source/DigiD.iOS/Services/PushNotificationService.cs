using System.Diagnostics;
using System.Threading.Tasks;
using DigiD.Common.Interfaces;
using DigiD.Common.Settings;
using DigiD.iOS.Services;
using UIKit;
using UserNotifications;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(PushNotificationService))]
namespace DigiD.iOS.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        public Task UnRegisterForRemoteNotifications()
        {
            UIApplication.SharedApplication.UnregisterForRemoteNotifications();
            return Task.CompletedTask;
        }

        public bool NotificationsEnabled()
        {
            var types = UIApplication.SharedApplication.CurrentUserNotificationSettings.Types;
            return types.HasFlag(UIUserNotificationType.Alert);
        }

        public bool NotificationsAvailable => DeviceInfo.DeviceType == DeviceType.Physical;

        public async Task<string> GetToken()
        {
            var granted = await RegisterForRemoteNotifications();

            if (granted)
            {
                var token = DependencyService.Get<IMobileSettings>().NotificationToken;

                var sw = Stopwatch.StartNew();

                while (string.IsNullOrEmpty(token) && sw.Elapsed.TotalSeconds < 5)
                {
                    System.Console.WriteLine("Token not found");
                    token = DependencyService.Get<IMobileSettings>().NotificationToken;
                    await Task.Delay(200);
                }
            }

            return DependencyService.Get<IMobileSettings>().NotificationToken;
        }

        private static Task<bool> RegisterForRemoteNotifications()
        {
            var tcs = new TaskCompletionSource<bool>();
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert |
                                                                  UNAuthorizationOptions.Badge |
                                                                  UNAuthorizationOptions.Sound,
                (granted, error) =>
                {
                    if (granted)
                        Device.BeginInvokeOnMainThread(UIApplication.SharedApplication.RegisterForRemoteNotifications);

                    tcs.SetResult(granted);
                });

            return tcs.Task;
        }
    }
}
