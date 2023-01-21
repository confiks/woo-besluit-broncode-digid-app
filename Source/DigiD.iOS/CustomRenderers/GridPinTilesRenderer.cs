using CoreAnimation;
using DigiD.Common.Constants;
using DigiD.Common.Controls;
using DigiD.iOS.CustomRenderers;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PinViewTiles), typeof(GridPinTilesRenderer))]
namespace DigiD.iOS.CustomRenderers
{
	internal class GridPinTilesRenderer : VisualElementRenderer<ContentView>
	{
		CAKeyFrameAnimation animation;
		protected override void OnElementChanged (ElementChangedEventArgs<ContentView> e)
		{
			base.OnElementChanged (e);

		    if (e.NewElement == null)
                return;

		    MessagingCenter.Subscribe<PinViewTiles>(this, MessagingConstants.PinError, Shake);
        }

		private void Shake(PinViewTiles tiles)
		{
		    if (tiles != Element)
		        return;

            const float strength = 12.0f;

			animation = CAKeyFrameAnimation.FromKeyPath ("transform.translation.x");
			animation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);
			animation.Duration = 0.5;
			animation.Values = new NSObject[]
			{
				NSNumber.FromFloat(-strength),
				NSNumber.FromFloat(strength),
				NSNumber.FromFloat(-strength*0.66f),
				NSNumber.FromFloat(strength*0.66f),
				NSNumber.FromFloat(-strength*0.33f),
				NSNumber.FromFloat(strength*0.33f),
				NSNumber.FromFloat(0)
			};

			Layer.AddAnimation(animation, "shake");
		}

	    protected override void Dispose(bool disposing)
	    {
	        base.Dispose(disposing);

			animation?.Dispose();

			if (disposing)
	            MessagingCenter.Unsubscribe<PinViewTiles>(this, MessagingConstants.PinError);
	    }
    }
}

