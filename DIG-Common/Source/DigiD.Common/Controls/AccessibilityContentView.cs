using System.Collections;
using Xamarin.Forms;

namespace DigiD.Common.Controls
{
    public class AccessibilityContentView : ContentView
    {
        public static readonly BindableProperty ViewOrderProperty =
            BindableProperty.Create(nameof(ViewOrder), typeof(IEnumerable), typeof(AccessibilityContentView), new View[0]);

        public IEnumerable ViewOrder
        {
            get => (IEnumerable)GetValue(ViewOrderProperty);
            set => SetValue(ViewOrderProperty, value);
        }

        public AccessibilityContentView()
        {

        }
    }
}
