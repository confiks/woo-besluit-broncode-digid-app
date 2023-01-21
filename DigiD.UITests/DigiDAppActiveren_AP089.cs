using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class DigiDAppActiverenPagina_AP089 : Pageobject<DigiDAppActiverenPagina_AP089>
    {
        private const string _title = "DigiD app activeren";
        private const string _waitText = "De ID-check is niet gelukt. Wilt u het opnieuw proberen? U kunt de app nu al gebruiken, maar dan moet u later alsnog een keer uw identiteitsbewijs scannen. Dit is nodig om toegang te krijgen bij organisaties die erg privacygevoelige informatie van u hebben.";
        private const string _nogmaalsScannenButton = "Nogmaals scannen";
        private const string _scanOverslaanButton = "Scan overslaan";


        private DigiDAppActiverenPagina_AP089(string title) : base(title, _waitText) { }

        public static DigiDAppActiverenPagina_AP089 Load(string title = _title)
            => new DigiDAppActiverenPagina_AP089(title);

        public DigiDAppActiverenPagina_AP089 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public DigiDAppActiverenPagina_AP089 ActiveerNogmaalsScannen()
            => TapOn(_nogmaalsScannenButton);

        public DigiDAppActiverenPagina_AP089 ScanOverslaan()
            => TapOn(_scanOverslaanButton);

    }
}
