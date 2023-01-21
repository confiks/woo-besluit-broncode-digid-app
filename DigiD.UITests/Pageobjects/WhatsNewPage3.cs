using System;
using Belastingdienst.MCC.TestAAP.Commons;

namespace DigiD.UITests.Pageobjects
{
    public class WhatsNewPage3 : Pageobject<WhatsNewPage3>
    {
        private const string _title = "Toegankelijkheid";
        private const string _waitText = "Toegankelijkheid";
        public const string _WhatsNewTekst = "We willen dat iedereen de DigiD app kan gebruiken. Daarom hebben we de toegankelijkheid verbeterd.";
        private const string _volgendeButton = "Volgende";
        private const string _vorigeButton = "Vorige";
        private const string _sluitenButton = "Sluiten";


        private WhatsNewPage3(string title) : base(title, _waitText) { }

        public static WhatsNewPage3 Load(string title = _title)
            => new WhatsNewPage3(title);

        public WhatsNewPage3 ControleerOfJuisteTekstWordtGetoond()
            => WaitForElementToAppear(_WhatsNewTekst);

        public WhatsNewPage3 GaNaarVolgendePagina()
            => TapOn(_volgendeButton);

        public WhatsNewPage3 GaNaarVorigePagina()
            => TapOn(_vorigeButton);

        public WhatsNewPage3 SluitPagina()
           => TapOn(_sluitenButton);


    }
}
