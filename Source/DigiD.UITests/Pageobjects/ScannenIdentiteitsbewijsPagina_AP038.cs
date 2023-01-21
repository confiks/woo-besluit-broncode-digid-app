using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class ScannenIdentiteitsbewijsPagina_AP038 : Pageobject<ScannenIdentiteitsbewijsPagina_AP038>
    {
        private const string _title = "Scannen identiteitsbewijs";
        static Xquery _waitText = new Xquery()
        //{
        //    Android = c => c.Marked("Bezig met scannen van identiteitsbewijs"),

        //    iOS = c => c.Text("Houd het midden van het identiteitsbewijs tegen de bovenkant van de iPhone.")
        //};

        {
            //Android = c => c.Marked("Houd het identiteitsbewijs tegen de achterkant van de smartphone."),
            Android = c => c.Marked("Bezig met scannen van identiteitsbewijs"),

            iOS = c => c.Text("Houd het midden van het identiteitsbewijs tegen de bovenkant van de iPhone.")



        };

        private const string _helpButton = "Extra informatie Scannen identiteitsbewijs";
        private const string _titleHelpPagina = "ID-check";
        private const string _gelezenButton = "Gelezen";
        private const string _startScannen = "Start scannen";
        private const string _bezigMetScannen = "Bezig met scannen van identiteitsbewijs";


        private ScannenIdentiteitsbewijsPagina_AP038(string title) : base(title, _waitText) { }

        public static ScannenIdentiteitsbewijsPagina_AP038 Load(string title = _title)
            => new ScannenIdentiteitsbewijsPagina_AP038(title);

        public ScannenIdentiteitsbewijsPagina_AP038 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);

        public ScannenIdentiteitsbewijsPagina_AP038 OpenenHelpPagina()
            => TapOn(_helpButton);

        public ScannenIdentiteitsbewijsPagina_AP038 ControleerTekstHelpPagina()
            => WaitForElementToAppear(_titleHelpPagina);

        public ScannenIdentiteitsbewijsPagina_AP038 SluitHelpPagina()
            => TapOn(_gelezenButton);

        public ScannenIdentiteitsbewijsPagina_AP038 StartScannen()
        {
            // Here we can fill platform specific locators that have a different locator on iOS than on Android
            if (OnAndroid)
            {
                WaitForElementToAppear(_bezigMetScannen);
            }
            if (OniOS)
            {
                TapOn(_startScannen);
            }

            return this;
        }
    }
}
