using System;
using System.Threading.Tasks;
using DigiD.Common.Mobile.BaseClasses;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.Common.RDA.ViewModels
{
    public class AP103ViewModel : BaseViewModel
    {
#if A11YTEST
        public AP103ViewModel() : this(async (r) => { await Task.FromResult(false); }, async () => { await Task.CompletedTask; })
        {

        }
#endif

        public AP103ViewModel(Func<bool, Task> completeAction, Func<Task> retryAction)
        {
            PageId = "AP103";
            HasBackButton = true;
            ButtonCommand = new AsyncCommand(async () =>
            {
                await NavigationService.PushAsync(new AP109ViewModel(completeAction, retryAction));
            });
        }

        public AsyncCommand BackCommand => new AsyncCommand(async () =>
        {
            await NavigationService.GoBack();
        });
    }
}
