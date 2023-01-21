namespace DigiD.Common.NFC.Enums
{
    public enum P1
    {
        SELECT_MF = 0x00,
        CHILD_DF = 0x01,
        CHILD_EF = 0x02,
        PARENT_DF = 0X03,
        APPLICATION_ID = 0x04,
        ABS_PATH = 0x08,
        REL_PATH = 0x09,
        COMPUTE_DIGITAL_SIGNATURE = 0x41,
        PUT_HASH = 0xA0,
        PERFORM_SECURITY_OPERATION = 0xC1,
        SET_DST = 0x81,
        ERASE = 0xF3,
        DECRYPT = 0x80,
        ENCRYPT = 0x86,
        SIGN_HASH = 0x9E,
    }
}
