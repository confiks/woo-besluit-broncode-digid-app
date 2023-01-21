using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace DigiD.Common.Helpers
{
    internal static class CertificateExtension
    {
        internal static string GetSubjectName(this X509Certificate cert)
        {
            return cert.Subject.Split(',').Select(x => x.Split('=')).First(x => x[0] == "CN")[1];
        }

        internal static IEnumerable<string> GetSubjectAlternativeNames(this X509Certificate cert)
        {
            if (cert == null)
                throw new ArgumentNullException(nameof(cert));

            using (var certificate = new X509Certificate2(cert))
            {
                foreach (var extension in certificate.Extensions)
                {
                    // Create an AsnEncodedData object using the extensions information.
                    var asnEncodedData = new AsnEncodedData(extension.Oid, extension.RawData);
                    if (string.Equals(extension.Oid.FriendlyName, "Subject Alternative Name", StringComparison.InvariantCulture))
                    {
                        return new List<string>(asnEncodedData.Format(false).Split(new[] { ", ", "DNS Name=" }, StringSplitOptions.RemoveEmptyEntries));
                    }
                }

                return new List<string>();
            }
        }
    }
}
