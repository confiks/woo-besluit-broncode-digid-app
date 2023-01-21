using System;
using System.Reflection;
using System.Threading.Tasks;
using DigiD.Common.BaseClasses;
using DigiD.Common.Interfaces;
using DigiD.Common.Services;
using Xamarin.Forms;

namespace DigiD.Helpers
{
    public static class NavigationHelper
    {
        public static async Task PushTargetViewModel(Type targetViewModel, object[] arguments, bool animated = true)
        {
            var instance = Activator.CreateInstance(targetViewModel,
                BindingFlags.CreateInstance |
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.OptionalParamBinding,
                null, arguments, null);

            if (Application.Current.MainPage is CustomNavigationPage)
            {
                await DependencyService.Get<INavigationService>().PushAsync((CommonBaseViewModel)instance, animated);
                await DependencyService.Get<IA11YService>().NotifyForNewPage();
            }
        }
    }
}
