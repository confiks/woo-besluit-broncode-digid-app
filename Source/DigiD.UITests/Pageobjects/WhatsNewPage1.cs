using System;
using Belastingdienst.MCC.TestAAP.Commons;
using Xamarin.UITest;

namespace DigiD.UITests.Pageobjects
{
    public class WhatsNewPage1 : Pageobject<WhatsNewPage1>
    {
        private const string _title = "Gebruiksgeschiedenis";
        private const string _waitText = "Gebruiksgeschiedenis";
        public const string _WhatsNewTekst = "Bekijk het gebruik van uw DigiD in de app. U vindt dit in het menu onder 'Mijn Digid'.";
        private const string _volgendeButton = "Volgende";
        private const string _vorigeButton = "Vorige";
        private const string _sluitenButton = "Sluiten";

        private WhatsNewPage1(string title) : base(title, _waitText) { }

        public WhatsNewPage1(IApp app)
        {
        }

        public static WhatsNewPage1 Load(string title = _title)
            => new WhatsNewPage1(title);

        public WhatsNewPage1 ControleerOfJuisteTekstWordtGetoond()
            => WaitForTextToDisappear(_WhatsNewTekst);

        public WhatsNewPage1 GaNaarVolgendePagina()
            => TapOn(_volgendeButton);

        public WhatsNewPage1 GaNaarVorigePagina()
            => TapOn(_vorigeButton);

        public WhatsNewPage1 SluitPagina()
           => TapOn(_sluitenButton);


    }
}
