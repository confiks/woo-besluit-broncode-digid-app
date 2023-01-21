namespace DigiD.Common.NFC.Enums
{
    public enum CLA : byte
    {
        PLAIN = 0x00,
        COMMAND_CHAINING = 0x10,
        SECURE_MESSAGING = 0x0C,
        COMMAND_CHAINING_START = 0x1C,
        TIMEOUT = 0xff
    }
}
