using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuSwitchControl : Grid
    {
        public static double? MenuSwitchNormalHeight = 56;
        public static double? MenuSwitchExtraLargeHeight = 112;

        public static readonly BindableProperty CheckedProperty = BindableProperty.Create(nameof(Checked), typeof(bool), typeof(MenuSwitchControl), false, BindingMode.TwoWay, propertyChanged: CheckedPropertyChanged);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MenuSwitchControl), null, propertyChanged: TextPropertyChanged);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MenuSwitchControl));
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(MenuSwitchControl), propertyChanged:ImageSourcePropertyChanged);

        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((MenuSwitchControl) bindable).Image.Source = (ImageSource) newvalue;
        }

        private static void CheckedPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((MenuSwitchControl)bindable).ToggleSwitch.IsToggled = (bool)newvalue;
        }

        private static void TextPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((MenuSwitchControl)bindable).Label.Text = (string)newvalue;
        }

        public bool Checked
        {
            get => (bool)GetValue(CheckedProperty);
            set => SetValue(CheckedProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public ImageSource ImageSource
        {
            get => (ImageSource) GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public MenuSwitchControl()
        {
            InitializeComponent();
        }

        private bool _initialized;

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _initialized = true;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (HeightRequestProperty.PropertyName == propertyName)
            {
                SetRowHeight(HeightRequest);
            }
        }

        public void SetRowHeight(GridLength height)
        {
            if (RowDefinitions.Count > 0)
                RowDefinitions[0].Height = height;
        }

        private void ToggleSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            Checked = e.Value;

            if (!_initialized)
                return;

            if (Command != null && Command.CanExecute(e.Value))
                Command.Execute(e.Value);
        }
    }
}
