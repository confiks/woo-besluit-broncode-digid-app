using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class WachtOpBriefStatusPagina_AP001 : Pageobject<WachtOpBriefStatusPagina_AP001>
    {
        private const string _title = "Wacht op brief";
        private const string _waitText = "Heeft u de brief ontvangen? Volg de aanwijzingen en gebruik de knop hieronder om de app te activeren.";
        private const string _geenBriefGehadButton = "Geen brief gehad";
        private const string _activerenButton = "Activeren";


        private WachtOpBriefStatusPagina_AP001(string title) : base(title, _waitText) { }

        public static WachtOpBriefStatusPagina_AP001 Load(string title = _title)
            => new WachtOpBriefStatusPagina_AP001(title);

        public WachtOpBriefStatusPagina_AP001 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public WachtOpBriefStatusPagina_AP001 ActiveerKnopActiveren()
            => TapOn(_activerenButton);

    }
}
