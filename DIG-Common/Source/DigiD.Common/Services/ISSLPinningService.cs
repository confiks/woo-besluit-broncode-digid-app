using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace DigiD.Common.Services
{
    public interface ISslPinningService
    {
        bool ValidateCertificate(string domain, List<X509Certificate> certificates);
    }
}
