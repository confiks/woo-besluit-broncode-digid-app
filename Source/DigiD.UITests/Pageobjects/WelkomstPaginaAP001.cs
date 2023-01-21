using System;
using Belastingdienst.MCC.TestAAP.Commons;

namespace DigiD.UITests.Pageobjects
{
    public class WelkomstPaginaAP001 : Pageobject<WelkomstPaginaAP001>
    {
        private const string _title = "Welkom";
        private const string _waitText = "De makkelijkste manier om veilig in te loggen";
        public const string StatusWelkomstPagina = "De app moet nog geactiveerd worden.";
        private const string _startButton = "Start";
        
        private WelkomstPaginaAP001(string title) : base(title, _waitText) { }

        

        public static WelkomstPaginaAP001 Load(string title = _title)
            => new WelkomstPaginaAP001(title);

        public WelkomstPaginaAP001 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);


        public WelkomstPaginaAP001 ControleerStatusApp()
            => WaitForElementToAppear(StatusWelkomstPagina);

        public WelkomstPaginaAP001 StartActivatie()
            => TapOn(_startButton);



    }
}
