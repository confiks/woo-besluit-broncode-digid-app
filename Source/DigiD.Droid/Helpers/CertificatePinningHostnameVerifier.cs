using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DigiD.Common.Services;
using Javax.Net.Ssl;
using Xamarin.Forms;

namespace DigiD.Droid.Helpers
{
    internal class CertificatePinningHostnameVerifier : Java.Lang.Object, IHostnameVerifier
    {
        private readonly bool _test;

        public CertificatePinningHostnameVerifier(bool test = false)
        {
            _test = test;
        }

        public bool Verify(string hostname, ISSLSession session)
        {
            if (_test)
                return false;

            var certificates = ExtractCertificates(session);
            return DependencyService.Get<ISslPinningService>().ValidateCertificate(hostname, certificates);
        }

        private static List<X509Certificate> ExtractCertificates(ISSLSession session)
        {
            var result = new List<X509Certificate>();
            var certificates = session.GetPeerCertificateChain();

            if (certificates == null)
                return result;

            foreach (var certificate in certificates)
            {
                result.Add(new X509Certificate(certificate.GetEncoded()));
            }

            return result;
        }
    }
}
