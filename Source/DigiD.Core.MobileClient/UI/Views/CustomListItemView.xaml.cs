using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomListItemView : Grid
    {
        public static readonly BindableProperty AccessibilityTextProperty = BindableProperty.Create(nameof(AccessibilityText), typeof(string), typeof(CustomListItemView), string.Empty, BindingMode.OneTime, propertyChanged:AccessibilityTextPropertyChanged);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomListItemView), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(CustomListItemView), true);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CustomListItemView));
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomListItemView));

        private static void AccessibilityTextPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            AutomationProperties.SetName(bindable, newvalue.ToString());
        }

        public string AccessibilityText
        {
            get => (string) GetValue(AccessibilityTextProperty);
            set => SetValue(AccessibilityTextProperty, value);
        }

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsSelected
        {
            get => (bool) GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public CustomListItemView()
        {
            InitializeComponent();
        }

        public new void Focus()
        {
            base.Focus();
        }
    }
}
