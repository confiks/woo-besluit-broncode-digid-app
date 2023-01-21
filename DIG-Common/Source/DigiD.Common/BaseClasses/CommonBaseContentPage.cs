using Xamarin.Forms;
using Xamarin.Essentials;

namespace DigiD.Common.BaseClasses
{
    public abstract class CommonBaseContentPage : ContentPage
    {
        /// <summary>
        /// Onderstaande property wordt gebruikt om voor accessibility de focus op
        /// eerste (default element) te zetten, zodat de blinde gebruiker direct bij
        /// de juiste actie is. De CustomContentPageRenderer bevat een functie, FindDefaultElement()
        /// die gebruikt onderstsaande property om de Accessibility "Cursor" goed te zetten.
        /// </summary>
        private string _defaultElementName;
        public string DefaultElementName
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultElementName))
                    _defaultElementName = "DefaultElement";
                return _defaultElementName;
            }
            set => _defaultElementName = value;
        }

        private void DisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            OrientationChanged(e.DisplayInfo.Orientation);
        }

        protected virtual void OrientationChanged(DisplayOrientation orientation) { }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DeviceDisplay.MainDisplayInfoChanged += DisplayInfoChanged;

            if (BindingContext is CommonBaseViewModel bc)
            {
                bc.OnAppearing();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DeviceDisplay.MainDisplayInfoChanged -= DisplayInfoChanged;
        }

        protected override bool OnBackButtonPressed()
        {
            if (BindingContext is CommonBaseViewModel bc)
                return bc.OnBackButtonPressed();

            return true;
        }
    }
}
