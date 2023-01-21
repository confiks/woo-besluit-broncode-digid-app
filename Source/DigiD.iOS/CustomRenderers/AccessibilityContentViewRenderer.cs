using DigiD.Common.Controls;
using DigiD.iOS.CustomRenderers;
using DigiD.iOS.Extensions;
using Foundation;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AccessibilityContentView), typeof(AccessibilityContentViewRenderer))]
namespace DigiD.iOS.CustomRenderers
{
    public class AccessibilityContentViewRenderer : ViewRenderer, IUIAccessibilityContainer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var result = this.GetAccessibilityElements();

                if (result?.Count > 0)
                    this.SetAccessibilityElements(NSArray.FromNSObjects(result.ToArray()));
            }
        }


        AccessibilityContentView AccessibilityContentView => (AccessibilityContentView)Element;
        #region Accessibility

        private List<NSObject> GetAccessibilityElements()
        {
            var viewOrder = AccessibilityContentView.ViewOrder;

            List<NSObject> returnValue = new List<NSObject>();
            foreach(VisualElement visualElement in viewOrder)
            {
                var nativeView = visualElement.GetViewForAccessibility();
                if (nativeView != null)
                    returnValue.Add(nativeView);
            }

            return returnValue;
        }

        #endregion
    }
}
