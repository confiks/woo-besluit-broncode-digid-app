using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using DigiD.Droid.Helpers;

namespace DigiD.Droid.Services
{
    [Service]
    public class ScheduleService : Service
    {
        public static readonly Lazy<AlarmReceiver> AlarmReceiver = new Lazy<AlarmReceiver>(() => new AlarmReceiver());

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            return StartCommandResult.Sticky;
        }
    }
}
