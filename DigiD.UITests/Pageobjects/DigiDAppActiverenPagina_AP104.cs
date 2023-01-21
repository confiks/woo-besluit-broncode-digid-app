using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class DigiDAppActiverenPagina_AP104 : Pageobject<DigiDAppActiverenPagina_AP104>
    {
        private const string _title = "DigiD app activeren";
        private const string _waitText = "DigiD app activeren";
        private const string _volgendeButton = "Volgende";
        private Query _idCheckNietGelukt = x => x.Raw("* {text CONTAINS 'De ID-check is niet gelukt. U gaat nu uw DigiD app activeren met een sms-code.'}");


        private DigiDAppActiverenPagina_AP104 (string title) : base(title, _waitText) { }

        public static DigiDAppActiverenPagina_AP104 Load(string title = _title)
            => new DigiDAppActiverenPagina_AP104(title);

        public DigiDAppActiverenPagina_AP104 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_idCheckNietGelukt);

        public DigiDAppActiverenPagina_AP104 ActiveerKnopVolgende()
           => TapOn(_volgendeButton);

    }
}
