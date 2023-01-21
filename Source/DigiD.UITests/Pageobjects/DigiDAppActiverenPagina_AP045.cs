using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class DigiDAppActiverenPagina_AP045 : Pageobject<DigiDAppActiverenPagina_AP045>
    {
        private const string _title = "DigiD app activeren";
        private const string _waitText = "Voer activeringscode uit brief in.";
        private const string _sluitenButton = "Sluiten";
        
        private DigiDAppActiverenPagina_AP045(string title) : base(title, _waitText) { }

        public static DigiDAppActiverenPagina_AP045 Load(string title = _title)
            => new DigiDAppActiverenPagina_AP045(title);

        public DigiDAppActiverenPagina_AP045 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public DigiDAppActiverenPagina_AP045 VoerBriefActiveringscodeIn()
            => WaitForElementToAppear(_waitText)
            .EnterText("SSSSSSSSS");

                
    }
}
