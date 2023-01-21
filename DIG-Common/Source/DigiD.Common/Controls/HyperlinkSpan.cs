using System;
using DigiD.Common.Helpers;
using DigiD.Common.SessionModels;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Browser = Xamarin.Essentials.Browser;

namespace DigiD.Common.Controls
{
    public class HyperlinkSpan : Span
    {
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Source), typeof(string), typeof(HyperlinkSpan));

        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public HyperlinkSpan()
        {
            TextDecorations = TextDecorations.Underline;
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                // Launcher.OpenAsync is provided by Xamarin.Essentials.
                Command = new AsyncCommand(async  () =>
                {
                    if (AppSession.IsAppActivated && Source.IsUrlLinkToMyDigid())
                        await Browser.OpenAsync(AppSession.MyDigiDUrl, BrowserLaunchMode.External);
                    else
                        await Launcher.OpenAsync(new Uri(Source));
                }),
                NumberOfTapsRequired = 1
            });
        }
    }
}
