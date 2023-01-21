using System;
using System.Threading.Tasks;
using DigiD.Common.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Models.ResponseModels;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Common.SessionModels
{
    public static class AppSession
    {
        public static Process Process { get; set; }
        public static AppMode AppMode { get; set; }
        public static string AuthenticationSessionId { get; set; }
        public static Func<Task> ManagementAction { get; set; }
        public static AccountStatusResponse AccountStatus { get; private set; } = new AccountStatusResponse();
        public static string MyDigiDUrl { get; set; }
        public static bool IsAppActivated { get; set; } 

        public static void SetAccountStatus(AccountStatusResponse response)
        {
            AccountStatus = response;

            if (response == null)
                return;

            if (!response.TwoFactorEnabled)
                AccountStatus.OpenTasks.Add(AccountTask.TwoFactor);
            if (response.EmailStatus == EmailStatus.None)
                AccountStatus.OpenTasks.Add(AccountTask.Email);
            if (DependencyService.Get<IMobileSettings>().LoginLevel < LoginLevel.Substantieel)
                AccountStatus.OpenTasks.Add(AccountTask.RDA);
            if (!DependencyService.Get<IPushNotificationService>().NotificationsEnabled())
                AccountStatus.OpenTasks.Add(AccountTask.Notification);
        }

        public static event EventHandler MenuItemChanged;

        public static void MenuItemChange()
        {
            MenuItemChanged?.Invoke(null, EventArgs.Empty);
        }

        public static void Reset()
        {
            Process = Process.NotSet;
        }
    }
}
