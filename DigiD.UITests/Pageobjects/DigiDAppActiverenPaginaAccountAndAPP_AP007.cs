using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class DigiDAppActiverenPaginaAccountAndApp_AP007 : Pageobject<DigiDAppActiverenPaginaAccountAndApp_AP007>
    {
        private const string _title = "DigiD app activeren";
        private const string _waitText = "Uw DigiD account en DigiD app zijn geactiveerd!";
        private const string _neeDankJeButton = "Nee, dank je";
        private const string __jaDoorgaanButton = "Ja, doorgaan";
        private const string _helpButton = "Extra informatie ";
        private const string _titleHelpPagina = "ID-check";
        private const string _gelezenButton = "Gelezen";


        private DigiDAppActiverenPaginaAccountAndApp_AP007(string title) : base(title, _waitText) { }

        public static DigiDAppActiverenPaginaAccountAndApp_AP007 Load(string title = _title)
            => new DigiDAppActiverenPaginaAccountAndApp_AP007(title);

        public DigiDAppActiverenPaginaAccountAndApp_AP007 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public DigiDAppActiverenPaginaAccountAndApp_AP007 OpenenHelpPagina()
            => TapOn(_helpButton);

        public DigiDAppActiverenPaginaAccountAndApp_AP007 SluitHelpPagina()
            => TapOn(_gelezenButton);

        public DigiDAppActiverenPaginaAccountAndApp_AP007 ControleerTekstHelpPagina()
            => WaitForElementToAppear(_titleHelpPagina);

        public DigiDAppActiverenPaginaAccountAndApp_AP007 ActiveerKnopNeeDankJe()
            => TapOn(_neeDankJeButton);

        public DigiDAppActiverenPaginaAccountAndApp_AP007 ActiveerKnopJaDoorgaan()
            => TapOn(__jaDoorgaanButton);

    }
}
