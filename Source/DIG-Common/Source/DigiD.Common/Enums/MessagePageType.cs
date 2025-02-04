namespace DigiD.Common.Enums
{
    public enum MessagePageType
	{
		AppBlocked,
		ActivationTimeElapsed,
		ActivationCompleted,
		ActivationCancelled,
		ActivationFailed,
        ActivationFailedTooManyDevices,
        ActivationLetterBlocked,
        ActivationLetterExpired,
        ActivationDisabled,
        ActivationPending,
        ActivationFailedTooSoon,
        ActivationFailedTooFast,
        AccountBlocked,
        GBAFailed,
        LoginUnknown,
		LoginSuccess,
	    NoBSN,
        LoginCancelled,
        LoginAborted,
		NoInternetConnection,
		ErrorOccoured,
		InvalidScan,
		ScanTimeout,
        SessionTimeout,
        ChallengeFailed,
        HostUnknown,
        SSLException,
        BrowserUnsupported,
        NoNFCSupport,
        UpgradeAccountWithNFCSuccess,
        UpgradeAccountWithNFCFailed,
        UpgradeAccountWithNFCError,
        UpgradeAccountWithNFCCancelled,
	    UpgradeAccountWithNFCAborted,
        ReRequestLetterSuccess,
        NetworkTimeout,
        PasswordChangeSuccess,
        ActivateSMSAuthenticationSuccess,
        ActivateSMSAuthenticationBlocked,
        PhoneNumberChangeSuccess,
        WIDRandomizeSuccess,
        WIDRandomizeFailed,
        WIDSuspended,
        WIDCancelled,
        WIDFirstUse,
        WIDChangePINSuccess,
        WIDResumePINSuccess,
	    WIDResumePUKSuccess,
        WIDResumeFailed,
        WIDPINBlocked,
        WIDPUKBlocked,
        WIDFailed,
	    WIDLoginFailed,
        VerificationCodeFailed,
        WIDActivationSuccess,
        WIDActivationFailed,
        WIDReActivationSuccess,
        WIDReActivationFailed,
        WIDRetractSuccess,
        WIDNotSupported,
        WIDReaderTimeout,
        WIDActivationNeeded,
        InvalidAuthenticationLevel,
        UnknownError,
        NotActivated,
        ReactivationNeeded,
        InsufficientLoginLevel,
        RDADisabled,
        UpgradeAccountWithNFCNoDocuments,
        UpgradeAccountWithIDCheckerSuccess,
        EmailChange,
        EmailAdd,
        EmailRemove,
        AuthenticationAborted,
        AppActivationWithAppSuccess,
        ActivateViaRequestStationCancel,
        AccountCancelledSuccess,
        ChangePinSuccess,
        ChangePinFailed,
        ChangePinMaxReached,
        DeviceNotSupported,
        ActivateViaRequestStationCodeExpired,
        DeactivateAppSuccess,
        ActivationPendingNoSMSCheck, // Gebruiker heeft een account aangevraagd, maar dan zonder SMS controle
        InAppActivationCodeBlocked,
        InAppActivationCodeExpired,
        EmailRegistrationTooManyMails,
        EmailRegistrationMaximumReached,
        EmailRegistrationCodeBlocked, //te vaak foutieve code ingevoerd
        UpgradeAndAuthenticateFailed,
        EmailRegistrationAlreadyVerified,
        EmailRegistrationSuccess,
        EmailRegistrationSuccessfulVerified,
        AppDeactivated,
        NoValidDocuments,
        RequestAccountBlockedTemporarily,
        RequestAccountInvalid,
        RequestAccountNotAvailable,
        RequestAccountDeceased,
        RequestAccountTooOftenDay,
        RequestAccountTooOftenMonth,
        RequestAccountAccountBlocked,
        RequestAccountBRPTimeout,
        RequestAccountNotLatestAddress,
        RequestAccountAddressUnderInvestigating,
        RequestAccountAddressAbroad,
        UpgradeAndAuthenticateAborted,
        ClassifiedDeceased,
        ContactHelpDesk,
    }
}

