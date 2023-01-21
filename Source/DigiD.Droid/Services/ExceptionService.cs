using System;
using DigiD.Common.Exceptions;
using DigiD.Common.Interfaces;
using DigiD.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExceptionService))]
namespace DigiD.Droid.Services
{
    public class ExceptionService : IExceptionService
    {
        public Exception Cast(Exception ex)
        {
            if (ex is Javax.Net.Ssl.SSLException)
                return new SslException(ex);
            
            return ex;
        }
    }
}
