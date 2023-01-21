using System;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

namespace DigiD.Common.EID.CardSteps.PA
{
    public static class CmsVerifier
    {
        public static ContentInfo Verify(this SignedData signedData, ContentInfo signedMessage, DateTime date)
        {
            X509Certificate cert = certificate(signedData);
            var name = new X500DistinguishedName(cert.IssuerDN.GetEncoded());
            var cms = new CmsSignedData(signedMessage);
            var data = SignedData.GetInstance(signedMessage);
            return data.EncapContentInfo;
        }

        private static X509Certificate certificate(SignedData signedData)
        {
            var data = signedData.Certificates[0].ToAsn1Object();
            var x = new X509Certificate(X509CertificateStructure.GetInstance(data));
            return x;
        }
    }
}
