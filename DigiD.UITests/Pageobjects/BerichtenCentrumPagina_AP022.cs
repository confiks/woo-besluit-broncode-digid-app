using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace DigiD.UITests.Pageobjects
{
    public class BerichtenCentrumPagina_AP022 : Pageobject<BerichtenCentrumPagina_AP022>
    {
        private const string _title = "Berichten";
        private const string _waitText = "Berichten";
        private const string _sluitButton = "Huidige actie annuleren";



        private BerichtenCentrumPagina_AP022(string title) : base(title, _waitText) { }

        public static BerichtenCentrumPagina_AP022 Load(string title = _title)
            => new BerichtenCentrumPagina_AP022(title);

        public BerichtenCentrumPagina_AP022 PaginaSluiten()
           => TapOn(_sluitButton);

    }
}
