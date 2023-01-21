using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace DigiD.UITests.Pageobjects
{
    public class DigiDAppDeactiverenPagina_AP077 : Pageobject<DigiDAppDeactiverenPagina_AP077>
    {
        private const string _title = "DigiD app deactiveren";
        //private const string _waitText = "Uw DigiD app is gedeactiveerd.";
        static Xquery _waitText = new Xquery()
        {
            Android = c => c.Marked("Uw DigiD app is gedeactiveerd."),
            iOS = c => c.Text("Uw DigiD app is gedeactiveerd.")
        };
        private const string _okButton = "OK";

        private DigiDAppDeactiverenPagina_AP077(string title) : base(title, _waitText) { }

        public static DigiDAppDeactiverenPagina_AP077 Load(string title = _title)
            => new DigiDAppDeactiverenPagina_AP077(title);

        public DigiDAppDeactiverenPagina_AP077 ControleerOfJuisteTekstWordtGetoond()
            => WaitForElementToAppear(_waitText);

        public DigiDAppDeactiverenPagina_AP077 DeActiverenBevestigen()
            => TapOn(_okButton);

        

    }

}
