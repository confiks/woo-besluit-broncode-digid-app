using System.Threading.Tasks;

namespace DigiD.Common.Interfaces
{
    public interface IPushNotificationService
    {
        Task UnRegisterForRemoteNotifications();
        bool NotificationsEnabled();
        bool NotificationsAvailable { get; }
        Task<string> GetToken();
    }
}
