using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class PincodeInvoerenPagina_AP016 : Pageobject<PincodeInvoerenPagina_AP016>
    {
        private const string _title = "Pincode invoeren";
        private const string _titleBijWijzigen = "Pincode wijzigen";
        private const string _waitText = "Voer pincode in om in te loggen.";
        private const string _sluitenButton = "Sluiten";
        private const string _helpButton = "Extra informatie Pincode invoeren";
        private const string _titleHelpPagina = "Pincode invoeren";
        private const string _gelezenButton = "Gelezen";

        private PincodeInvoerenPagina_AP016(string title) : base(title, _waitText) { }

        public static PincodeInvoerenPagina_AP016 Load(string title = _title)
            => new PincodeInvoerenPagina_AP016(title);

        public PincodeInvoerenPagina_AP016 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public PincodeInvoerenPagina_AP016 OpenenHelpPagina()
            => TapOn(_helpButton);

        public PincodeInvoerenPagina_AP016 SluitHelpPagina()
            => TapOn(_gelezenButton);

        public PincodeInvoerenPagina_AP016 ControleerTekstHelpPagina()
            => WaitForElementToAppear(_titleHelpPagina);

        public PincodeInvoerenPagina_AP016 EnterPin(string pincode) => EnterPin(pincode.ToCharArray().Select(c => c.ToString()).ToArray());

        public PincodeInvoerenPagina_AP016 EnterPin(string[] pincode)
        {
            foreach (string cijfer in pincode) TapOn(cijfer);
            return this;
        }

        public PincodeInvoerenPagina_AP016 GeldigePincode(string pincode = "SSSSS")
        {
            return WaitForElementToAppear(_title)
                .EnterPin(pincode);
                

        }

        public PincodeInvoerenPagina_AP016 PincodeWijzigen(string pincode = "SSSSS")
        {
            return WaitForElementToAppear(_titleBijWijzigen)
                .EnterPin(pincode);


        }


    }



}
