using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class ScannenIdentiteitsbewijsPagina_AP107 : Pageobject<ScannenIdentiteitsbewijsPagina_AP107>
    {
        private const string _title = "Scannen identiteitsbewijs";
        private const string _waitText = "In de volgende stap scant u de chip in uw identiteitsbewijs met uw telefoon. Dit doet u door uw identiteitsbewijs tegen de achterkant van uw telefoon te houden.";
        private const string _volgendeButton = "Volgende";


        private ScannenIdentiteitsbewijsPagina_AP107(string title) : base(title, _waitText) { }

        public static ScannenIdentiteitsbewijsPagina_AP107 Load(string title = _title)
            => new ScannenIdentiteitsbewijsPagina_AP107(title);

        public ScannenIdentiteitsbewijsPagina_AP107 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public ScannenIdentiteitsbewijsPagina_AP107 ActiveerKnopVolgende()
            => TapOn(_volgendeButton);
    }

}
