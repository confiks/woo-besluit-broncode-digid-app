using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace DigiD.UITests.Pageobjects
{
    public class TaalPagina_AP096 : Pageobject<TaalPagina_AP096>
    {
        private const string _title = "Taal";
        private const string _waitText = "Taal";
        private const string _NederlandsButton = "Nederlands";
        private const string _EngelsButton = "English";
        private const string _titleEngels = "Language";
        private const string _sluitButton = "Huidige actie annuleren";


        private TaalPagina_AP096(string title) : base(title, _waitText) { }

        public static TaalPagina_AP096 Load(string title = _title)
            => new TaalPagina_AP096(title);

        public TaalPagina_AP096 ControleerOfJuisteTekstWordtGetoond()
            => WaitForElementToAppear(_waitText);

        public TaalPagina_AP096 TaalInEngels()
             => TapOn(_EngelsButton);

        public TaalPagina_AP096 TaalInNederlands()
             => TapOn(_NederlandsButton);

        public TaalPagina_AP096 ControleerWijzigingTaalEngels()
            => WaitForElementToAppear(_titleEngels);

        public TaalPagina_AP096 ControleerWijzigingTaalNederlands()
            => WaitForElementToAppear(_title);

        public TaalPagina_AP096 PaginaSluiten()
            =>  TapOn(_sluitButton);



    }
}
