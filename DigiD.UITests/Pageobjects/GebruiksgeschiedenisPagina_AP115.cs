using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace DigiD.UITests.Pageobjects
{
    public class GebruiksgeschiedenisPagina_AP115 : Pageobject<GebruiksgeschiedenisPagina_AP115>
    {
        private const string _title = "Gebruiksgeschiedenins";
        private const string _waitText = "Gebruiksgeschiedenis";
        private const string _sluitButton = "Huidige actie annuleren";

        private GebruiksgeschiedenisPagina_AP115(string title) : base(title, _waitText) { }

        public static GebruiksgeschiedenisPagina_AP115 Load(string title = _title)
            => new GebruiksgeschiedenisPagina_AP115(title);

        public GebruiksgeschiedenisPagina_AP115 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public GebruiksgeschiedenisPagina_AP115 PaginaSluiten()
           => TapOn(_sluitButton);

    }
}
