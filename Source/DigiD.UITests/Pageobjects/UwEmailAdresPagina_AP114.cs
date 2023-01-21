using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace DigiD.UITests.Pageobjects
{
    public class UwEmailAdresPagina_AP114 : Pageobject<UwEmailAdresPagina_AP114>
    {
        private const string _title = "Uw e-mailadres";
        private const string _waitText = "Uw e-mailadres";
        private Query _informatie = x => x.Raw("* {text CONTAINS 'Het e-mailadres dat wij van u geregistreerd hebben is:'}");
        private const string _neeButton = "Nee";
        private const string _jaButton = "Ja";




        private UwEmailAdresPagina_AP114(string title) : base(title, _waitText) { }

        public static UwEmailAdresPagina_AP114 Load(string title = _title)
            => new UwEmailAdresPagina_AP114(title);

        public UwEmailAdresPagina_AP114 ControleerOfJuisteTekstWordtGetoond()
            => WaitForElementToAppear(_informatie);


        public UwEmailAdresPagina_AP114 EmailAdresAkkoord()
           => TapOn(_jaButton);

        public UwEmailAdresPagina_AP114 EmailAdresNietAkkoord()
           => TapOn(_neeButton);

    }
}
