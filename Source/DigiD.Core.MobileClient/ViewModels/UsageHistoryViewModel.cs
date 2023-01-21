using System.Collections.Generic;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Models;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.ViewModels
{
    public class UsageHistoryViewModel : BaseViewModel
    {
        private int _pageIndex;
        private bool _endOfData;

        public ObservableRangeCollection<UsageHistoryModel> Items { get; set; }
        public bool IsBusy { get; set; }

        public UsageHistoryViewModel(IEnumerable<UsageHistoryModel> items)
        {
            PageId = "AP115";
            HasBackButton = true;
            Items = new ObservableRangeCollection<UsageHistoryModel>(items);

            NavCloseCommand = new AsyncCommand(async () =>
            {
                await NavigationService.PopToRoot();
            });
        }

        public AsyncCommand LoadDataCommand => new AsyncCommand(async () =>
        {
            if (IsBusy || _endOfData)
                return;

            IsBusy = true;

            _pageIndex++;

            var items = await DependencyService.Get<IUsageHistoryService>().GetUsageHistory(_pageIndex);

            Items.AddRange(items);
            _endOfData = items.Count == 0;

            IsBusy = false;
        });
    }
}
