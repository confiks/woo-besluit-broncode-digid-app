using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using DigiD.Common.Enums;
using DigiD.Common.Helpers;
using DigiD.Common.SessionModels;
using DigiD.Droid.Helpers;
using DigiD.Helpers;
using DigiD.Services;
using FFImageLoading.Forms.Platform;
using LabelHtml.Forms.Plugin.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

namespace DigiD.Droid
{
    [Activity(
        Theme = "@style/Theme.App.Starting",
        MainLauncher = true,
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenLayout | ConfigChanges.UiMode)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataScheme = "https",
#if PROD
      DataHosts = new[] { "SSSSSSSSSSSS"},
#elif PREPROD
      DataHosts = new[] { "SSSSSSSSSSSSSSSSSSSSS"},
#else
      DataHosts = new[] {
        "SSSSSSSSSSSS",
        "SSSSSSSSSSSSSSS",
        "SSSSSSSSSSSSSSS",
        "SSSSSSSSSSSSSSS",
        "SSSSSSSSSSSSSSS",
        "SSSSSSSSSSSSSSS",
        "SSSSSSSSSSSSSSS",
        "SSSSSSSSSSSSSSS",
        "SSSSSSSSSSSSSSS",
        "SSSSSSSSSSSSSSS",
        "SSSSSSSSSSSSSSSSSSSSS"},
#endif
          AutoVerify = true, DataPathPrefixes = new[] { "SSSSSSSSSS" })]
    [IntentFilter(new[] { Intent.ActionView, Intent.ActionSend }, Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable }, DataScheme = "SSSSS")]
    internal class MainActivity : FormsAppCompatActivity
    {
        internal const string TAG = "mainactivity";
        internal static bool IsAppVisible { get; private set; }
        internal static bool IsInitialized { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            AndroidX.Core.SplashScreen.SplashScreen.InstallSplashScreen(this);

            base.OnCreate(savedInstanceState);
            
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            }

#if PROD || PREPROD
            Window.AddFlags(WindowManagerFlags.Secure);
#endif

            Window.SetStatusBarColor(Resources.GetColor(Resource.Color.dark_orange, Theme));
            Window.SetNavigationBarColor(Color.Black);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            HtmlLabelRenderer.Initialize();

            Forms.Init(this, savedInstanceState);

            IsInitialized = true;

            SetThemeFromPreferences();

#if DEBUG
            var config = new FFImageLoading.Config.Configuration
            {
                VerboseLogging = false,
                VerbosePerformanceLogging = false,
                VerboseMemoryCacheLogging = false,
                VerboseLoadingCancelledLogging = false,
                Logger = new Common.CustomLogger(),
            };

            FFImageLoading.ImageService.Instance.Initialize(config);
#endif

            CachedImageRenderer.Init(true);

            LoadApplication(new App());
            Xamarin.Forms.Application.Current.RequestedThemeChanged += (sender, args) => SetThemeFromPreferences();

            HandleIntentData(Intent);
        }

        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            DeviceLockService.SetTimer();
            return base.DispatchTouchEvent(ev);
        }

        public void SetThemeFromPreferences()
        {
            SetTheme(ThemeHelper.IsInDarkMode ? Resource.Style.DigiDThemeDark : Resource.Style.DigiDThemeLight);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            HandleIntentData(intent);
        }

        protected override void OnResume()
        {
            base.OnResume();
            IsAppVisible = true;
        }

        protected override void OnPause()
        {
            base.OnPause();
            IsAppVisible = false;
        }

#pragma warning disable S3168 // "async" methods should not return "void"
        private static async void HandleIntentData(Intent intent)
#pragma warning restore S3168 // "async" methods should not return "void"
        {
            if (intent.Data != null)
            {
                HttpSession.TempSessionData = null;

                if (intent.Scheme == "https" && intent.Data.Query.StartsWith("app-app=", StringComparison.CurrentCultureIgnoreCase))
                {
                    App.ShowSplashScreen(true, false);
                    HttpSession.IsApp2AppSession = true;
                    HttpSession.SourceApplication = "ExternalApp";

                    //App is called from an other app
                    await App2AppHelper.ProcessSamlRequest(intent.Data.Query);

                    intent.SetData(null);
                    return;
                }

                HttpSession.SourceApplication = intent.GetStringExtra("com.android.browser.application_id");

                var data = intent.Scheme == "https" ? intent.Data.Query.Replace("data=", string.Empty) : intent.GetStringExtra("data");

                data = data.Replace("://", ":");

                IncomingDataHelper.HandleIncomingData(new Uri(data));

                intent.SetData(null);
            }
            else if (intent.HasExtra(AlarmReceiver.NotificationIdKey))
            {
                var id = intent.GetIntExtra(AlarmReceiver.NotificationIdKey, -1);
                LocalNotificationHelper.HandleIncomingNotification((NotificationType)id);
            }
            else if (intent.Extras != null)
            {
                var key = intent.GetStringExtra("google.message_id");
                if (!string.IsNullOrEmpty(key))
                    await RemoteNotificationHelper.ConfirmShowNotifications();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
