using System;
using Belastingdienst.MCC.TestAAP.Commons;

namespace DigiD.UITests.Pageobjects
{
    public class DigiDAppOpAnderApparaatPagina : Pageobject<DigiDAppOpAnderApparaatPagina>
    {
        private const string _title = "Gebruikt u de DigiD app al op een ander apparaat?";
        private const string _waitText = "Gebruikt u de DigiD app al op een ander apparaat?";
        private const string _jaButton = "Ja";
        private const string _NeeButton = "Nee";
        private const string _sluitButton = "Sluiten";

        private DigiDAppOpAnderApparaatPagina (string title) : base(title, _waitText) { }

        public static DigiDAppOpAnderApparaatPagina Load(string title = _title)
            => new DigiDAppOpAnderApparaatPagina (title);

        public DigiDAppOpAnderApparaatPagina ControleerOfJuisteTekstWordtGetoond()
            => WaitForElementToAppear(_title);

        public DigiDAppOpAnderApparaatPagina ActiveerKnopNee()
            => TapOn(_NeeButton);

        public DigiDAppOpAnderApparaatPagina ActiveerKnopJa()
            => TapOn(_jaButton);

        public DigiDAppOpAnderApparaatPagina ActiveerKnopSluiten()
            => TapOn(_sluitButton);


    }
}
