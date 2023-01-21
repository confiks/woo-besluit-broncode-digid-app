using System.Net.Http;

namespace DigiD.Common.Interfaces
{
    public interface IHttpMessageHandlerService
    {
        HttpMessageHandler GetHttpMessageHandler();
        int RemoveNativeCookies(string url = null, string name = null);
    }
}
