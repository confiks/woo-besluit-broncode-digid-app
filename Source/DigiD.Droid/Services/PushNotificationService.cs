using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Extensions;
using Android.OS;
using AndroidX.Core.App;
using DigiD.Common.Interfaces;
using DigiD.Droid.Services;
using Firebase.Messaging;
using Xamarin.Forms;

[assembly: Dependency(typeof(PushNotificationService))]
namespace DigiD.Droid.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        public async Task UnRegisterForRemoteNotifications()
        {
            try
            {
                await FirebaseMessaging.Instance.DeleteToken();
            }
            catch (Exception)
            {
                //Always ignore exception
            }
            
        }

        public bool NotificationsEnabled()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var nm = (NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService);

                if (!nm.AreNotificationsEnabled())
                    return false;

                return nm.NotificationChannels == null || nm.NotificationChannels.All(channel => channel.Importance != NotificationImportance.None);
            }

            return NotificationManagerCompat.From(Android.App.Application.Context).AreNotificationsEnabled();
        }

        public bool NotificationsAvailable
        {
            get
            {
                var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(Xamarin.Essentials.Platform.CurrentActivity);
                
                if (resultCode == ConnectionResult.Success) 
                    return true;

                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    // User can fix it.
                    var dict = new Dictionary<string, string>
                    {
                        { "resultCode", resultCode.ToString() },
                        { "errorString", GoogleApiAvailability.Instance.GetErrorString(resultCode) }
                    };

                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("google_play_service", dict);
                }
                else
                {
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("google_play_service_not_supported");
                }
                return false;

            }
        }

        public async Task<string> GetToken()
        {
            try
            {
                var token = await FirebaseMessaging.Instance.GetToken();
                return token.ToString();
            }
            catch
            {
                Console.WriteLine("FireBase not initialized");
                return null;
            }
        }
    }
}
