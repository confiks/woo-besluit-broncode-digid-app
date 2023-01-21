namespace DigiD.Common.NFC.Enums
{
    public enum INS : byte
    {
        EXTERNAL_AUTHENTICATE = 0x82,
        CHALLENGE = 0x84,
        GENERAL_AUTH = 0x86,
        PERFORM_SEC_OP = 0x2A,
        MANAGE_SEC_ENV = 0x22,
        SELECT_FILE = 0xA4,
        READ_FILE = 0xB0,
        SHORT_FILE_ACCESS = 0xB1,
        VERIFY = 0x20,
        RESET_RETRY_COUNTER = 0x2c,
        CHANGE_REFERENCE_DATA = 0x24,
        EMPTY = 0x00

    }
}
