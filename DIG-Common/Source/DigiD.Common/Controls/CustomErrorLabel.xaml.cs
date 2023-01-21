using DigiD.Common.Helpers;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.Common.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomErrorLabel : Grid
    {
        public string ErrorText {
            get => __errorLabel__.Text;
            set => __errorLabel__.Text = value;
        }

        public LineBreakMode LineBreakMode {
            get => __errorLabel__.LineBreakMode;
            set => __errorLabel__.LineBreakMode = value;
        }

        public CustomErrorLabel()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (nameof(IsVisible).Equals(propertyName))
            {
                __errorLabel__.IsVisible = IsVisible;
                if (IsVisible)
                {
                    // In het geval van static texten, deze tekst "verversen" zodat het "LiveUpdate" effect ook zijn werk kan doen.
                    __errorLabel__.Text = __errorLabel__.Text.ChangeForA11y();
                }
            }
        }
    }
}
