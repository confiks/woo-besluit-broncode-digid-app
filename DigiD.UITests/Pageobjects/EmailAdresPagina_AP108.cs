using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class EmailAdresPagina_AP108 : Pageobject<EmailAdresPagina_AP108>
    {
        private const string _title = "E-mailadres";
        private const string _waitText= "E-mailadres";
        private Query _informatie = x => x.Raw("* {text CONTAINS 'U heeft dit e-mailadres nog niet bevestigd.'}");
        private const string _overslaanButton = "Overslaan";
        private const string _nieuweCodeSturenButton = "Nieuwe code sturen";
        private const string _helpButton = "Extra informatie E-mailadres";
        
        private EmailAdresPagina_AP108(string title) : base(title, _waitText) { }

        public static EmailAdresPagina_AP108 Load(string title = _title)
            => new EmailAdresPagina_AP108(title);

        public EmailAdresPagina_AP108 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public EmailAdresPagina_AP108 OpenenHelpPagina()
            => TapOn(_helpButton);

        public EmailAdresPagina_AP108 ControleerTekstHelpPagina()
            => WaitForElementToAppear(_waitText);

        public EmailAdresPagina_AP108 VulControleCodeEmail()
              => EnterText("SSSSSSSSS");


        public EmailAdresPagina_AP108 ActiveerKnopOverslaan()
            => TapOn(_overslaanButton);

        public EmailAdresPagina_AP108 NieuweCodeSturen()
            => TapOn(_nieuweCodeSturenButton);

    }
}
