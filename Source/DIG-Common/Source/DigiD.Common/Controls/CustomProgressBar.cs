using Xamarin.Forms;

namespace DigiD.Common.Controls
{
    public class CustomProgressBar : ProgressBar
    {
        public static readonly BindableProperty BarHeightProperty = BindableProperty.Create(nameof(BarHeight), typeof(float), typeof(CustomProgressBar), 6f, BindingMode.OneTime);

        public float BarHeight
        {
            get => (float) GetValue(BarHeightProperty);
            set => SetValue(BarHeightProperty, value);
        }
    }
}
