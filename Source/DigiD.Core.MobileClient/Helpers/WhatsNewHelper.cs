using System;
using System.IO;
using System.Reflection;
using DigiD.Common.Settings;
using DigiD.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using DigiD.ViewModels;
using DigiD.Common.Interfaces;
using System.Threading.Tasks;
using DigiD.Common.SessionModels;

namespace DigiD.Helpers
{
    public static class WhatsNewHelper
    {
        public static WhatsNewDataModel Data { get; private set; }

        public static void Init()
        {
            var assembly = typeof(WhatsNewHelper).GetTypeInfo().Assembly;

            try
            {
                using (var reader = new StreamReader(assembly.GetManifestResourceStream("DigiD.Resources.WhatsNew.json")))
                {
                    Data = JsonConvert.DeserializeObject<WhatsNewDataModel>(reader.ReadToEnd());
                }
            }
            catch
            {
                Data = new WhatsNewDataModel();
            }
        }

        internal static bool MustShow
        {
            get
            {
                if (Data == null)
                    Init();

                var oldVersion = DependencyService.Get<IGeneralPreferences>().WhatsNewVersion ?? string.Empty;
                return Data!.PageData.Count > 0 && !oldVersion.Equals(Data.Version) && !HttpSession.IsApp2AppSession && !HttpSession.IsWeb2AppSession;
            }
        }

        internal static async Task Show(Func<Task> stopAction = null, WhatsNewTourViewModel whatsNewTourViewModel = null)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                try
                {
                    var viewModel = whatsNewTourViewModel ?? new WhatsNewTourViewModel(stopAction);
                    var page = DependencyService.Get<INavigationService>().GetPage(viewModel);
                    await Application.Current.MainPage.Navigation.PushModalAsync(page, false);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Cannot display WhatsNew: {ex.Message}");
                }
            });
        }
    }
}
