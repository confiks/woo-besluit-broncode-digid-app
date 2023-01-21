using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class DigiDAppActiverenPaginaLetter_AP007 : Pageobject<DigiDAppActiverenPaginaLetter_AP007>
    {
        private const string _title = "DigiD app activeren";
        private const string _waitText = "Uw DigiD app is geactiveerd!";
        private const string _neeDankJeButton = "Nee, dank je";
        private const string __jaDoorgaanButton = "Ja, doorgaan";
        private const string __begrepenButton = "Begrepen";
        private const string _helpButton = "Extra informatie ";
        private const string _titleHelpPagina = "ID-check";
        private const string _gelezenButton = "Gelezen";


        private DigiDAppActiverenPaginaLetter_AP007(string title) : base(title, _waitText) { }

        public static DigiDAppActiverenPaginaLetter_AP007 Load(string title = _title)
            => new DigiDAppActiverenPaginaLetter_AP007(title);

        public DigiDAppActiverenPaginaLetter_AP007 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public DigiDAppActiverenPaginaLetter_AP007 OpenenHelpPagina()
            => TapOn(_helpButton);

        public DigiDAppActiverenPaginaLetter_AP007 SluitHelpPagina()
            => TapOn(_gelezenButton);

        public DigiDAppActiverenPaginaLetter_AP007 ControleerTekstHelpPagina()
            => WaitForElementToAppear(_titleHelpPagina);

        public DigiDAppActiverenPaginaLetter_AP007 ActiveerKnopNeeDankJe()
            => TapOn(_neeDankJeButton);

        public DigiDAppActiverenPaginaLetter_AP007 ActiveerKnopJaDoorgaan()
            => TapOn(__jaDoorgaanButton);

        public DigiDAppActiverenPaginaLetter_AP007 ActiveerKnopBegrepen()
            => TapOn(__begrepenButton);

    }
}
