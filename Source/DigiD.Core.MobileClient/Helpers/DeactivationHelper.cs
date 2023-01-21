using System.Threading.Tasks;
using DigiD.Common.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.SessionModels;
using DigiD.Common.Settings;
using Xamarin.Forms;
using static DigiD.App;

namespace DigiD.Helpers
{
    internal static class DeactivationHelper
    {
        internal static async Task DeactivateApp()
        {
            if (DependencyService.Get<IMobileSettings>().ActivationStatus == ActivationStatus.Pending)
            {
                LocalNotificationHelper.CancelNotification(NotificationType.LetterFirst);
                LocalNotificationHelper.CancelNotification(NotificationType.LetterSecond);
            }

            if (DependencyService.Get<IMobileSettings>().LoginLevel == LoginLevel.Midden)
                LocalNotificationHelper.CancelNotification(NotificationType.IDcheck);

            var permissionSet = DependencyService.Get<IMobileSettings>().NotificationPermissionSet;

            if (permissionSet.HasValue && permissionSet.Value)
                await DependencyService.Get<IPushNotificationService>().UnRegisterForRemoteNotifications();

            AppSession.SetAccountStatus(null);
            AppSession.MenuItemChange();

            Reset();
        }
    }
}
