using System.Linq;
using BerTlv;

namespace DigiD.Common.EID.Helpers
{
    public static class TlvHelper
    {
        public static Tlv FindTag(this Tlv tlv, params string[] tags)
        {
            if (tags.Contains(tlv.HexTag))
                return tlv;

            if (tlv.Children.Count > 0)
            {
                foreach (var t in tlv.Children)
                {
                    var result =  t.FindTag(tags);

                    if (result != null)
                        return result;
                }
            }

            return null;
        }
    }
}
