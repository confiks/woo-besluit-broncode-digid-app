using Android.Content;
using Android.Views.Animations;
using DigiD.Common.Constants;
using DigiD.Common.Controls;
using DigiD.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PinViewTiles), typeof(GridPinTilesRenderer))]
namespace DigiD.Droid.CustomRenderers
{
	internal class GridPinTilesRenderer : VisualElementRenderer<ContentView>
	{
        public GridPinTilesRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged (ElementChangedEventArgs<ContentView> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null)
				return;

            MessagingCenter.Subscribe<PinViewTiles>(this,MessagingConstants.PinError, Shake);
		}

		private void Shake(PinViewTiles tiles)
		{
		    if (tiles == Element)
		    {
		        var shake = AnimationUtils.LoadAnimation(Xamarin.Essentials.Platform.CurrentActivity, DigiD.Droid.Resource.Animation.shake);
		        StartAnimation(shake);
            }
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);

            if (disposing)
		        MessagingCenter.Unsubscribe<PinViewTiles>(this, MessagingConstants.PinError);
        }
	}
}

