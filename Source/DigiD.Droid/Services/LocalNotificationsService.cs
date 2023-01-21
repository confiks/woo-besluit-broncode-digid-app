using System;
using System.Collections.Generic;
using Android.Content;
using DigiD.Common.Enums;
using DigiD.Common.Mobile.Interfaces;
using DigiD.Droid.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalNotificationsService))]
namespace DigiD.Droid.Services
{
    public class LocalNotification
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int Id { get; set; }
        public DateTimeOffset NotifyTime { get; set; }
    }

    public class LocalNotificationsService : ILocalNotifications
    {
        internal bool FromBoot;

        public void Cancel(int id)
        {
            ScheduleService.AlarmReceiver.Value.CancelAlarm(id);

            var notifications = Notifications;

            if (notifications.ContainsKey((NotificationType)id))
                notifications.Remove((NotificationType)id);

            Notifications = notifications;
        }

        public void Show(string title, string message, int id = 0)
        {
            Show(title, message, id, DateTime.UtcNow);
        }

        public void Show(string title, string message, int id, DateTimeOffset notifyTime)
        {
            ScheduleService.AlarmReceiver.Value.SetAlarm(new LocalNotification
            {
                Body = message,
                Id = id,
                NotifyTime = notifyTime,
                Title = title
            });

            if (FromBoot) 
                return;

            var notifications = Notifications;

            if (notifications.ContainsKey((NotificationType) id))
                notifications[(NotificationType) id] = notifyTime;
            else
                notifications.Add((NotificationType)id, notifyTime);

            Notifications = notifications;
        }

        public Dictionary<NotificationType, DateTimeOffset> Notifications
        {
            get
            {
                var preferences = Android.App.Application.Context.GetSharedPreferences("Notifications", FileCreationMode.Private);
                var notifications = new Dictionary<NotificationType, DateTimeOffset>();

                if (preferences.Contains(nameof(Notifications)))
                {
                    var value = preferences.GetString(nameof(Notifications), JsonConvert.SerializeObject(new Dictionary<NotificationType, DateTimeOffset>()));
                    if (!string.IsNullOrEmpty(value))
                        notifications = JsonConvert.DeserializeObject<Dictionary<NotificationType, DateTimeOffset>>(value);
                }

                return notifications;
            }
            set
            {
                var preferences = Android.App.Application.Context.GetSharedPreferences("Notifications", FileCreationMode.Private);
                preferences.Edit().PutString(nameof(Notifications), JsonConvert.SerializeObject(value)).Commit();
            }
        }
    }
}
