using System;
using System.Linq;
using Belastingdienst.MCC.TestAAP.Commons;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace DigiD.UITests.Pageobjects
{
    public class OpenSourceBibliothekenPagina_AP085 : Pageobject<OpenSourceBibliothekenPagina_AP085>
    {
        private const string _title = "Open-source bibliotheken";
        private const string _waitText = "Open-source bibliotheken";
        private const string _naarVoorgaandePagina = "Vorige";

        private OpenSourceBibliothekenPagina_AP085(string title) : base(title, _waitText) { }

        public static OpenSourceBibliothekenPagina_AP085 Load(string title = _title)
            => new OpenSourceBibliothekenPagina_AP085(title);

        //public OpenSourceBibliothekenPagina_AP085 VorigePagina()
        //    => TapOn(_naarVoorgaandePagina);

        public OpenSourceBibliothekenPagina_AP085 VorigePagina()
        {

            if (OnAndroid)
            {
                Back(); 
            }
            if (OniOS)
            {
                TapOn(_naarVoorgaandePagina);
            }

            return this;





        }



    }

}

