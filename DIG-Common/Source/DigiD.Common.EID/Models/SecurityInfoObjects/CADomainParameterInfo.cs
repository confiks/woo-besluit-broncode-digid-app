using System;
using DigiD.Common.EID.Interfaces;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;

namespace DigiD.Common.EID.Models.SecurityInfoObjects
{
    public class CADomainParameterInfo : Asn1Encodable, IAsn1Info
    {
        public DerObjectIdentifier OID { get; }
        public AlgorithmIdentifier DomainParameter { get; }
        public X9ECParameters Algorithm => new X9ECParameters(Asn1Sequence.GetInstance(DomainParameter.Parameters));

        public CADomainParameterInfo(Asn1Sequence sequence)
        {
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));

            OID = (DerObjectIdentifier) sequence[0];
            DomainParameter = AlgorithmIdentifier.GetInstance(sequence[1]);
        }

        public override string ToString()
        {
            return $"CADomainParameters\r\nOID: {OID}\r\nDomainParameter: {DomainParameter.Algorithm}\r\n";
        }

        public override Asn1Object ToAsn1Object()
        {
            var v = new Asn1EncodableVector { OID, DomainParameter };
            return new DerSequence(v);
        }
    }
}
