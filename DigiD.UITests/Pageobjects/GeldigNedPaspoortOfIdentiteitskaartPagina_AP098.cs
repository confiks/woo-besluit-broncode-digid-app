using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class GeldigNedPaspoortOfIdentiteitskaartPagina_AP098 : Pageobject<GeldigNedPaspoortOfIdentiteitskaartPagina_AP098>
    {
        private const string _title = "Heeft u een geldig Nederlands paspoort of identiteitskaart?";
        private const string _waitText = "Heeft u een geldig Nederlands paspoort of identiteitskaart?";
        private const string _jaButton = "Ja";
        private const string _neeButton = "Nee";


        private GeldigNedPaspoortOfIdentiteitskaartPagina_AP098(string title) : base(title, _waitText) { }

        public static GeldigNedPaspoortOfIdentiteitskaartPagina_AP098 Load(string title = _title)
            => new GeldigNedPaspoortOfIdentiteitskaartPagina_AP098(title);

        public GeldigNedPaspoortOfIdentiteitskaartPagina_AP098 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public GeldigNedPaspoortOfIdentiteitskaartPagina_AP098 ActiveerKnopJa()
            => TapOn(_jaButton);

        public GeldigNedPaspoortOfIdentiteitskaartPagina_AP098 ActiveerKnopNee()
            => TapOn(_neeButton);



    }



}
