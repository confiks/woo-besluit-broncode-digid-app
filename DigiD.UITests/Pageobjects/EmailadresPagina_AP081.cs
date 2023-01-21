using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class EmailAdresPagina_AP081 : Pageobject<EmailAdresPagina_AP081>
    {
        private const string _title = "E-mailadres";
        private const string _waitText = "Voer de controlecode in die is verstuurd naar demo1@digid.nl. Sluit de app niet af.";
        private const string _overslaanButton = "Overslaan";
        private const string _geenMailOntvangenButton = "Geen e-mail ontvangen";
        private const string _helpButton = "Extra informatie E-mailadres";
        private const string _titleHelpPagina = "E-mail bevestigen";
        private const string _gelezenButton = "Gelezen";


        private EmailAdresPagina_AP081(string title) : base(title, _waitText) { }

        public static EmailAdresPagina_AP081 Load(string title = _title)
            => new EmailAdresPagina_AP081(title);

        public EmailAdresPagina_AP081 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public EmailAdresPagina_AP081 OpenenHelpPagina()
            => TapOn(_helpButton);

        public EmailAdresPagina_AP081 SluitHelpPagina()
            => TapOn(_gelezenButton);

        public EmailAdresPagina_AP081 ControleerTekstHelpPagina()
            => WaitForElementToAppear(_titleHelpPagina);

        public EmailAdresPagina_AP081 VulControleCodeEmail()
              => EnterText("SSSSSSSSS");


        public EmailAdresPagina_AP081 ActiveerKnopOverslaan()
            => TapOn(_overslaanButton);

        public EmailAdresPagina_AP081 ActiveerKnopGeenEmail()
            => TapOn(_geenMailOntvangenButton);

    }
}
