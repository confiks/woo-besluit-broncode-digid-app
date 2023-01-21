using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView : StackLayout
    {
        public event EventHandler Close;
        public MenuView()
        {
            InitializeComponent();
        }

        void Button_Clicked(object sender, System.EventArgs e)
        {
            Close?.Invoke(this, EventArgs.Empty);
        }

        private void SwipeGestureRecognizer_OnSwiped(object sender, SwipedEventArgs e)
        {
            Close?.Invoke(this, EventArgs.Empty);
        }
    }
}
