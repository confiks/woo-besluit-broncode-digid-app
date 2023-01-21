using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class DigiDAppActiverenPagina_AP088 : Pageobject<DigiDAppActiverenPagina_AP088>
    {
        private const string _title = "DigiD app activeren";
        private const string _waitText = "De ID-check is niet gelukt. Wilt u het opnieuw proberen? U kunt ook uw DigiD app activeren met een sms-code. U moet dan later nog wel een keer uw identiteitsbewijs scannen. Dit is nodig om toegang te krijgen bij organisaties die erg privacygevoelige informatie van u hebben.";
        private const string _nogmaalsScannenButton = "Nogmaals scannen";
        private const string _activerenMetSMSButton = "Activeren met sms";


        private DigiDAppActiverenPagina_AP088(string title) : base(title, _waitText) { }

        public static DigiDAppActiverenPagina_AP088 Load(string title = _title)
            => new DigiDAppActiverenPagina_AP088(title);

        public DigiDAppActiverenPagina_AP088 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public DigiDAppActiverenPagina_AP088 ActiveerNogmaalsScannen()
            => TapOn(_nogmaalsScannenButton);

        public DigiDAppActiverenPagina_AP088 ActiveerActiverenMetSMS()
            => TapOn(_activerenMetSMSButton);

    }
}
