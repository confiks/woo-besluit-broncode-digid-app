using System;
using DigiD.Common.EID.Interfaces;
using Org.BouncyCastle.Asn1;

namespace DigiD.Common.EID.Models.SecurityInfoObjects
{
    public class TAInfo : Asn1Encodable, IAsn1Info
    {
        
        public DerObjectIdentifier OID { get; }
        public DerInteger Version { get; }

        public TAInfo(Asn1Sequence sequence)
        {
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));

            OID = (DerObjectIdentifier)sequence[0];
            Version = (DerInteger)sequence[1];
        }

        public override string ToString()
        {
            return $"TAInfo\r\nOID: {OID}\r\nVersion: {Version}\n";
        }

        public override Asn1Object ToAsn1Object()
        {
            var v = new Asn1EncodableVector { OID, Version };
            return new DerSequence(v);
        }
    }
}
