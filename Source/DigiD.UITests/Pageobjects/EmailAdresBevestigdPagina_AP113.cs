using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace DigiD.UITests.Pageobjects
{
    public class EmailAdresBevestigdPagina_AP113 : Pageobject<EmailAdresBevestigdPagina_AP113>
    {
        private const string _title = "E-mailadres bevestigd";
        //private const string _waitText = "Uw e-mailadres is nu bevestigd.";
        static Xquery _waitText = new Xquery()
        {
            Android = c => c.Marked("Uw e-mailadres is nu bevestigd."),
            iOS = c => c.Text("Uw e-mailadres is nu bevestigd.")
        };
        private const string _okButton = "OK";
      
        private EmailAdresBevestigdPagina_AP113(string title) : base(title, _waitText) { }

        public static EmailAdresBevestigdPagina_AP113 Load(string title = _title)
            => new EmailAdresBevestigdPagina_AP113(title);

        public EmailAdresBevestigdPagina_AP113 ControleerOfJuisteTekstWordtGetoond()
            => WaitForElementToAppear(_waitText);

        public EmailAdresBevestigdPagina_AP113 ActiveerKnopOK()
             => TapOn(_okButton);

    }
}
