﻿using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace DigiD.UITests.Pageobjects
{
    public class InstellingenPagina_AP068 : Pageobject<InstellingenPagina_AP068>
    {
        private const string _title = "Instellingen";
        private const string _waitText = "Mogen wij uw anonieme gegevens verzamelen, zodat wij deze app kunnen verbeteren?";
        private Query _taalButton = x => x.Raw("* {text CONTAINS 'Taal'}");
        private Query _donkereModusButton = x => x.Raw("* {text CONTAINS 'Donkere modus'}");
        
        private Query _deactiverenButton = x => x.Raw("* {text CONTAINS 'Deactiveren'}");
        private const string _sluitButton = "Huidige actie annuleren";


        private InstellingenPagina_AP068(string title) : base(title, _waitText) { }

        public static InstellingenPagina_AP068 Load(string title = _title)
            => new InstellingenPagina_AP068(title);

        public InstellingenPagina_AP068 ControleerOfJuisteTekstWordtGetoond()
            => WaitForElementToAppear(_waitText);

        public InstellingenPagina_AP068 OpenTaal()
             => TapOn(_taalButton);

        public InstellingenPagina_AP068 OpenDonkereModus()
             => TapOn(_donkereModusButton);

        public InstellingenPagina_AP068 PaginaSluiten()
             => TapOn(_sluitButton);



        public InstellingenPagina_AP068 Deactiveren()
            => TapOn(_deactiverenButton);

        
    }
}
