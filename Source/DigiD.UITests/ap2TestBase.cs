using System;
using Belastingdienst.MCC.TestAAP.Commons;
using Belastingdienst.MCC.TestAAP.Model;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Xamarin.UITest;

namespace DigiD.UITests
{
    /// <summary>
    /// Tip: An easy way to centrally run only one platform is use (a combination of) environment variables
    /// as symbols in compiler options, but don't forget to change temporary changes back!
    /// No options selected will run IOS and require a default contructor with no parameters.
    /// Normally IOS and ANDROID are suffient, but being able to run all tests in LANDSCAPE can also be handy.
    /// </summary>

#if IOS
    // Each Test Class will require a constructor with one parameter
    // public MyTest(Platform platform) : this(platform, AppOrientation.Portrait) { }
    [TestFixture(Platform.iOS)]
#endif

#if IOS_PORTRAIT
    // Each Test Class will require a constructor with 2 parameters
    // public MyTest(Platform platform, AppOrientation orientation) : this(platform, orientation) { }
    [TestFixture(Platform.iOS, AppOrientation.Portrait)]
#endif

#if IOS_LANDSCAPE
    // Each Test Class will require a constructor with 2 parameters
    // public MyTest(Platform platform, AppOrientation orientation) : this(platform, orientation) { }
    TestFixture(Platform.iOS, AppOrientation.Landscape)]
#endif

#if ANDROID
    // Each Test Class will require a constructor with one parameter
    // public MyTest(Platform platform) : this(platform, AppOrientation.Portrait) { }
    [TestFixture(Platform.Android)]
#endif

#if ANDROID_PORTRAIT
    // Each Test Class will require a constructor with 2 parameters
    // public MyTest(Platform platform, AppOrientation orientation) : this(platform, orientation) { }
    [TestFixture(Platform.Android, AppOrientation.Portrait)]
#endif

#if ANDROID_LANDSCAPE
    // Each Test Class will require a constructor with 2 parameters
    // public MyTest(Platform platform, AppOrientation orientation) : this(platform, orientation) { }
    [TestFixture(Platform.iOS, AppOrientation.Landscape)]
#endif

    public class Ap2TestBase : SmartTestBase
    {
        /// <summary>
        /// Show Repl when test fails - handy for debugging tests, default false - change with compile option CALL_REPL_ON_FAIL
        /// </summary>
        protected bool CallReplOnFailureEnabled { get; set; }

        public Ap2TestBase() : this(Platform.iOS, AppOrientation.Portrait) { }
        public Ap2TestBase(Platform platform) : this(platform, AppOrientation.Portrait) { }
        public Ap2TestBase(Platform platform, AppOrientation orientation, bool oneTimeSetup = false) : base(platform, orientation)
        {
            // Tip: An easy way to centrally opt in to show Repl when debugging tests is to set this to true, but don't forget to change it back!
            // .... or use the compile option CALL_REPL_ON_FAIL.
            CallReplOnFailureEnabled = false;

#if CALL_REPL_ON_FAIL
            CallReplOnFailureEnabled = true;
#endif
            #if DEBUG
            // Give extra visual help during development for debugging
            IsDebugmode = true && !TestEnvironment.IsTestCloud;
            #endif
        }

        [OneTimeSetUp]
        public override void BeforeFirstTest()
        {
            try
            {
                if (!StartAppPerTest)
                {
                    //Start app
                    app = null; //ensure always empty, by evt. exception
                    app = AppInitializer.StartApp(platform);
                }
                base.BeforeFirstTest();
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot start app on {platform}", e);
            }
        }

        /// <summary>
        /// Before each test <para/>
        /// - Initialise and start the app <para/>
        /// - Set orientation <para/>
        /// - Set path "LocalScreenshotPath" for any local screenshots <para/>
        /// </summary>
        [SetUp]
        public override void BeforeEachTest()
        {
            try
            {
                if (StartAppPerTest)
                {
                    //Start app
                    app = null; //ensure always empty, by evt. exception
                    app = AppInitializer.StartApp(platform);
                }
                base.BeforeEachTest();
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot start app on {platform}", e);
            }
        }

        /// <summary>
        /// After each test: <para/>
        /// - Optionally call app.Repl on failure (see property "CallReplOnFailure"). <para/>
        /// - Take screenshot <para/>
        /// - Reset simulator <para/>
        /// </summary>
        public override void AfterEachTest()
        {
            CallReplOnFailure();
            base.AfterEachTest();
        }

        private void CallReplOnFailure()
        {
            if (app!= null && TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Failed)
                  && !TestEnvironment.IsTestCloud
                  && CallReplOnFailureEnabled)
            {
                app.Repl();
            }
        }
    }
}
