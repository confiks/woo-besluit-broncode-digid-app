using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class DigiDAppActiverenPagina_AP069 : Pageobject<DigiDAppActiverenPagina_AP069>
    {
        private const string _title = "DigiD app activeren";
        private const string _waitText = "DigiD app activeren";
        private const string _overslaanButton = "Overslaan";
        private const string _herinnerenButton = "Herinneren";
        private Query _informatie = x => x.Raw("* {text CONTAINS 'U krijgt binnen 3 dagen een brief met activeringscode thuisgestuurd.'}");


        private DigiDAppActiverenPagina_AP069(string title) : base(title, _waitText) { }

        public static DigiDAppActiverenPagina_AP069 Load(string title = _title)
            => new DigiDAppActiverenPagina_AP069(title);

        public DigiDAppActiverenPagina_AP069 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_informatie);

        public DigiDAppActiverenPagina_AP069 ActiveerKnopOverslaan()
            => TapOn(_overslaanButton);

        public DigiDAppActiverenPagina_AP069 ActiveerKnopHerinneren()
            => TapOn(_herinnerenButton);


    }



}
