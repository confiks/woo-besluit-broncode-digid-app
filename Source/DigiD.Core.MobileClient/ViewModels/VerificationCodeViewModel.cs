using DigiD.Common;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.SessionModels;
using DigiD.Common.ViewModels;
using DigiD.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

#if !DEBUG
using System.Linq;
using System.Security.Cryptography;
using System;
#endif

namespace DigiD.ViewModels
{
    public class VerificationCodeViewModel : BaseViewModel
    {
        public string VerificationCode { get; set; }
        public AsyncCommand LinkCommand => new AsyncCommand(async () =>
        {
            await DependencyService.Get<INavigationService>().PushModalAsync(new MoreLoginInformationViewModel(), nav: false);
        });

#if A11YTEST
        public VerificationCodeViewModel() : this(false) { }
#endif

        public VerificationCodeViewModel()
        {
            PageId = "AP042";
            HeaderText = AppResources.KoppelcodeHeader;
            HasBackButton = true;

            ButtonCommand = new AsyncCommand(async () =>
            {
                HttpSession.VerificationCode = VerificationCode;
                await QRCodeScannerHelper.ShowScannerPage();
            });

            GenerateCode();

            void GenerateCode()
            {
#if !DEBUG
                const string chars = "BCDFGHJLMNPQRSTVWXZ";
                using (var random = new RNGCryptoServiceProvider())
                {
                    var result = new string(Enumerable.Repeat(chars, 4).Select(s => s[RandomInteger(random, s.Length-1)]).ToArray());
                    VerificationCode = result;
                }

                //http://csharphelper.com/blog/2014/08/use-a-cryptographic-random-number-generator-in-c/
                int RandomInteger(RandomNumberGenerator random, int max)
                {
                    const int min = 0;
                    var scale = uint.MaxValue;
                    while (scale == uint.MaxValue)
                    {
                        // Get four random bytes.
                        var four_bytes = new byte[4];
                        random.GetBytes(four_bytes);

                        // Convert that into an uint.
                        scale = BitConverter.ToUInt32(four_bytes, 0);
                    }

                    // Add min to the scaled difference between max and min.
                    return (int)(min + (max - min) *
                        (scale / (double)uint.MaxValue));
                }
#else

                VerificationCode = "ZZZZ";
#endif
            }
        }

        public AsyncCommand HelpCommand
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    await NavigationService.PushModalAsync(new VideoPlayerViewModel(VideoFile.Koppelcode), true, false);
                });
            }
        }
    }
}
