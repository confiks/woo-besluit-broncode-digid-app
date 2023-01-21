using System;
using Belastingdienst.MCC.TestAAP.Commons;

namespace DigiD.UITests.Pageobjects
{
    public class WhatsNewPage2 : Pageobject<WhatsNewPage2>
    {
        private const string _title = "Bijwerken e-mailadres";
        private const string _waitText = "Bijwerken e-mailadres";
        public const string _WhatsNewTekst = "Voeg uw e-mailadres toe aan uw DigiD of wijzig dit in de app. Dit doet u in het menu onder 'Mijn DigiD'.";
        private const string _volgendeButton = "Volgende";
        private const string _vorigeButton = "Vorige";
        private const string _sluitenButton = "Sluiten";

        private WhatsNewPage2(string title) : base(title, _waitText) { }

        public static WhatsNewPage2 Load(string title = _title)
            => new WhatsNewPage2(title);

        public WhatsNewPage2 ControleerOfJuisteTekstWordtGetoond()
            => WaitForElementToAppear(_WhatsNewTekst);

        public WhatsNewPage2 GaNaarVolgendePagina()
            => TapOn(_volgendeButton);

        public WhatsNewPage2 GaNaarVorigePagina()
            => TapOn(_vorigeButton);

        public WhatsNewPage2 SluitPagina()
           => TapOn(_sluitenButton);


    }
}
