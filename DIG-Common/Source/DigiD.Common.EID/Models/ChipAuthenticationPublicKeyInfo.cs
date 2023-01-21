using System;
using DigiD.Common.EID.Interfaces;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X9;

namespace DigiD.Common.EID.Models
{
    public class ChipAuthenticationPublicKeyInfo : IAsn1Info
    {
        public byte[] Pk { get; }
        public DerObjectIdentifier OID { get; set; }
        public static X9ECParameters Algorithm => ECNamedCurveTable.GetByName("BrainpoolP320R1");

        public ChipAuthenticationPublicKeyInfo(Asn1Sequence data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            OID = DerObjectIdentifier.GetInstance(data[0]);
            var primitive = ((DerSequence) data[1])[1].ToAsn1Object();
            Pk = ((DerBitString) Asn1Object.FromByteArray(primitive.GetEncoded())).GetBytes();
        }
    }
}
