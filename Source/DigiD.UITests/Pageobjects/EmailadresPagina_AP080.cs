using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class EmailAdresPagina_AP080 : Pageobject<EmailAdresPagina_AP080>
    {
        private const string _title = "E-mailadres";
        private const string _waitText = "Vul het e-mailadres in waarop wij u kunnen bereiken. Er wordt een controlecode gestuurd naar dit adres.";
        private const string _overslaanButton = "Overslaan";
        private const string _volgendeButton = "Volgende";
        private const string _helpButton = "Extra informatie E-mailadres";
        private const string _titleHelpPagina = "Uw e-mailadres";
        private const string _gelezenButton = "Gelezen";
        private const string _JaButton = "Ja";
        private const string _NeeButton = "Nee";
        //private const string _invulveldEmailAdres = " Tooltip info";
        static Xquery _invulveldEmailAdres = new Xquery()
        {
            Android = c => c.Marked(" Tooltip info"),
            iOS = c => c.Id("E-mailadres")
        };
        private Query _geenMailadresPopUp = x => x.Raw("* {text CONTAINS 'Geen e-mailadres ingevuld'}");



        private EmailAdresPagina_AP080(string title) : base(title, _waitText) { }

        public static EmailAdresPagina_AP080 Load(string title = _title)
            => new EmailAdresPagina_AP080(title);

        public EmailAdresPagina_AP080 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public EmailAdresPagina_AP080 OpenenHelpPagina()
            => TapOn(_helpButton);

        public EmailAdresPagina_AP080 SluitHelpPagina()
            => TapOn(_gelezenButton);

        public EmailAdresPagina_AP080 ControleerTekstHelpPagina()
            => WaitForElementToAppear(_titleHelpPagina);

        public EmailAdresPagina_AP080 VulMailAdres()
            => WaitForElementToAppear(_invulveldEmailAdres)
               .Tap(_invulveldEmailAdres)
               .EnterText("SSSSSSSSSSSSSS")
               .DismissKeyboard();
            

        public EmailAdresPagina_AP080 ActiveerKnopOverslaan()
            => TapOn(_overslaanButton);

        public EmailAdresPagina_AP080 ActiveerKnopVolgende()
            => TapOn(_volgendeButton);

        public EmailAdresPagina_AP080 WachtOpPopUp()
            => WaitForElementToAppear(_geenMailadresPopUp);

        public EmailAdresPagina_AP080 BevestigGeenMail()
           => TapOn(_JaButton);

        public EmailAdresPagina_AP080 AlsnogMail()
           => TapOn(_NeeButton);

    }



}
