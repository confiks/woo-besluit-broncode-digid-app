using System;
using System.Windows.Input;
using DigiD.Common.Helpers;
using DigiD.Common.Mobile.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BlurredOverlay : Grid
	{
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(BlurredOverlay));

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            
            if (propertyName == nameof(IsVisible) && IsVisible)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    // Spinner moet vierkant zijn en de kleinste waarde gebruiken
                    spinnerView.WidthRequest = OrientationHelper.IsInLandscapeMode  
                        ? Math.Min(DisplayHelper.Height, 500) 
                        : Math.Min(DisplayHelper.Width, 500);
                    spinnerView.HeightRequest = spinnerView.WidthRequest;
                    spinnerView.Play();
                });
            }
        }
        
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        public BlurredOverlay ()
		{
			InitializeComponent ();
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (Command?.CanExecute(null) == true)
                Command.Execute(null);
        }

        private void SpinnerView_OnOnFinish(object sender, EventArgs e)
        {
            if (Command?.CanExecute(null) == true)
                Command.Execute(null);
        }
    }
}
