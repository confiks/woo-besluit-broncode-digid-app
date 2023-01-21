using System.Collections.Generic;
using System.Linq;
using BerTlv;

namespace DigiD.Common.EID.Models.CardFiles
{
    public class EFDir : File
    {
        public EFDir(byte[] data) : base(data, "EF_Dir")
        {
        }

        public List<byte[]> Identifiers { get; } = new List<byte[]>();

        public override bool Parse()
        {
            var tlvs = Tlv.ParseTlv(Bytes);
            
            foreach (var o in tlvs)
            {
                Identifiers.Add(o.Children.First().Value);
            }

            return true;
        }
    }
}
