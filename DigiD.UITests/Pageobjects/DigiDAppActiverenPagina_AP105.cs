using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class DigiDAppActiverenPagina_AP105 : Pageobject<DigiDAppActiverenPagina_AP105>
    {
        private const string _title = "DigiD app activeren";
        private const string _waitText = "DigiD app activeren";
        private const string _volgendeButton = "Volgende";
        private Query _idCheckNietGelukt = x => x.Raw("* {text CONTAINS 'De ID-check is niet gelukt. Maar uw DigiD app'}");


        private DigiDAppActiverenPagina_AP105(string title) : base(title, _waitText) { }

        public static DigiDAppActiverenPagina_AP105 Load(string title = _title)
            => new DigiDAppActiverenPagina_AP105(title);

        public DigiDAppActiverenPagina_AP105 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_idCheckNietGelukt);

        public DigiDAppActiverenPagina_AP105 ActiveerKnopVolgende()
           => TapOn(_volgendeButton);

    }
}
