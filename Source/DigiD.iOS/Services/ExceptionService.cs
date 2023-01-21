using System;
using System.Net.Http;
using DigiD.Common.Exceptions;
using DigiD.Common.Interfaces;
using DigiD.iOS.Services;
using Xamarin.Forms;

[assembly:Dependency(typeof(ExceptionService))]
namespace DigiD.iOS.Services
{
    public class ExceptionService : IExceptionService
    {
        public Exception Cast(Exception ex)
        {
            if (ex is HttpRequestException webException && webException.InnerException?.Message == "Error: TrustFailure")
                return new SslException(ex);

            return ex;
        }
    }
}
