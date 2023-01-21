using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.Themes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseTheme : ResourceDictionary
    {
        public BaseTheme()
        {
            InitializeComponent();
        }
    }
}
