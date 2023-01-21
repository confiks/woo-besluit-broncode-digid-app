using Javax.Net.Ssl;

namespace DigiD.Droid.Helpers
{
    public class AndroidClientHandler : Xamarin.Android.Net.AndroidClientHandler
    {
#if DEBUG || DEV || TEST || ACC 
        [System.Obsolete]
        protected override SSLSocketFactory ConfigureCustomSSLSocketFactory(HttpsURLConnection connection)
        {
            return Android.Net.SSLCertificateSocketFactory.GetInsecure(1000, null);
        }
#endif

        protected override IHostnameVerifier GetSSLHostnameVerifier(HttpsURLConnection connection)
        {
            return new CertificatePinningHostnameVerifier();
        }
    }

    public class TestAndroidClientHandler : Xamarin.Android.Net.AndroidClientHandler
    {
        protected override IHostnameVerifier GetSSLHostnameVerifier(HttpsURLConnection connection)
        {
            return new CertificatePinningHostnameVerifier(true);
        }
    }
}
