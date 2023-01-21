using System.Linq;
using BerTlv;
using DigiD.Common.EID.Helpers;

namespace DigiD.Common.EID.Models.CardFiles
{
    public class EFCOM : File
    {
        public byte[] SupportedTags { get; private set; }
        public EFCOM(byte[] data) : base(data, "EFCOM")
        {
            
        }

        public override bool Parse()
        {
            var tlvs = Tlv.ParseTlv(Bytes);
            var taglist = tlvs.First().FindTag("5C");
            SupportedTags = taglist.Value;
            
            return true;
        }
    }
}
