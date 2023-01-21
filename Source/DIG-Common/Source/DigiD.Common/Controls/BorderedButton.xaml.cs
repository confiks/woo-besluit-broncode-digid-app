using DigiD.Common.Enums;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace DigiD.Common.Controls
{
    public partial class BorderedButton : Button
    {
        public static double? DefaultButtonHeight = 56;
        public static double? XLButtonHeight = 112;  // 2 * defaultHeight

        public static double DefaultButtonHeightValue => DefaultButtonHeight.Value;
        public static double XLButtonHeightValue => XLButtonHeight.Value;

        public static readonly BindableProperty ButtonTypeProperty = BindableProperty.Create(nameof(ButtonType), typeof(ButtonType),
            typeof(BorderedButton), ButtonType.Primary, BindingMode.TwoWay, propertyChanged: ButtonTypePropertyChanged);

        private static void ButtonTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var bb = (BorderedButton)bindable;
                var newType = (ButtonType)newValue;
                bb.ButtonType = newType;
                bb.ChangeButtonState();
            }
        }

        public ButtonType ButtonType
        {
            get => (ButtonType)GetValue(ButtonTypeProperty);
            set => SetValue(ButtonTypeProperty, value);
        }

        public bool IsPrimaryButton => ButtonType == ButtonType.Primary;

        public BorderedButton()
        {
            InitializeComponent();
            ChangeButtonState();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            ChangeButtonState();
        }

        public void ChangeButtonState()
        {
            if (IsEnabled)
                VisualStateManager.GoToState(this, IsPrimaryButton ? ButtonType.Primary.ToString() : ButtonType.Secundairy.ToString());
            else
                VisualStateManager.GoToState(this, "Disabled");
        }

    }
}
