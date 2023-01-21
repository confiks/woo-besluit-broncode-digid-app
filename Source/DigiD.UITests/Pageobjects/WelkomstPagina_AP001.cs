 using System;
using Belastingdienst.MCC.TestAAP.Commons;

namespace DigiD.UITests.Pageobjects
{
    public class WelkomstPagina_AP001 : Pageobject<WelkomstPagina_AP001>
    {
        private const string _title = "Welkom";
        private const string _waitText = "De makkelijkste manier om veilig in te loggen";
        private const string _waitTextNaActivatie = "Ga naar een website en kies voor inloggen met de DigiD app.";
        public const string StatusWelkomstPagina = "De app moet nog geactiveerd worden.";
        private const string _StatusNaActivatie = "U heeft de app geactiveerd.";
        private const string _startButton = "Start";
        private const string _openMenuButton = "Open Menu pagina";

        private WelkomstPagina_AP001(string title) : base(title, _waitText) { }
        

        public static WelkomstPagina_AP001 Load(string title = _title)
            => new WelkomstPagina_AP001(title);

        public static WelkomstPagina_AP001 LoadNaActivatie(string title = _waitTextNaActivatie)
             => new WelkomstPagina_AP001(title);

        public WelkomstPagina_AP001 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);


        public WelkomstPagina_AP001 ControleerStatusApp()
            => WaitForElementToAppear(StatusWelkomstPagina);

        public WelkomstPagina_AP001 StartActivatie()
            => TapOn(_startButton);

        public WelkomstPagina_AP001 ControleerStatusNaActivatie()
            => WaitForElementToAppear(_StatusNaActivatie);

        public WelkomstPagina_AP001 OpenMenu()
            => TapOn(_openMenuButton);

    }
}
