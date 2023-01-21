using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class KiesUwPincodePagina_AP006 : Pageobject<KiesUwPincodePagina_AP006>
    {
        private const string _title = "Kies uw pincode";
        private const string _subtitle = "Herhaal uw pincode";
        private const string _waitText = "Kies een pincode om mee in te loggen.";
        private const string _geenSMSOntvangenButton = "Geen sms-code ontvangen";
        private const string _sluitenButton = "Sluiten";
        private const string _helpButton = "Extra informatie Kies uw pincode";
        private const string _titleHelpPagina = "Kies uw pincode";
        private const string _gelezenButton = "Gelezen";

        private KiesUwPincodePagina_AP006(string title) : base(title, _waitText) { }

        public static KiesUwPincodePagina_AP006 Load(string title = _title)
            => new KiesUwPincodePagina_AP006(title);

        public KiesUwPincodePagina_AP006 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public KiesUwPincodePagina_AP006 OpenenHelpPagina()
            => TapOn(_helpButton);

        public KiesUwPincodePagina_AP006 SluitHelpPagina()
            => TapOn(_gelezenButton);

        public KiesUwPincodePagina_AP006 ControleerTekstHelpPagina()
            => WaitForElementToAppear(_titleHelpPagina);

        public KiesUwPincodePagina_AP006 EnterPin(string pincode) => EnterPin(pincode.ToCharArray().Select(c => c.ToString()).ToArray());

        public KiesUwPincodePagina_AP006 EnterPin(string[] pincode)
        {
            foreach (string cijfer in pincode) TapOn(cijfer);
            return this;
        }

        public KiesUwPincodePagina_AP006 GeldigePincode(string pincode = "SSSSS")
        {
            return WaitForElementToAppear(_title)
                .EnterPin(pincode)
                .WaitForTextToAppear(_subtitle)
                .EnterPin(pincode);
                
        }


    }



}
