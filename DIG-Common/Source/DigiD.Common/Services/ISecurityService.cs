using System.Net.Http;

namespace DigiD.Common.Services
{
    public interface ISecurityService
    {
        bool IsDebuggerAttached { get; }
        bool IsRunningOnVirtualDevice { get; }
        HttpMessageHandler GetHttpMessageHandler();
    }
}
