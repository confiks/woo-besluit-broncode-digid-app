using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.Common.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExternalLinkLabel : Label
    {
        public static readonly BindableProperty UriProperty = BindableProperty.Create(nameof(Uri), typeof(string), typeof(ExternalLinkLabel), null, BindingMode.OneTime);
        public string Uri
        {
            get => (string)GetValue(UriProperty);
            set => SetValue(UriProperty, value);
        }

        public static readonly BindableProperty LinkTextProperty = BindableProperty.Create(nameof(LinkText), typeof(string), typeof(ExternalLinkLabel), null, BindingMode.OneTime);
        public string LinkText
        {
            get => (string)GetValue(LinkTextProperty);
            set => SetValue(LinkTextProperty, value);
        }

        public ExternalLinkLabel()
        {
            InitializeComponent();
            BindingContext = this;

            var gesture = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
                Command = new AsyncCommand(async () =>
                {
                    await Launcher.OpenAsync(Uri);
                })
            };

            this.GestureRecognizers.Add(gesture);
        }
    }
}
