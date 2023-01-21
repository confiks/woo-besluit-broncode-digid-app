using System;
using DigiD.Common.Mobile.Interfaces;
using DigiD.iOS.Services;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalNotificationsService))]
namespace DigiD.iOS.Services
{
    public class LocalNotificationsService : ILocalNotifications
    {
        public void Cancel(int id)
        {
            foreach (var notification in UIApplication.SharedApplication.ScheduledLocalNotifications)
            {
                if (notification.AlertAction == id.ToString())
                    UIApplication.SharedApplication.CancelLocalNotification(notification);
            }
        }

        public void Show(string title, string message, int id, DateTimeOffset notifyTime)
        {
            var notification = new UILocalNotification
            {
                FireDate = NSDate.FromTimeIntervalSince1970(notifyTime.ToUnixTimeSeconds()),
                AlertBody = message,
                AlertTitle = title,
                AlertAction = id.ToString(),
                SoundName = UILocalNotification.DefaultSoundName
            };

            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }
    }
}
