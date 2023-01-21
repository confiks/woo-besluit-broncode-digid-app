using System.Drawing;
using Foundation;
using UIKit;

namespace DigiD.iOS.Views
{
    [Register("LoadingOverlay")]
    internal sealed class LoadingOverlay : UIView
    {
        UIActivityIndicatorView activitySpinner;

        public LoadingOverlay(RectangleF frame, string message, string imageSource, string cancelText) : base(frame)
        {
            BackgroundColor = UIColor.Black;
            Alpha = 0.60f;
            AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

            var labelHeight = 22;
            var labelWidth = (float)Frame.Width - 20;

            float centerX = (float)Frame.Width / 2;
            float centerY = (float)Frame.Height / 2;

            if (!string.IsNullOrEmpty(message))
            {
                activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);

                activitySpinner.Frame = new RectangleF(
                    centerX - ((float)activitySpinner.Frame.Width / 2),
                    centerY - (float)activitySpinner.Frame.Height - 20,
                    (float)activitySpinner.Frame.Width,
                    (float)activitySpinner.Frame.Height);

                activitySpinner.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;

                AddSubview(activitySpinner);

                activitySpinner.StartAnimating();

                var loadingLabel = new UILabel(new RectangleF(
                    centerX - (labelWidth / 2),
                    centerY + 20,
                    labelWidth,
                    labelHeight
                    ));

                loadingLabel.BackgroundColor = UIColor.Clear;
                loadingLabel.TextColor = UIColor.White;
                loadingLabel.Text = message;
                loadingLabel.TextAlignment = UITextAlignment.Center;
                loadingLabel.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
                loadingLabel.Font = UIFont.FromName("RO-Regular", loadingLabel.Font.PointSize);
                AddSubview(loadingLabel);
            }
            else
            {
                var imageView = new UIImageView(UIImage.FromBundle(imageSource));

                imageView.Frame = new RectangleF(
                    centerX - ((float)imageView.Frame.Width / 2),
                    centerY - (float)imageView.Frame.Height / 2,
                    (float)imageView.Frame.Width,
                    (float)imageView.Frame.Height);

                imageView.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;

                AddSubview(imageView);
            }
        }

        public void Hide(bool delay)
        {
            Animate(
                delay ? 0.5 : 0, // duration
                () => { Alpha = 0; },
                RemoveFromSuperview
            );
        }
    }
}
