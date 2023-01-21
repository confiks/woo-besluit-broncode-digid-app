using DigiD.Common.Helpers;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Mobile.Helpers;
using DigiD.Common.RDA.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.Common.RDA.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RDAPage : BaseContentPage
    {
        public bool AnimationPortraitVisible { get; private set; }
        public bool AnimationLandscapeVisible { get; private set; }
        public bool ImagePortraitVisible { get; private set; }
        public bool ImageLandscapeVisible { get; private set; }

        double AnimationWidth => Device.Idiom == TargetIdiom.Tablet ? DisplayHelper.Width - OrientationHelper.TabletPaddingWidth : DisplayHelper.Width;
        double HorizontalPadding => (Device.Idiom == TargetIdiom.Tablet && OrientationHelper.IsInLandscapeMode) ? OrientationHelper.TabletPaddingWidth + 40 : 40;

        RdaViewModel _viewModel;

        public RDAPage()
        {
            InitializeComponent();
            if (DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait || Device.Idiom == TargetIdiom.Tablet)
            {
                AnimationPortraitVisible = true;
                animationViewPortrait.WidthRequest = AnimationWidth - HorizontalPadding; //ktr Device.Idiom == TargetIdiom.Phone ? DisplayHelper.Width - 40 : DisplayHelper.Width - OrientationHelper.TabletPaddingWidth;
                animationViewPortrait.HeightRequest = animationViewPortrait.WidthRequest;
            }
            else
            {
                AnimationLandscapeVisible = Device.Idiom == TargetIdiom.Phone ? true : false;
                animationViewLandscape.WidthRequest = DisplayHelper.Width / 2 - HorizontalPadding;
                animationViewLandscape.HeightRequest = animationViewLandscape.WidthRequest;
            }
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _viewModel = BindingContext as RdaViewModel;
        }

        private void SetAnimationSize(DisplayOrientation orientation)
        {
            var width = AnimationWidth;

            var horizontalMargin = HorizontalPadding;

            if (orientation == DisplayOrientation.Landscape && Device.Idiom == TargetIdiom.Phone)
            {
                AnimationLandscapeVisible = _viewModel.AnimationVisible;
                AnimationPortraitVisible = false;
                ImagePortraitVisible = false;
                ImageLandscapeVisible = !_viewModel.AnimationVisible;

                animationViewPortrait.HeightRequest = 0;
                imageViewPortrait.HeightRequest = 0;

                if (AnimationLandscapeVisible)
                    animationViewLandscape.HeightRequest = width / 2 - horizontalMargin;
                if (ImageLandscapeVisible)
                    imageViewLandscape.HeightRequest = width / 2 - horizontalMargin;
            }
            else
            {
                AnimationPortraitVisible = _viewModel.AnimationVisible;
                AnimationLandscapeVisible = false;
                ImageLandscapeVisible = false;
                ImagePortraitVisible = !_viewModel.AnimationVisible;

                animationViewLandscape.HeightRequest = 0;
                imageViewLandscape.HeightRequest = 0;

                if (AnimationPortraitVisible)
                    animationViewPortrait.HeightRequest = width - horizontalMargin;
                if (ImagePortraitVisible)
                    imageViewPortrait.HeightRequest = width - horizontalMargin;
            }
            mainGrid.ForceLayout();
        }

        protected override void OrientationChanged(DisplayOrientation orientation)
        {
            base.OrientationChanged(orientation);
            SetAnimationSize(orientation);
        }
    }
}
