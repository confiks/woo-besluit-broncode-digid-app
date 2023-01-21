using System.Net.Http;
using Android.App;
using Android.Content.PM;
using Android.OS;
using DigiD.Common.Services;
using DigiD.Droid.Helpers;
using DigiD.Droid.Services;
using Xamarin.Essentials;

[assembly: Xamarin.Forms.Dependency(typeof(SecurityService))]
namespace DigiD.Droid.Services
{
    public class SecurityService : ISecurityService
    {
        public bool IsDebuggerAttached => Debug.IsDebuggerConnected
                                          || Application.Context.ApplicationInfo != null && Application.Context.ApplicationInfo.Flags.HasFlag(ApplicationInfoFlags.Debuggable)
                                          || System.Diagnostics.Debugger.IsAttached;

        public bool IsRunningOnVirtualDevice => DeviceInfo.DeviceType != DeviceType.Physical;
        public HttpMessageHandler GetHttpMessageHandler()
        {
            return new TestAndroidClientHandler();
        }
    }
}
