﻿using System;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace DigiD.UITests.Pageobjects
{
    public class DigiDAppActiverenPagina_AP043 : Pageobject<DigiDAppActiverenPagina_AP043>
    {
        private const string _title = "DigiD app activeren";
        private const string _waitText = "Log in met uw DigiD";
        private const string _geenDigiDButton = "Geen DigiD";
        private const string _volgendeButton = "Volgende";
        private const string _sluitenButton = "Sluiten";
        private const string _helpButton = "Extra informatie DigiD app activeren";
        private const string _titleHelpPagina = "Inloggen met DigiD";
        private const string _sluitenHelpPagina = "Annuleren";
        private const string _gelezenButton = "Gelezen";
        private Query _invulveldGebruikersnaam = x => x.Marked("Gebruikersnaam");
        //private const string _invulveldGebruikersnaam = "Gebruikersnaam";
        private Query _invulveldWachtwoord = x => x.Marked("Wachtwoord");



        private DigiDAppActiverenPagina_AP043(string title) : base(title, _waitText) { }

        public static DigiDAppActiverenPagina_AP043 Load(string title = _title)
            => new DigiDAppActiverenPagina_AP043(title);

        public DigiDAppActiverenPagina_AP043 ControleerOfJuisteTekstWordtGetoond()
           => WaitForElementToAppear(_waitText);


        public DigiDAppActiverenPagina_AP043 ActiveerKnopGeenDigiD()
            => TapOn(_geenDigiDButton);

        public DigiDAppActiverenPagina_AP043 Volgende()
            => TapOn(_volgendeButton);

        public DigiDAppActiverenPagina_AP043 OpenenHelpPagina()
            => TapOn(_helpButton);
            

        public DigiDAppActiverenPagina_AP043 SluitHelpPagina()
           => TapOn(_sluitenHelpPagina);

             
        public DigiDAppActiverenPagina_AP043  ControleerTekstHelpPagina()
            => WaitForElementToAppear(_titleHelpPagina);



        public DigiDAppActiverenPagina_AP043 EnterGebruikersnaam(string demoUser)
            => WaitForElementToAppear(_invulveldGebruikersnaam)
                .Tap(_invulveldGebruikersnaam)
                .EnterText(demoUser)
                .DismissKeyboard();
                
                


        public DigiDAppActiverenPagina_AP043 EnterWachtwoord(string wachtwoord)
              => WaitForElementToAppear(_invulveldWachtwoord)
                    .Tap(_invulveldWachtwoord)
                    .EnterText(wachtwoord)
                    .DismissKeyboard();

        public DigiDAppActiverenPagina_AP043 Inloggen(string demoUser, string wachtwoord)
                => EnterGebruikersnaam(demoUser)
                     .EnterWachtwoord(wachtwoord)
                     .TapOn(_volgendeButton);
                    

    }



}
