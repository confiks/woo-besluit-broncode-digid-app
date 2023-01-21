using System;
using CoreGraphics;
using DigiD.Common;
using DigiD.Common.Helpers;
using DigiD.Common.Mobile.Controls;
using DigiD.Common.Services;
using DigiD.iOS.Services;
using DigiD.UI;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency (typeof(Dialog))]
namespace DigiD.iOS.Services
{
	internal class Dialog : IDialog
	{
	    private UIView _loadingOverlay;
        
		public void ShowProgressDialog ()
		{
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (_loadingOverlay != null)
                    return;
                try
                {
                    var rootViewController = UIApplication.SharedApplication.Delegate.GetWindow().RootViewController;
                    var childViewController = rootViewController?.ChildViewControllers[0];

                    var bounds = childViewController?.View?.Bounds;

                    if (bounds == null)
                        return;

                    var _overlay = new LoadingOverlay();
                    AutomationProperties.SetIsInAccessibleTree(_overlay, false);

                    if (DependencyService.Get<IA11YService>().IsInVoiceOverMode() && (_overlay.FindByName("_overlayAnimation") is CustomAnimationView animationView) && !string.IsNullOrEmpty(animationView.AlternateText))
                    {
                        await DependencyService.Get<IA11YService>().Speak(animationView.AlternateText);
                    }

                    _loadingOverlay = ConvertFormsToNative(_overlay, bounds.Value);

                    childViewController.View.AccessibilityTraits = UIAccessibilityTrait.None;
                    childViewController.View.Add(_loadingOverlay);
                    await DependencyService.Get<IA11YService>().Speak(AppResources.AccessibilitySpinnerText);
                }
                catch (Exception e)
                {
                    AppCenterHelper.TrackError(e);
                }
            });
		}

        public void HideProgressDialog()
		{
            Device.BeginInvokeOnMainThread(() =>
            {
                if (_loadingOverlay == null || _loadingOverlay.Hidden) return;

                _loadingOverlay.RemoveFromSuperview();
                _loadingOverlay = null;
            });
		}

	    private static UIView ConvertFormsToNative(VisualElement view, CGRect size)
	    {
	        var renderer = Platform.CreateRenderer(view);

	        renderer.NativeView.Frame = size;

	        renderer.NativeView.AutoresizingMask = UIViewAutoresizing.All;
	        renderer.NativeView.ContentMode = UIViewContentMode.ScaleToFill;

	        renderer.Element.Layout(size.ToRectangle());

	        var nativeView = renderer.NativeView;

	        nativeView.SetNeedsLayout();

	        return nativeView;
	    }
	}
}
