using BerTlv;

namespace DigiD.Common.EID.Models.CardFiles
{
    public class DG15 : File
    {
        public DG15(byte[] data) : base(data, "EF_DG15")
        {
        }

        public override bool Parse()
        {
            var tlv = Tlv.ParseTlv(Bytes);
            return tlv.Count > 0;
        }
    }
}
