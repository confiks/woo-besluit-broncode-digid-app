using Android.App;
using Android.Util;
using DigiD.Helpers;
using Firebase.Messaging;

namespace DigiD.Droid.Services
{
    [Service(Exported = false)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseNotificationService : FirebaseMessagingService
    {
        private const string TAG = "FirebaseNotificationService";

        public override async void OnMessageReceived(RemoteMessage p0)
        {
            Log.Debug(TAG, "From: " + p0.From);

            if (p0.GetNotification() == null) return;

            // These is how most messages will be received
            Log.Debug(TAG, "Notification Message Body: " + p0.GetNotification().Body);
            await RemoteNotificationHelper.ConfirmShowNotifications(true);
        }

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);

            if (MainActivity.IsInitialized)
                RemoteNotificationHelper.SetToken(token);
            else
                App.NewNotificationToken = token;
        }
    }
}
