using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace DigiD.UITests.Pageobjects
{
    public class DonkereModusPagina_AP097 : Pageobject<DonkereModusPagina_AP097>
    {
        private const string _title = "Donkere modus";
        private const string _waitText = "Bij 'automatisch' volgt de app vanzelf de donkere modus-instelling van uw mobiele apparaat.";
        private const string _DonkereModusAanButton = "Aan";
        private const string _DonkereModusUitButton = "Uit";
        private const string _DonkereModusAutomatischButton = "Automatisch";
        private const string _sluitButton = "Huidige actie annuleren";


        private DonkereModusPagina_AP097(string title) : base(title, _waitText) { }

        public static DonkereModusPagina_AP097 Load(string title = _title)
            => new DonkereModusPagina_AP097(title);

        public DonkereModusPagina_AP097 ControleerOfJuisteTekstWordtGetoond()
            => WaitForElementToAppear(_waitText);

        public DonkereModusPagina_AP097 DonkereModusAan()
             => TapOn(_DonkereModusAanButton);

        public DonkereModusPagina_AP097 DonkereModusUit()
             => TapOn(_DonkereModusUitButton);

        public DonkereModusPagina_AP097 DonkereModusAutomatisch()
             => TapOn(_DonkereModusAutomatischButton);

        public DonkereModusPagina_AP097 PaginaSluiten()
            => TapOn(_sluitButton);



    }
}
