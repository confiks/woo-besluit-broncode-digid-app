using System;
using System.Threading.Tasks;
using DigiD.Common.Enums;

namespace DigiD.Helpers
{
    public static class LetterNotificationHelper
    {
#if PROD || PREPROD
        public static readonly TimeSpan FirstNotificationAfter = TimeSpan.FromDays(4);
        public static readonly TimeSpan SecondNotificationAfter = TimeSpan.FromDays(15);
#else
        public static readonly TimeSpan FirstNotificationAfter = TimeSpan.FromMinutes(2);
        public static readonly TimeSpan SecondNotificationAfter = TimeSpan.FromMinutes(4);
#endif

        internal static async Task<bool> CreateLetterNotification()
        {
            LocalNotificationHelper.LocalNotificationService.Cancel((int)NotificationType.LetterFirst);
            LocalNotificationHelper.LocalNotificationService.Cancel((int)NotificationType.LetterSecond);

            var now = DateTimeOffset.UtcNow;

#if PROD || PREPROD
            now = now.Date.AddHours(19);
#endif

            var result = await LocalNotificationHelper.SetNotification(now.Add(FirstNotificationAfter), NotificationType.LetterFirst);

            if (result)
                result = await LocalNotificationHelper.SetNotification(now.Add(SecondNotificationAfter), NotificationType.LetterSecond);

            return result;
        }

        internal static void ResetNotifications()
        {
            LocalNotificationHelper.CancelNotification(NotificationType.LetterFirst);
            LocalNotificationHelper.CancelNotification(NotificationType.LetterSecond);
        }
    }
}
