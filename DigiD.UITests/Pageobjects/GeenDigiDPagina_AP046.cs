using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class GeenDigiDPagina_AP046 : Pageobject<GeenDigiDPagina_AP046>
    {
        private const string _title = "Geen DigiD";
        private const string _waitText = "Geen DigiD";
        private const string _ikWoonInBuitenlandButton = "Ik woon buiten Nederland";
        private const string _DigiDAanvragenButton = "DigiD aanvragen";
        private Query _informatie = x => x.Raw("* {text CONTAINS 'Als u geen DigiD heeft, kunt u direct in de app uw DigiD aanvragen.'}");


        private GeenDigiDPagina_AP046(string title) : base(title, _waitText) { }

        public static GeenDigiDPagina_AP046 Load(string title = _title)
            => new GeenDigiDPagina_AP046(title);

        public GeenDigiDPagina_AP046 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_informatie);

        public GeenDigiDPagina_AP046 ActiveerKnopBuitenland()
           => TapOn(_ikWoonInBuitenlandButton);

        public GeenDigiDPagina_AP046 ActiveerKnopDigiDAanvragen()
           => TapOn(_DigiDAanvragenButton);

    }
}
