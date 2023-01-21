using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace DigiD.UITests.Pageobjects
{
    public class MijnDigiDPagina_AP106 : Pageobject<MijnDigiDPagina_AP106>
    {
        private const string _title = "Mijn DigiD";
        private const string _waitText = "Overige gegevens";
        //private const string _emailadresButton = "E-mailadres";
        private Query _emailadresButton = x => x.Raw("* {text CONTAINS 'E-mailadres'}");
        private const string _naarMijnDigiDButton = "Naar Mijn DigiD";
        private Query _gebruiksgeschiedenisButton = x => x.Raw("* {text CONTAINS 'Gebruiksgeschiedenis'}");
        private Query _pincodeWijzigenButton = x => x.Raw("* {text CONTAINS 'Pincode wijzigen'}");




        private MijnDigiDPagina_AP106(string title) : base(title, _waitText) { }

        public static MijnDigiDPagina_AP106 Load(string title = _title)
            => new MijnDigiDPagina_AP106(title);

        public MijnDigiDPagina_AP106 EmailAdresToevoegen()
           => TapOn(_emailadresButton);

        public MijnDigiDPagina_AP106 GebruiksgeschiedenisOpenen()
           => TapOn(_gebruiksgeschiedenisButton);

        public MijnDigiDPagina_AP106 OpenPincodeWijzigen()
            => TapOn(_pincodeWijzigenButton);



    }
}
