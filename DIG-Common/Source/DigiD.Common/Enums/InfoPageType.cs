namespace DigiD.Common.Enums
{
    public enum InfoPageType
	{
        Undefined,
        About,
		PinActivated,
		PinRegistration,
	    WIDHelpChangeTransportPIN, //Tijdelijke pincode invoeren
        WIDHelpChangePIN_PIN, //Pincode instellen/wijzigen
        WIDHelpEnterPIN, //Pincode invoeren
        WIDHelpResumePIN, //Rijbewijsnummer (pincode) invoeren
        WIDHelpScannerInfo,
        LoginHelp,
        KoppelcodeHelp,
        UpgradeRDA,
        LoginAndUpgrade,
        ChangeAppPIN,
        EmailEntry,
        EmailConfirm,
    }
}

