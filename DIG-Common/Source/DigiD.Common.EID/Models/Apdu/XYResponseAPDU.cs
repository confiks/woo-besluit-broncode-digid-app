using System;
using System.Linq;
using DigiD.Common.NFC.Models;

namespace DigiD.Common.EID.Models.Apdu
{
    public class XYResponseAPDU
    {
        public bool UncompressedPoint { get; set; }

        public byte[] EncodedPoint { get; set; }
        public byte[] PointX { get; set; }
        public byte[] PointY { get; set; }
        public XYResponseAPDU(ResponseApdu apdu)
        {
            if (apdu == null)
                throw new ArgumentNullException(nameof(apdu));

            var data = apdu.Data;
            UncompressedPoint = data[4] == 0x04;

            EncodedPoint = data.Skip(4).ToArray();

            var points = data.Skip(5).ToArray();

            PointX = points.Take(points.Length / 2).ToArray();
            PointY = points.Skip(points.Length / 2).ToArray();
        }
    }
}
