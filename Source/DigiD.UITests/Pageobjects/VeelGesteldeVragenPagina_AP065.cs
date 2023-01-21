using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace DigiD.UITests.Pageobjects
{
    public class VeelGesteldeVragenPagina_AP065: Pageobject<VeelGesteldeVragenPagina_AP065>
    {
        private const string _title = "Veelgestelde vragen";
        private const string _waitText = "Veelgestelde vragen";
        private Query _informatie = x => x.Raw("* {text CONTAINS 'Hoe activeer ik de DigiD app?'}");
        private const string _sluitButton = "Huidige actie annuleren";
        
        private VeelGesteldeVragenPagina_AP065(string title) : base(title, _waitText) { }

        public static VeelGesteldeVragenPagina_AP065 Load(string title = _title)
            => new VeelGesteldeVragenPagina_AP065(title);

        public VeelGesteldeVragenPagina_AP065 ControleerOfJuisteTekstWordtGetoond()
            => WaitForElementToAppear(_informatie);

        public VeelGesteldeVragenPagina_AP065 VorigePagina()
            => Back();


    }

}
