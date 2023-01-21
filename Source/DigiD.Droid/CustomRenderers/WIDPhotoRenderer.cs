using System;
using System.IO;
using Android.Graphics;
using Android.Widget;
using CSJ2K;
using CSJ2K.j2k.util;
using DigiD.Controls;
using DigiD.Droid.CustomRenderers;
using FFImageLoading;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WidPhoto), typeof(WidPhotoRenderer))]
namespace DigiD.Droid.CustomRenderers
{
    public class WidPhotoRenderer : ImageRenderer
    {
        public WidPhotoRenderer(Android.Content.Context context) : base(context)
        {
        }

        protected override async void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null && e.NewElement is WidPhoto widPhoto)
            {
                var androidImage = new ImageView(Context);
                try
                {
                    var source = (StreamImageSource)widPhoto.Source;
                    var cancellationToken = System.Threading.CancellationToken.None;
                    var stream = await source.Stream(cancellationToken);
                    stream.Seek(0, SeekOrigin.Begin);
                    var bytes = stream.ToByteArray();

                    CSJ2K.Util.AndroidBitmapImageCreator.Register();
                    var paramList = new ParameterList(J2kImage.GetDefaultDecoderParameterList(null));
                    paramList["rate"] = "1"; // zolang de rate maar niet -1 (default) of 0 is.
                    var bmp = J2kImage.FromBytes(bytes, paramList).As<Bitmap>();

                    if (bmp != null)
                    {
                        androidImage.SetImageBitmap(bmp);
                        SetNativeControl(androidImage);
                    }
                }
                catch (InvalidOperationException ioe)
                {
                    System.Diagnostics.Debug.WriteLine($"Fout bij lezen jp2 data: [{ioe.Message}]");
                }
                catch (Exception exc)
                {
                    System.Diagnostics.Debug.WriteLine($"Fout bij lezen jp2 data: [{exc.Message}]");
                }
            }
        }
    }
}
