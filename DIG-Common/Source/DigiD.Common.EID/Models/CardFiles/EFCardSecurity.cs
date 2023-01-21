﻿using DigiD.Common.EID.Helpers;
using Org.BouncyCastle.Asn1;

namespace DigiD.Common.EID.Models.CardFiles
{
    public class EFCardSecurity : File
    {
        public DerObjectIdentifier OID { get; private set; }
        public byte[] KeyReference { get; private set; }
        public ChipAuthenticationPublicKeyInfo ChipAuthenticationPublicKeyInfo  { get; set; }

        public EFCardSecurity(byte[] data) : base(data, "EF.CardSecurity")
        {
        }

        public override bool Parse()
        {
            using (var s = new Asn1InputStream(Bytes))
            {
                var tagged1 = (DerTaggedObject) Asn1Sequence.GetInstance(s.ReadObject())[1];
                var seq = (Asn1Sequence) Asn1Sequence.GetInstance(tagged1, false)[0];
                var tagged2 = (DerTaggedObject)((Asn1Sequence) seq[2])[1];

                var idSecurityObject = Asn1OctetString.GetInstance(tagged2, false).GetOctets();

                var caPkInfoSeq = (DerSequence)((DerSet) Asn1Object.FromByteArray(idSecurityObject))[1];
                
                ChipAuthenticationPublicKeyInfo = (ChipAuthenticationPublicKeyInfo) InfoFactory.Parse(caPkInfoSeq);

                var caDomInfoSeq = (DerSequence)((DerSet)Asn1Object.FromByteArray(idSecurityObject))[2];

                OID = (DerObjectIdentifier) caDomInfoSeq[0];
                KeyReference = ((DerInteger) caDomInfoSeq[1]).PositiveValue.ToByteArray();
            }

            return true;
        }
    }
}
