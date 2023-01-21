using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{

    public class ControleerEmailadres_AP112 : Pageobject<ControleerEmailadres_AP112>

    {
        private const string _title = "Controleer uw e-mailadres";
        private const string _waitText = "Controleer uw e-mailadres";
        private  Query _informatie = x => x.Raw("* {text CONTAINS 'U heeft langere tijd uw e-mailadres niet aangepast via de app of in Mijn DigiD.'}"); 
        private const string _neeknop = "Nee";
        private const string _jaknop = "Ja";

        private ControleerEmailadres_AP112(string title) : base(title, _waitText) { }

        public static ControleerEmailadres_AP112 Load(string title = _title)
            => new ControleerEmailadres_AP112(title);

        public ControleerEmailadres_AP112 ControleerOfJuisteTekstWordtGetoond()
            => WaitForElementToAppear(_informatie);

        public ControleerEmailadres_AP112 MailAdresBevestigen()
            => TapOn(_jaknop);

        public ControleerEmailadres_AP112 MailAdresNietBevestigen()
            => TapOn(_neeknop);





    }



}
