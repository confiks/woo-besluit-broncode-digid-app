using System.Net;

namespace DigiD.Common.Interfaces
{
    public interface IProxyInfoProvider
    {
        WebProxy GetProxySettings();
    }
}
