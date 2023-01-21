using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using DigiD.Common;
using DigiD.Common.Enums;
using DigiD.Droid.Services;
using DigiD.Helpers;

namespace DigiD.Droid.Helpers
{
    [BroadcastReceiver(Enabled = true, Exported = false, DirectBootAware = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted})]
    public class BootCompletedReceiver : BroadcastReceiver
    {
        public override async void OnReceive(Context context, Intent intent)
        {
            System.Diagnostics.Debug.WriteLine("==> DigiD - BootCompletedReceiver OnReceive");

            if (intent.Action != Intent.ActionBootCompleted) 
                return;

            context.StopService(new Intent(context, typeof(ScheduleService)));
            var preferences = Android.App.Application.Context.GetSharedPreferences("Notifications", FileCreationMode.Private);

            var language = "nl";
            if (preferences.Contains(nameof(GeneralPreferences.Language)))
                language = preferences.GetString(nameof(GeneralPreferences.Language), "nl");

            Localization.Init(CultureInfo.GetCultureInfo(language));

            var service = new LocalNotificationsService {FromBoot = true};
            LocalNotificationHelper.LocalNotificationService = service;

            await InitializeLocalNotificationsAfterReboot(service.Notifications);
        }

        public static async Task InitializeLocalNotificationsAfterReboot(Dictionary<NotificationType, DateTimeOffset> notifications)
        {
            System.Diagnostics.Debug.WriteLine($"==> DigiD - Now: {DateTimeOffset.UtcNow}");
            
            foreach (var (notificationType, date) in notifications)
            {
                if (date > DateTimeOffset.UtcNow)
                {
                    System.Diagnostics.Debug.WriteLine($"==> DigiD - Rescheduled() - {notificationType}: {date}");
                    await LocalNotificationHelper.SetNotification(date, notificationType);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"==> DigiD - Cancelled() - {notificationType}: {date}");
                    LocalNotificationHelper.CancelNotification(notificationType);
                }
            }
        }
    }
}
