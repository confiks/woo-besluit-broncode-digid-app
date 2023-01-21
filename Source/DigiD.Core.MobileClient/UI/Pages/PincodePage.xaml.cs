using System;
using DigiD.Common.Helpers;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PincodePage : BaseContentPage
    {
        public PincodePage()
        {
            DefaultElementName = "PinTiles";

            InitializeComponent();

            ChangeAppearance();

            SetGridOrientation();
        }

       protected override void OrientationChanged(DisplayOrientation orientation)
        {
            SetGridOrientation();
        }

        private void SetGridOrientation()
        {
            switch (DeviceDisplay.MainDisplayInfo.Orientation)
            {
                case DisplayOrientation.Landscape:
                    if (Device.Idiom == TargetIdiom.Phone)
                        SetLandscape();
                    else
                        SetPortrait();
                    break;
                case DisplayOrientation.Portrait:
                    SetPortrait();
                    break;
            }
        }

        private void SetPortrait()
        {
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();

            grid.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = GridLength.Star },
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto }
            };

            Grid.SetColumn(NumPad, 0);
            Grid.SetColumn(scrollView, 0);
            Grid.SetColumn(LabelPincodeForgotten, 0);
            Grid.SetRow(NumPad, 0);
            Grid.SetRow(LabelPincodeForgotten, 1);
            Grid.SetRow(NumPad, 2);
        }

        private void SetLandscape()
        {
            var rightHanded = DeviceDisplay.MainDisplayInfo.Rotation == DisplayRotation.Rotation90;

            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            grid.ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Star }
                    };
            grid.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition {Height = GridLength.Star},
                new RowDefinition {Height = GridLength.Auto}
            };

            Grid.SetColumn(NumPad, rightHanded ? 1 : 0);
            Grid.SetColumn(scrollView, rightHanded ? 0 : 1);
            Grid.SetColumn(LabelPincodeForgotten, rightHanded ? 0 : 1);
            Grid.SetRow(LabelPincodeForgotten, 1);

            Grid.SetRow(NumPad, 0);
            Grid.SetRowSpan(NumPad, 2);
        }

        private void ViewModelOnPinErrorEvent(object o, EventArgs eventArgs)
        {
            ChangeAppearance();
            PinTiles.ForceLayout();
            PinTiles.Error();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((PinCodeViewModel)BindingContext).PinErrorEvent += ViewModelOnPinErrorEvent;
            ChangeAppearance();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            NavigationPage.SetHasNavigationBar(this, !((PinCodeViewModel)BindingContext).PreventLock);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((PinCodeViewModel)BindingContext).PinErrorEvent -= ViewModelOnPinErrorEvent;
        }

        private void ChangeAppearance()
        {
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                NumPad.Padding = new Thickness(1, 1, 1, 0);
            }
            else
            {
                if (Device.RuntimePlatform == Device.iOS && Device.Idiom == TargetIdiom.Phone && DisplayHelper.Height >= 812)   // Phone X
                {
                    var margin = 20;
                    var topMargin = 35;

                    MeldingContainer.Margin = new Thickness(margin);
                    PinTiles.Margin = new Thickness(margin, topMargin, margin, margin);
                }
                else if (Device.RuntimePlatform == Device.iOS && Device.Idiom == TargetIdiom.Phone && DisplayHelper.Height < 568)
                {    //iPhone 4s
                    MeldingContainer.Margin = new Thickness(5, 5, 5, -10);
                    PinTiles.Margin = new Thickness(5);
                }
                else
                {
                    MeldingContainer.Margin = new Thickness(20, 20, 20, -10);
                    PinTiles.Margin = new Thickness(20);
                }
            }
        }
    }
}
