using Belastingdienst.MCC.TestAAP.Model;
using DigiD.UITests.Pageobjects;
using NUnit.Framework;
using Shouldly;
using Xamarin.UITest;

namespace DigiD.UITests.RegressionTests
{

    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class WhatsNewTest_Page2 : Ap2TestBase
    {

        public WhatsNewTest_Page2() : this(Platform.iOS) { }
        public WhatsNewTest_Page2(Platform platform) : this(platform, AppOrientation.Portrait) { }
        public WhatsNewTest_Page2(Platform platform, AppOrientation appOrientation) : base(platform, appOrientation) { }



        // Reuseable reference to the page that we are testing.
        WhatsNewPage2 _page;


        // Initialises the test by executing the following before each test.<para/>
        // Note: The underlying setup in any base classes does not need to be called explicitly,
        // the NUunit framework does this and works bottom up.
        [SetUp]
        public void InitTest()
        {
            //laad pagina 1
            WhatsNewPage1.Load()
               .GaNaarVolgendePagina();

            _page = WhatsNewPage2
                    .Load();
        }

        [Test]
        public void ControleerWeergaveWhatsNewPagina2()
        {
            //app.Repl();
            _page.ControleerOfJuisteTekstWordtGetoond();
            _page.GaNaarVolgendePagina();
        }


    }
}
