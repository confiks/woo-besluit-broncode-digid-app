using DigiD.Common.EID.Helpers;
using DigiD.Common.EID.Models.SecurityInfoObjects;
using Org.BouncyCastle.Asn1;

namespace DigiD.Common.EID.Models.CardFiles
{
    public class DG14 : File
    {
        public ChipAuthenticationPublicKeyInfo ChipAuthenticationPublicKeyInfo { get; set; }
        public CAInfo ChipAuthenticationInfo { get; set; }

        public DG14(byte[] data) : base(data, "EF_DG14")
        {
            
        }

        public override bool Parse()
        {
            var app = (DerApplicationSpecific)Asn1Object.FromByteArray(Bytes);
            var set = (Asn1Set)Asn1Object.FromByteArray(app.GetContents());

            ChipAuthenticationInfo = (CAInfo)InfoFactory.Parse<CAInfo>(set);
            ChipAuthenticationPublicKeyInfo = (ChipAuthenticationPublicKeyInfo)InfoFactory.Parse<ChipAuthenticationPublicKeyInfo>(set);

            return true;
        }
    }
}
