using DigiD.Common.NFC.Helpers;

namespace DigiD.Common.EID.Models.CardFiles
{
    public abstract class File
    {
        protected File(byte[] data, string name)
        {
            Bytes = data;
            Name = name;
        }

        public abstract bool Parse();

        public byte[] Bytes { get; }
        public string Name { get; }
        public string Base64Encoded => Bytes.ToBase64();
    }
}
