namespace DigiD.Common.Enums
{
    public enum PinEntryType
    {
        Enrollment = 1,
        Authentication = 2,
        ChangeTransportPIN = 4,
        ChangePIN_PIN = 8,
        ResumePIN_CAN = 16,
#if READ_PHOTO
        ReadPhoto = 64
#endif
    }
}

