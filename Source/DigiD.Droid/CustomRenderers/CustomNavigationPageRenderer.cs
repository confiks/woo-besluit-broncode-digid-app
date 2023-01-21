using System.Collections.Generic;
using Android.Content;
using DigiD.Common;
using DigiD.Common.BaseClasses;
using DigiD.Common.Helpers;
using DigiD.Droid.CustomRenderers;
using Java.Interop;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace DigiD.Droid.CustomRenderers
{
    public class CustomNavigationPageRenderer : NavigationPageRenderer
    {

        public CustomNavigationPageRenderer(Context context) : base(context)
        {
        }
        
        public override void AddChildrenForAccessibility(IList<Android.Views.View> outChildren)
        {
            base.AddChildrenForAccessibility(outChildren);

            try
            {
                CheckAndModifyChildren(outChildren);
            }
            catch (System.Exception ex)
            {
                AppCenterHelper.TrackError(ex);
            }
        }

        private static void CheckAndModifyChildren(IEnumerable<View> outChildren)
        {
            foreach (var child in outChildren)
                CheckAndModifyView(child);
        }

        private static void CheckAndModifyView(IJavaPeerable child)
        {
            if (child is Android.Widget.ImageButton btn)
            {
                btn.Clickable = true;

                if (string.IsNullOrEmpty(btn.ContentDescription))
                    btn.ContentDescription = AppResources.AccessibilityNavigationBackButtonText;
                if (btn.ContentDescription.ToLowerInvariant().Equals("ok"))
                {
                    btn.ContentDescription = AppResources.AccessibilityAppMainMenu;
                }
                if (btn.ContentDescription.ToLowerInvariant().StartsWith("geen") ||
                    btn.ContentDescription.ToLowerInvariant().StartsWith("unlabeled"))
                {

                    btn.ImportantForAccessibility = Android.Views.ImportantForAccessibility.No;
                    btn.FocusableInTouchMode = false;
                }
            }
        }
    }
}
