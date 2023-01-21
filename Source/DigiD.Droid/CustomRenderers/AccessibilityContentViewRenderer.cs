
using Android.Content;
using DigiD.Common.Controls;
using DigiD.Droid.CustomRenderers;
using DigiD.Droid.Extensions;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AccessibilityContentView), typeof(AccessibilityContentViewRenderer))]
namespace DigiD.Droid.CustomRenderers
{
    public class AccessibilityContentViewRenderer : ViewRenderer
    {
        AccessibilityContentView AccessibilityContentView => (AccessibilityContentView)Element;

        public AccessibilityContentViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                this.SetAccessibilityElements();
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            SetAccessibilityElements();
            base.OnLayout(changed, l, t, r, b);
        }

        void SetAccessibilityElements()
        {
            var viewOrder = AccessibilityContentView.ViewOrder.OfType<BorderedButton>().ToList();

            if (viewOrder.Count != 2)
                return;

            var view1 = viewOrder[0].GetViewForAccessibility();
            var view2 = viewOrder[1].GetViewForAccessibility();

            if (view1 == null || view2 == null)
                return;

            view2.AccessibilityTraversalAfter = view1.Id;
            view1.AccessibilityTraversalBefore = view2.Id;

        }
    }
}
