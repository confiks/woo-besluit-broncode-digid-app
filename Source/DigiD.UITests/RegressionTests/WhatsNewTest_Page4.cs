using Belastingdienst.MCC.TestAAP.Model;
using DigiD.UITests.Pageobjects;
using NUnit.Framework;
using Shouldly;
using Xamarin.UITest;

namespace DigiD.UITests.RegressionTests
{

    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class WhatsNewTest_Page3 : Ap2TestBase
    {

        public WhatsNewTest_Page3() : this(Platform.iOS) { }
        public WhatsNewTest_Page3(Platform platform) : this(platform, AppOrientation.Portrait) { }
        public WhatsNewTest_Page3(Platform platform, AppOrientation appOrientation) : base(platform, appOrientation) { }



        // Reuseable reference to the page that we are testing.
        WhatsNewPage3 _page;


        // Initialises the test by executing the following before each test.<para/>
        // Note: The underlying setup in any base classes does not need to be called explicitly,
        // the NUunit framework does this and works bottom up.
        [SetUp]
        public void InitTest()
        {
           
            WhatsNewPage1.Load()
               .GaNaarVolgendePagina();

            WhatsNewPage2.Load()
                .GaNaarVolgendePagina();

            _page = WhatsNewPage3
                    .Load();
        }

        [Test]
        public void ControleerWeergaveWhatsNewPagina3()
        {
            //app.Repl();
            _page.ControleerOfJuisteTekstWordtGetoond();
            _page.SluitPagina();
        }


    }
}
