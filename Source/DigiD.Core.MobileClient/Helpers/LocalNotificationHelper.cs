using System;
using System.Threading.Tasks;
using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.Interfaces;
using DigiD.Common.Services;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Helpers
{
    public static class LocalNotificationHelper
    {
        private static ILocalNotifications _service;
        public static ILocalNotifications LocalNotificationService
        {
            get => _service ??= DependencyService.Get<ILocalNotifications>();
            set => _service = value;
        }

        public static void CancelNotification(NotificationType type)
        {
            LocalNotificationService.Cancel((int)type);
        }

        public static async Task<bool> SetNotification(DateTimeOffset date, NotificationType type)
        {
            var title = string.Empty;
            var message = string.Empty;

            switch (type)
            {
                case NotificationType.LetterFirst:
                    title = AppResources.LetterNotificationHeader;
                    message = AppResources.FirstLetterNotificationMessage;
                    break;
                case NotificationType.LetterSecond:
                    title = AppResources.LetterNotificationHeader;
                    message = AppResources.SecondLetterNotificationMessage;
                    break;
                case NotificationType.IDcheck:
                    title = AppResources.IDCheckNotificationTitle;
                    message = AppResources.IDCheckNotificationMessage;
                    break;
            }

            if (await DependencyService.Get<IDevice>().AskForLocalNotificationPermission())
            {
                LocalNotificationService.Show(title, message, (int)type, date);
                return true;
            }

            return false;
        }
#pragma warning disable S3168 // "async" methods should not return "void"
        public static async void HandleIncomingNotification(NotificationType type)
#pragma warning restore S3168 // "async" methods should not return "void"
        {
            switch (type)
            {
                case NotificationType.LetterFirst:
                case NotificationType.LetterSecond:
                {
                    if (DependencyService.Get<IMobileSettings>().ActivationStatus == ActivationStatus.Pending)
                        await ActivationHelper.ContinueLetterActivation();
                    else
                        await DependencyService.Get<INavigationService>().PopToRoot();
                    break;
                }
                case NotificationType.IDcheck:
                {
                    if (DependencyService.Get<IMobileSettings>().LoginLevel == LoginLevel.Midden)
                        await RdaHelper.Init(false);
                    else
                        await DependencyService.Get<INavigationService>().PopToRoot();

                    break;
                }
            }
        }
    }
}
