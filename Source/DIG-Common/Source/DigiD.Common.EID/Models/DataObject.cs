using System;
using System.Globalization;
using System.IO;
using DigiD.Common.NFC.Helpers;

namespace DigiD.Common.EID.Models
{
    public class DataObject
    {
        public DataObject()
        {
            
        }

        public DataObject(byte[] tag, DataObject data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Tag = tag;
            Value = data.GetEncoded();
        }

        public DataObject(byte[] tag, byte[] data)
        {
            Tag = tag;
            Value = data;
        }

        public byte[] Tag { get; set; }
        public byte[] Value { get; set; }
        public byte[] Length => Value != null ? Value.Length.ToString("X2", CultureInfo.InvariantCulture).ConvertHexToBytes() : new byte[]{ 0x00 };
        public DataObject[] Children { get; set; }

        public string HexTag=> Tag.ToHexString();
        public string HexValue => Value.ToHexString();
        public string HexLength => Length.ToHexString();

        public byte[] GetEncoded()
        {
            using (var s = new MemoryStream())
            {
                using (var bw = new BinaryWriter(s))
                {
                    bw.Write(Tag);
                    if (Length.Length > 1)
                        bw.Write(new byte[]{0x82});

                    bw.Write(Length);

                    if (Value != null)
                        bw.Write(Value);

                    return s.ToArray();
                }
            }
        }
    }
}
