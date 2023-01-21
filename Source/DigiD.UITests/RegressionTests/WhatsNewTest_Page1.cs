using Belastingdienst.MCC.TestAAP.Model;
using DigiD.UITests.Pageobjects;
using NUnit.Framework;
using Shouldly;
using Xamarin.UITest;

namespace DigiD.UITests.RegressionTests
{

    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class WhatsNewTest_Page1 : Ap2TestBase
{

        public WhatsNewTest_Page1() : this(Platform.iOS) { }
        public WhatsNewTest_Page1(Platform platform) : this(platform, AppOrientation.Portrait) { }
        public WhatsNewTest_Page1(Platform platform, AppOrientation appOrientation) : base(platform, appOrientation) { }



        // Reuseable reference to the page that we are testing.
        WhatsNewPage1 _page;
        

        // Initialises the test by executing the following before each test.<para/>
        // Note: The underlying setup in any base classes does not need to be called explicitly,
        // the NUunit framework does this and works bottom up.
        [SetUp]
        public void InitTest()
        {
            //        ////Start - Welkom
            //        //WelkomPage.Load()
            //        //    .LoadTestScenario(_dataSet, _happyFlowBsnScenario)
            //        //    .Volgende();

            //        ////Pincode instellen
            //        //PinPage.Load()
            //        //    .SetValidPin();

            _page = WhatsNewPage1
                    .Load();
        }

    [Test]
    public void ControleerWeergaveWhatsNewPagina1()
    {
            _page.ControleerOfJuisteTekstWordtGetoond();
            _page.GaNaarVolgendePagina();
     }

   
    }
}
