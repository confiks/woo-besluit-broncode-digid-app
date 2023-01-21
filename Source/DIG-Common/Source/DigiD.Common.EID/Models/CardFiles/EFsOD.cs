using System.Collections.Generic;
using System.Linq;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;

namespace DigiD.Common.EID.Models.CardFiles
{
    public class EFSOd : File
    {
        internal SignedData SignedData { get; private set; }
        internal Dictionary<int, byte[]> Hashes = new Dictionary<int, byte[]>();

        public EFSOd(byte[] data) : base(data, "EF.SOd")
        {
        }

        public override bool Parse()
        {
            using (var s = new Asn1InputStream(Bytes))
            {
                var app = (DerApplicationSpecific)s.ReadObject();
                var sequence = (DerSequence)app.GetObject();
                var objectIdentifier = (DerObjectIdentifier)sequence[0];

                if (objectIdentifier.Id != "1.2.840.113549.1.7.2")
                {
                    //Didn't found SignedData!
                    return false;
                }

                var s2 = (DerSequence)((DerTaggedObject)sequence[1]).GetObject();
                SignedData = SignedData.GetInstance(s2);

                var obj1 = (DerTaggedObject)sequence[1];
                var seq2 = (DerSequence)obj1.GetObject();
                var seq3 = (DerSequence)seq2[2];
                var obj2 = (DerTaggedObject)seq3[1];
                var dos = (DerOctetString)obj2.GetObject();

                var mRTDSignatureData = (DerSequence)Asn1Object.FromByteArray(dos.GetOctets());
                var hashes = (DerSequence)mRTDSignatureData[2];

                foreach (var hash in hashes.Cast<DerSequence>())
                {
                    var nr = ((DerInteger)hash[0]).PositiveValue.IntValue;
                    Hashes.Add(nr, ((Asn1OctetString)hash[1]).GetOctets());
                }
            }

            return true;
        }
    }
}
