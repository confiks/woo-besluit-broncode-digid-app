using System;
using Android.App;
using Android.Views;
using DigiD.Common.Services;
using DigiD.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Dialog = DigiD.Droid.Services.Dialog;

[assembly: Dependency(typeof(Dialog))]
namespace DigiD.Droid.Services
{
    internal class Dialog : IDialog
    {
        private readonly Android.App.Dialog _dialog;

        public Dialog()
        {
            var overlay = new LoadingOverlay();
            AutomationProperties.SetIsInAccessibleTree(overlay, false);                    

            var metrics = Xamarin.Essentials.Platform.CurrentActivity.Resources!.DisplayMetrics;
            var height = Convert.ToDouble(metrics!.HeightPixels / metrics.Density);
            var width = Convert.ToDouble(metrics.WidthPixels / metrics.Density);

            var builder = new AlertDialog.Builder(Xamarin.Essentials.Platform.CurrentActivity);
            var overlayView = ConvertFormsToNative(overlay, new Rectangle(0,0,width,height));
            builder.SetView(overlayView);
            builder.SetCancelable(false);

            _dialog = builder.Create();
            _dialog.Window.SetGravity(GravityFlags.Bottom | GravityFlags.FillHorizontal);
            _dialog.Window.SetBackgroundDrawable(new Android.Graphics.Drawables.ColorDrawable(Color.Transparent.ToAndroid()));
            _dialog.Window.DecorView.ImportantForAccessibility = ImportantForAccessibility.NoHideDescendants;
        }

        public void ShowProgressDialog()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!_dialog.IsShowing && MainActivity.IsAppVisible)
                {
                    try
                    {
                        _dialog.Show();
                    }
                    catch (Exception)
                    {
                        //Something went wrong :-)
                    }
                }
            });
        }

        public void HideProgressDialog()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (_dialog.IsShowing && MainActivity.IsAppVisible)
                {
                    try
                    {
                        _dialog.Dismiss();
                    }
                    catch (Exception)
                    {
                        //Something went wrong :-)
                    }
                }
            });
        }

        public Android.Views.View ConvertFormsToNative(Xamarin.Forms.View view, Rectangle size)
        {
            var vRenderer = Platform.CreateRendererWithContext(view, Xamarin.Essentials.Platform.CurrentActivity);
            var viewGroup = vRenderer.View;

            vRenderer.Tracker.UpdateLayout();
            var layoutParams = new ViewGroup.LayoutParams((int)size.Width, (int)size.Height);
            viewGroup.LayoutParameters = layoutParams;
            view.Layout(size);
            viewGroup.Layout(0, 0, (int)view.WidthRequest, (int)view.HeightRequest);
            return viewGroup;
        }
    }
}
