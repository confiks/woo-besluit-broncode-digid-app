using System.Linq;
using System.Text;
using BerTlv;
using DigiD.Common.EID.Helpers;

namespace DigiD.Common.EID.Models.CardFiles
{
    public class DG1 : File
    {
        public string MRZ { get; private set; }
        public DG1(byte[] data) : base(data, "DG1")
        {
            
        }

        public override bool Parse()
        {
            var tlv = Tlv.ParseTlv(Bytes);

            if (tlv.Count == 1)
            {
                var mrzData = tlv.First().FindTag("5F1F");
                var dlData = tlv.First().FindTag("5F02");

                if (mrzData != null)
                    MRZ = Encoding.ASCII.GetString(mrzData.Value);
                else
                    MRZ = Encoding.ASCII.GetString(dlData.Value);

                return true;
            }

            return false;
        }
    }
}
