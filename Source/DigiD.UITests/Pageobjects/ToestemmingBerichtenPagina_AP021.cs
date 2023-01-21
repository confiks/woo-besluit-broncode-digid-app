using System;
using System.Linq;
using System.Threading.Tasks;
using Belastingdienst.MCC.TestAAP.Extensions;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class ToestemmingBerichtenPagina_AP021 : Pageobject<ToestemmingBerichtenPagina_AP021>
    {
        private const string _title = "Toestemming berichten";
        static Xquery _waitText = new Xquery()
        {
            Android = c => c.Marked("Wij kunnen u via de app berichten sturen als er iets wijzigt in uw DigiD. Wilt u deze meldingen ontvangen?"),
            iOS = c => c.Text("Wij kunnen u via de app berichten sturen als er iets wijzigt in uw DigiD. Wilt u deze meldingen ontvangen?")
        };
        private const string _jaButton = "Ja";
        private const string _NeeButton = "Nee";


        


        private ToestemmingBerichtenPagina_AP021(string title) : base(title, _waitText) { }

        public static ToestemmingBerichtenPagina_AP021 Load(string title = _title)
            => new ToestemmingBerichtenPagina_AP021(title);

        public ToestemmingBerichtenPagina_AP021 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public ToestemmingBerichtenPagina_AP021 ActiveerKnopNee()
            => TapOn(_NeeButton);

        public ToestemmingBerichtenPagina_AP021 ActiveerKnopJa()
            => TapOn(_jaButton);


    }



}
