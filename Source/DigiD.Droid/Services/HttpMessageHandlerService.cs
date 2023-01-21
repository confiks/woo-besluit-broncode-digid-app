using System;
using System.Net;
using System.Net.Http;
using DigiD.Common.Interfaces;
using DigiD.Common.NFC.Helpers;
using DigiD.Droid.Helpers;
using DigiD.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpMessageHandlerService))]
namespace DigiD.Droid.Services
{
    public class HttpMessageHandlerService : IHttpMessageHandlerService
    {
        private AndroidClientHandler _handler;

        public HttpMessageHandler GetHttpMessageHandler()
        {
            _handler = new AndroidClientHandler
            {
                AllowAutoRedirect = false,
#if DEBUG || DEV || TEST || ACC
                ConnectTimeout = TimeSpan.FromMilliseconds(200),
#endif
            };

            return _handler;
        }

        public int RemoveNativeCookies(string url = null, string name = null)
        {
            if (url == null)
            {
                if (_handler != null)
                    _handler.CookieContainer = new CookieContainer();
                return 0;
            }

            var cookieContainer = _handler.CookieContainer;
            var cookies = cookieContainer.GetCookies(new Uri(url));

            if (name != null)
            {
                try
                {
                    var cookie = cookies[name];
                    if (cookie != null)
                    {
                        cookie.Expired = true;

                        Debugger.WriteLine($"Cookie with name '{name}' was deleted.");
                        return 1;
                    }
                }
                catch
                {
                    Debugger.WriteLine($"Cookie with name '{name}' was not found.");
                    return 0;
                }
            }

            var deletedCookies = 0;

            for (int i = 0; i < cookies.Count; i++)
            {
                try
                {
                    var cookie = cookies[i];
                    cookie.Expired = true;

                    Debugger.WriteLine($"Cookie with name '{cookie.Name}' was deleted.");
                    deletedCookies++;
                }
                catch (Exception ex)
                {
                    Debugger.WriteLine($"Exception removing the cookie at index {i}, with message: {ex.Message}.");
                }
            }

            return deletedCookies;
        }
    }
}
