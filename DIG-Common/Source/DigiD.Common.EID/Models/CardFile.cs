using DigiD.Common.NFC.Enums;
using DigiD.Common.NFC.Helpers;

namespace DigiD.Common.EID.Models
{
    public class CardFile
    {
        public P1 Type { get; internal set; }
        internal byte[] Id { get; }
        public string Identifier => Id.ToHexString();

        internal CardFile(P1 p1, byte[] fileId)
        {
            Type = p1;
            Id = fileId;
        }

        public static CardFile EFDir => new CardFile(P1.CHILD_EF, "2F-00".ConvertHexToBytes());
        public static CardFile EFCardAccess => new CardFile(P1.CHILD_EF, "01-1C".ConvertHexToBytes());
        internal static CardFile EFCardSecurity => new CardFile(P1.CHILD_EF, "01-1D".ConvertHexToBytes());
        internal static CardFile EFCVCA => new CardFile(P1.CHILD_EF, "01-1C".ConvertHexToBytes());
    }
}
