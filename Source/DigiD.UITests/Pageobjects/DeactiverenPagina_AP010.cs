using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace DigiD.UITests.Pageobjects
{
    public class DeactiverenPagina_AP010 : Pageobject<DeactiverenPagina_AP010>
    {
        private const string _title = "DigiD app deactiveren";
        private const string _waitText = "DigiD app deactiveren";
        private Query _informatie = x => x.Raw("* {text CONTAINS 'Let op! Als u de DigiD app deactiveert, kunt u er niet meer mee inloggen.'}");
        private const string _neeButton = "Nee";
        private const string _jaButton = "Ja";
        private const string _sluitButton = "Huidige actie annuleren";
        private Query _informatiePopUp = x => x.Raw("* {text CONTAINS 'U gaat nu de DigiD app deactiveren'}");
        private const string _bevestigenButton = "Bevestigen";
        private const string _annulerenButton = "Bevestigen";

        private DeactiverenPagina_AP010(string title) : base(title, _waitText) { }

        public static DeactiverenPagina_AP010 Load(string title = _title)
            => new DeactiverenPagina_AP010(title);

        public DeactiverenPagina_AP010 ControleerOfJuisteTekstWordtGetoond()
            => WaitForElementToAppear(_informatie);

        public DeactiverenPagina_AP010 DeActiverenBevestigen()
            => TapOn(_jaButton);

        public DeactiverenPagina_AP010 DeActiverenAnnuleren()
            => TapOn(_neeButton);

        public DeactiverenPagina_AP010 VorigePagina()
            => Back();

        public DeactiverenPagina_AP010 DeActiveren()
            => TapOn(_bevestigenButton);

        public DeactiverenPagina_AP010 NietDeActiveren()
            => TapOn(_annulerenButton);

        public DeactiverenPagina_AP010 ControleerOfJuisteTekstWordtGetoondInPopUp()
            => WaitForElementToAppear(_informatiePopUp);


    }

}
