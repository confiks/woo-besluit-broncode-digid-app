using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class DigiDAppActiverenPagina_AP012 : Pageobject<DigiDAppActiverenPagina_AP012>
    {
        private const string _title = "DigiD app activeren";
        //private const string _waitText = "Met de activeringscode uit de brief kunt u de DigiD app activeren.";
        static Xquery _waitText = new Xquery()
        {
            Android = c => c.Marked("Met de activeringscode uit de brief kunt u de DigiD app activeren."),
            iOS = c => c.Text("Met de activeringscode uit de brief kunt u de DigiD app activeren.")
        };
        private const string _annulerenButton = "Annuleren";
        private const string _activerenButton = "Activeren";

        private DigiDAppActiverenPagina_AP012(string title) : base(title, _waitText) { }

        public static DigiDAppActiverenPagina_AP012 Load(string title = _title)
            => new DigiDAppActiverenPagina_AP012(title);

        public DigiDAppActiverenPagina_AP012 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public DigiDAppActiverenPagina_AP012 ActiveerKnopActiveren()
            => TapOn(_activerenButton);

    }
}
