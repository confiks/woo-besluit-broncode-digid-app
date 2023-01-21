#if SSL_UNPINNING
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace DigiD.Common.Services
{
    public class SSLPinningDisabled : ISslPinningService
    {
        public bool ValidateCertificate(string domain, List<X509Certificate> certificates)
        {
            return true;
        }
    }
}
#endif
