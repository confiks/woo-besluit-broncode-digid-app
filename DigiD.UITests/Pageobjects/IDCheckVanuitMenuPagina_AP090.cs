using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace DigiD.UITests.Pageobjects
{
    public class IDCheckVanuitMenuPagina_AP090 : Pageobject<IDCheckVanuitMenuPagina_AP090>
    {
        private const string _title = "ID-check";
        private const string _waitText = "ID-check";
        private Query _informatie = x => x.Raw("* {text CONTAINS 'Sommige gegevens zijn extra privacygevoelig, bijvoorbeeld informatie over uw gezondheid.'}");
        private const string _startIDCheckButton = "Start ID-check met de DigiD app";
        private const string _sluitButton = "Huidige actie annuleren";



        private IDCheckVanuitMenuPagina_AP090(string title) : base(title, _waitText) { }

        public static IDCheckVanuitMenuPagina_AP090 Load(string title = _title)
            => new IDCheckVanuitMenuPagina_AP090(title);

        public IDCheckVanuitMenuPagina_AP090 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_informatie);

        public IDCheckVanuitMenuPagina_AP090 StartIDCheck()
           => TapOn(_startIDCheckButton);

        public IDCheckVanuitMenuPagina_AP090 PaginaSluiten()
           => TapOn(_sluitButton);

    }
}
