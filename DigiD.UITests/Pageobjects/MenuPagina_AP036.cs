using System;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class MenuPagina_AP036 : Pageobject<MenuPagina_AP036>
    {
        private const string _title = "DigiD logo";
        private const string _waitText = "ID-check";
        private const string _idCheckButton = "ID-check";
        private Query _mijnDigiDButton = x => x.Raw("* {text CONTAINS 'Mijn DigiD'}");
        private Query _instellingenButton = x => x.Raw("* {text CONTAINS 'Instellingen'}");
        private Query _hulpEnInfoButton = x => x.Raw("* {text CONTAINS 'Hulp & Info'}");
        private Query _berichtenButton = x => x.Raw("* {text CONTAINS 'Berichten'}");


        private MenuPagina_AP036(string title) : base(title, _waitText) { }


        public static MenuPagina_AP036 Load(string title = _title)
            => new MenuPagina_AP036(title);

        public static MenuPagina_AP036 LoadNaActivatie(string title = _waitText)
             => new MenuPagina_AP036(title);

        public MenuPagina_AP036()
           => WaitForElementToAppear(_waitText);

        public MenuPagina_AP036 StartIDCheck()
            => TapOn(_idCheckButton);

        public MenuPagina_AP036 OpenBerichtenPagina()
           => TapOn(_berichtenButton);

        public MenuPagina_AP036 OpenMijnDigiD()
           => TapOn(_mijnDigiDButton);

        public MenuPagina_AP036 OpenInstellingenPagina()
           => TapOn(_instellingenButton);

        public MenuPagina_AP036 OpenHulpEnInfoPagina()
           => TapOn(_hulpEnInfoButton);

    }
}

