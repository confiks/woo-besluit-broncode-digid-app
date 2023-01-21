using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class DigiDAppActiverenPaginaRDA_AP007 : Pageobject<DigiDAppActiverenPaginaRDA_AP007>
    {
        private const string _title = "DigiD app Activeren";
        private const string _waitText = "Uw DigiD app is geactiveerd!";
        private const string _begrepenButton = "Begrepen";

        private DigiDAppActiverenPaginaRDA_AP007(string title) : base(title, _waitText) { }

        public static DigiDAppActiverenPaginaRDA_AP007 Load(string title = _title)
            => new DigiDAppActiverenPaginaRDA_AP007(title);

        public DigiDAppActiverenPaginaRDA_AP007 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public DigiDAppActiverenPaginaRDA_AP007 ActiveerKnopBegrepen()
            => TapOn(_begrepenButton);



    }



}
