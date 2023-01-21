using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace DigiD.UITests.Pageobjects
{
    public class PincodeGewijzigdPagina_AP075 : Pageobject<PincodeGewijzigdPagina_AP075>
    {
        private const string _title = "Pincode gewijzigd";
        //private const string _waitText = "De pincode van uw DigiD app is succesvol gewijzigd.";

        static Xquery _waitText = new Xquery()
        {
            Android = c => c.Marked("De pincode van uw DigiD app is succesvol gewijzigd."),
            iOS = c => c.Text("De pincode van uw DigiD app is succesvol gewijzigd.")
        };
        private const string _knopOK = "OK";
        
        private PincodeGewijzigdPagina_AP075(string title) : base(title, _waitText) { }

        public static PincodeGewijzigdPagina_AP075 Load(string title = _title)
            => new PincodeGewijzigdPagina_AP075(title);

        public PincodeGewijzigdPagina_AP075 ControleerOfJuisteTekstWordtGetoond()
            => WaitForElementToAppear(_waitText);

        public PincodeGewijzigdPagina_AP075 PincodeGewijzigdAkkoord()
            => TapOn(_knopOK);



    }
}
