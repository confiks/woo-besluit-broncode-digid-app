using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class DigiDAppActiverenPaginaPassword_AP007 : Pageobject<DigiDAppActiverenPaginaPassword_AP007>
    {
        private const string _title = "DigiD app Activeren";
        private const string _waitText = "Uw DigiD app is geactiveerd!";
        private const string _begrepenButton = "Begrepen";

        private DigiDAppActiverenPaginaPassword_AP007(string title) : base(title, _waitText) { }

        public static DigiDAppActiverenPaginaPassword_AP007 Load(string title = _title)
            => new DigiDAppActiverenPaginaPassword_AP007(title);

        public DigiDAppActiverenPaginaPassword_AP007 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public DigiDAppActiverenPaginaPassword_AP007 ActiveerKnopBegrepen()
            => TapOn(_begrepenButton);



    }



}
