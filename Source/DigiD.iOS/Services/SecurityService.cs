using System.Net.Http;
using DigiD.Common.Services;
using DigiD.iOS.Services;
using Xamarin.Essentials;

[assembly: Xamarin.Forms.Dependency(typeof(SecurityService))]
namespace DigiD.iOS.Services
{
    public class SecurityService : ISecurityService
    {
        public bool IsDebuggerAttached => System.Diagnostics.Debugger.IsAttached;

        public bool IsRunningOnVirtualDevice => DeviceInfo.DeviceType != DeviceType.Physical;
        public HttpMessageHandler GetHttpMessageHandler()
        {
            return new NSUrlSessionHandler
            {
                TrustOverrideForUrl = (sender, url, trust) => false,
            };
        }
    }
}
