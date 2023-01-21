using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Settings;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class AP116ViewModel : BaseViewModel
    {
        public string AppVersionNumber => $"{AppInfo.VersionString} ({AppInfo.BuildString})";
        public string AppCode => $"{DependencyService.Get<IGeneralPreferences>().InstanceId[..6].ToUpperInvariant()}";
        public string PlatformVersion => $"{Device.RuntimePlatform} {DeviceInfo.VersionString}";

        public string SupportCode
        {
            get
            {
                var vi = AppInfo.Version;
                var dv = DeviceInfo.Version;
                var osVersion = $"{dv.Major,3}{dv.Minor,1}{(dv.Build > 0 ? $"{dv.Build,2}" : "00")}";
                var tmp = $"-{osVersion.Substring(0, 4)}-{osVersion.Substring(4)}{vi.Major,2}-{vi.Minor,2}{(vi.Build > 0 ? $"{vi.Build,2}": "00")}-{AppInfo.BuildString,4}";
                tmp = tmp.Replace(' ', '0');    // eerst de spaties vervangen door voorloopnullen indien nodig.
                
                return SupportCodeHelper.GenerateSupportCode() + tmp.Replace("-", " - "); // tussen de nibbles van 4 bits een spatie rondom het streepje.
            }
        }

        public AP116ViewModel()
        {
            HasBackButton = true;
            PageId = "AP116";

            NavCloseCommand = new AsyncCommand(async () => { await NavigationService.PopToRoot(); });
        }
    }
}
