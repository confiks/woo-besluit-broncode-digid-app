using System.Threading.Tasks;
using DigiD.Common.Enums;

namespace DigiD.Common.Services
{
    public interface IDevice
    {
        int PiwikId { get; }
        bool HasHomeButton { get; }
        string RuntimePlatform { get; }
        string DeviceTypeName { get; }
        string DefaultLanguage { get; }
        bool IsScreenCaptured { get; }
        bool IsInDeveloperMode { get; }
        SystemFontSize GetSystemFontSize();

        void OpenSettings();
        bool OpenBrowser(string name, string uri);
        Task<string> UserAgent();
        void CloseApp();
        Task<bool> AskForLocalNotificationPermission();
    }
}
