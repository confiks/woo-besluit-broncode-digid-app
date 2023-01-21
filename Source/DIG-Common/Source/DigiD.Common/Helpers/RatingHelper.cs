using System;
using System.Threading.Tasks;
using Plugin.StoreReview;
using Xamarin.Essentials;
using Device = Xamarin.Forms.Device;

namespace DigiD.Common.Helpers
{
    public static class RatingHelper
    {
        public static async Task RequestRating(bool forced = false)
        {
            var testMode = true;

#if PROD
            testMode = false;
#endif

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    if (forced)
                        CrossStoreReview.Current.OpenStoreReviewPage("SSSSSSSSSSSSSSSSSSSSSSSSSS");
                    else
                    {
                        try
                        {
                            await CrossStoreReview.Current.RequestReview(testMode);
                        }
                        catch (Exception)
                        {
                            //Do nothing if exception is raised
                        }
                    }
                    break;
                case Device.macOS:
                    await Launcher.OpenAsync(new System.Uri("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS"));
                    break;
                case Device.iOS:
                    if (forced)
                        CrossStoreReview.Current.OpenStoreReviewPage("SSSSSSSSSS");
                    else
                        await CrossStoreReview.Current.RequestReview(testMode);
                    break;
                default:
                    await CrossStoreReview.Current.RequestReview(testMode);
                    break;
            }
        }
    }
}
