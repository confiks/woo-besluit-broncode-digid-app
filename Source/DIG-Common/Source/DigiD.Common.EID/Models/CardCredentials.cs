using System;
using System.Security;
using DigiD.Common.EID.Enums;
using DigiD.Common.EID.Helpers;

namespace DigiD.Common.EID.Models
{
    public class CardCredentials
    {
        public SecureString PIN { get; }
        public SecureString CAN { get; }
        public SecureString PUK { get; private set; }
        public SecureString MRZ {get;set;}

        public CardCredentials(SecureString pin, SecureString can, SecureString puk, SecureString mrz = null)
        {
            PIN = pin;
            CAN = can;
            PUK = puk;
            MRZ = mrz;
        }

        public byte[] Password(PasswordType passwordType)
        {
            switch (passwordType)
            {
                case PasswordType.CAN:
                    return CAN.ToBytes();
                case PasswordType.PIN:
                    return PIN.ToBytes();
                case PasswordType.PUK:
                    return PUK.ToBytes();
                case PasswordType.MRZ:
                    return MRZ.ToBytes();
            }

            return Array.Empty<byte>();
        }
    }
}
