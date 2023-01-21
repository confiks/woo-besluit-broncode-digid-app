using System;
using DigiD.Common.Helpers;
using DigiD.Common.Enums;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiD.Common.Controls
{
    public partial class AlertView : Popup
    {
        public string HeaderText { get; set; }
        public string MessageText { get; set; }
        public string AcceptText { get; set; }
        public string CancelText { get; set; }
        public bool TwoButtonsVisible { get; set; }
        public bool SingleButtonVisible => !TwoButtonsVisible;
        
        public AlertView(string title, string message, string accept, string cancel, bool invertCallToAction = false)
        {
            InitializeComponent();

            BindingContext = this;

            AcceptText = accept;
            CancelText = cancel;
            HeaderText = title;
            MessageText = message;

            if (invertCallToAction)
            {
                btnAccept.ButtonType = ButtonType.Secundairy;
                btnCancel.ButtonType = ButtonType.Primary;
            }

            TwoButtonsVisible = !string.IsNullOrEmpty(cancel);

            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
            container.SizeChanged += Layout_SizeChanged;
        }
        protected override void LightDismiss()
        {
            DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplay_MainDisplayInfoChanged;
            base.LightDismiss();
        }
        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            var isLandscape = e.DisplayInfo.Orientation == DisplayOrientation.Landscape;
            var height = Math.Min(container.Height, isLandscape ? DisplayHelper.Height - 40 : DisplayHelper.Height * .8);
            Resize(height, true);
        }

        private void ClosePopup(object result)
        {
            container.SizeChanged -= Layout_SizeChanged;
            DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplay_MainDisplayInfoChanged;
            Dismiss(result);
        }

        protected override object GetLightDismissResult()
        {
            return false;
        }

        void BorderedButton_OkClicked(object sender, EventArgs e)
        {
            ClosePopup(true);
        }

        void BorderedButton_CancelClicked(object sender, EventArgs e)
        {
            ClosePopup(false);
        }

        void Layout_SizeChanged(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var height = container.Height;
                Resize(height);
            });
        }

        private void Resize(double height, bool orientationChanged = false)
        {
            if (height == popupFrame.HeightRequest && !orientationChanged)
                return;

            var currentOrientation = DeviceDisplay.MainDisplayInfo.Orientation;
            if (currentOrientation == DisplayOrientation.Landscape)
            {
                popupFrame.WidthRequest = DisplayHelper.Width * .8;
                scrollView.WidthRequest = DisplayHelper.Width * .8 - popupFrame.Padding.HorizontalThickness;
            }
            else
            {
                popupFrame.WidthRequest = DisplayHelper.Width - 40;
                scrollView.WidthRequest = DisplayHelper.Width - 40 - popupFrame.Padding.HorizontalThickness;
            }
            popupFrame.HeightRequest = height + popupFrame.Padding.VerticalThickness;
            scrollView.HeightRequest = height;

            WidthRequest = popupFrame.WidthRequest;     // Device.RuntimePlatform == Device.Android ? popupFrame.WidthRequest : DisplayHelper.Width;
            HeightRequest = popupFrame.HeightRequest;   // Device.RuntimePlatform == Device.Android ? popupFrame.HeightRequest : DisplayHelper.Height;

            //var dbString = $"=====> AlertView.Resize(height: '{height}', orientationChanged: '{orientationChanged}') -- popupFrame.Size (w / h): '{popupFrame.Width}' / '{popupFrame.Height}' -- orientation: '{currentOrientation}' -- scrollView.WidthRequest: '{scrollView.WidthRequest}'";

            Size = new Xamarin.Forms.Size(WidthRequest, HeightRequest);
        }
    }
}
