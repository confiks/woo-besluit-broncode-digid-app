using System.Linq;
using BerTlv;
using DigiD.Common.EID.Helpers;

namespace DigiD.Common.EID.Models.CardFiles
{
    public class PhotoFile : File
    {
        public byte[] Photo { get; private set; }

        public PhotoFile(byte[] data, string name) : base(data, name)
        {
        }

        public override bool Parse()
        {
            var tlvs = Tlv.ParseTlv(Bytes);

            if (tlvs.Count == 1)
            {
                var photo = tlvs.First().FindTag("7F2E", "5F2E");

                if (photo != null)
                {
                    Photo = photo.Data.Skip(51).ToArray();
                }

                return true;
            }

            return false;
        }
    }

    public class DG2 : PhotoFile
    {
        public DG2(byte[] data) : base(data, "DG2")
        {

        }
    }

    public class DG6 : PhotoFile
    {
        public DG6(byte[] data) : base(data, "DG6")
        {

        }
    }
}
