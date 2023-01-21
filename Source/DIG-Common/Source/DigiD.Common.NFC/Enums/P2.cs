namespace DigiD.Common.NFC.Enums
{
    public enum P2
    {
        FCP = 0x04,
        FMD = 0x08,
        NONE = 0x0C,
        SET_AT = 0xA4,
        HASH_ALGORITHM = 0xAA,
        COMPUTE_DIGITAL_SIGNATURE = 0xB6,
        ENCRYPT_OPERATION = 0xB8,
        DEFAULT_CHANNEL = 0x00,
        UNCRYPT_DATA = 0x80,
        ENCRYPT_DATA = 0x86,
        HASH_VALUE = 0x9A,
        CERTIFICATE = 0xBE
    }
}
