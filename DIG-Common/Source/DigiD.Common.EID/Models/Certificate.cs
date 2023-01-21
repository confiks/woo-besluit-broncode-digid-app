using System.Linq;
using DigiD.Common.EID.Helpers;
using Org.BouncyCastle.Asn1;

namespace DigiD.Common.EID.Models
{
    public class Certificate
    {
        public Certificate(byte[] certBytes)
        {
            using (var stream = new Asn1InputStream(certBytes))
            {
                var data = (DerSequence)stream.ReadObject();
                Parse(data);
            }
        }

        public byte[] Body { get; private set; }
        public byte[] Signature { get; private set; }

        public DataObject ProfileIdentifier { get; set; }
        public DataObject AuthReference { get; set; }
        public DataObject PublicKeyOID { get; set; }
        public DataObject PublicKeyECPoint { get; set; }
        public DataObject HolderReference { get; set; }
        public DataObject ChatOID { get; set; }
        public DataObject ChatDiscretionaryData { get; set; }
        public DataObject EffectiveDate { get; set; }
        public DataObject ExpirationDate { get; set; }
        public DataObject CertRelativeAuth { get; set; }


        private void Parse(DerSequence o)
        {
            var certificateEncoded = Asn1Sequence.GetInstance(o);
            Body = certificateEncoded[0].ToAsn1Object().GetEncoded();

            Signature = certificateEncoded[1].ToAsn1Object().GetEncoded();

            var certBody = Asn1Sequence.GetInstance(((DerApplicationSpecific)certificateEncoded[0]).GetObject(16)); //Sequence

            var tmp = certBody[0].ToAsn1Object().GetEncoded();
            ProfileIdentifier = new DataObject(new[] { tmp[0], tmp[1] }, tmp.Skip(3).ToArray());

            tmp = certBody[1].ToAsn1Object().GetEncoded();
            AuthReference = new DataObject(new[] { tmp[0] }, tmp.Skip(2).ToArray());

            var sequence = Asn1Sequence.GetInstance(((DerApplicationSpecific)certBody[2]).GetObject(16)); //Sequence

            tmp = sequence[0].ToAsn1Object().GetEncoded();
            PublicKeyOID = new DataObject(new []{tmp[0]}, tmp.Skip(2).ToArray());

            tmp = sequence[1].ToAsn1Object().GetEncoded();
            PublicKeyECPoint = new DataObject(new[]{tmp[0]}, tmp.Skip(3).ToArray());

            tmp = certBody[3].ToAsn1Object().GetEncoded();
            HolderReference = new DataObject(new []{tmp[0], tmp[1]}, tmp.Skip(3).ToArray() );

            sequence = Asn1Sequence.GetInstance(((DerApplicationSpecific)certBody[4]).GetObject(16)); //Sequence
            tmp = sequence[0].ToAsn1Object().GetEncoded();
            ChatOID = new DataObject(new[] { tmp[0] }, tmp.Skip(2).ToArray());

            tmp = sequence[1].ToAsn1Object().GetEncoded();
            ChatDiscretionaryData = new DataObject(new[] { tmp[0] }, tmp.Skip(2).ToArray());

            tmp = certBody[5].ToAsn1Object().GetEncoded();
            EffectiveDate = new DataObject(new[] { tmp[0], tmp[1] }, tmp.Skip(3).ToArray());

            tmp = certBody[6].ToAsn1Object().GetEncoded();
            ExpirationDate = new DataObject(new[] { tmp[0], tmp[1] }, tmp.Skip(3).ToArray());

            tmp = certBody[7].ToAsn1Object().GetEncoded();
            CertRelativeAuth = DataObjectHelper.GetDataObject(tmp);
        }
    }
}
