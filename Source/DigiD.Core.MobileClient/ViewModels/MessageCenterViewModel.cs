using System.Collections.Generic;
using System.Linq;
using DigiD.Common;
using DigiD.Common.Models.ResponseModels;
using DigiD.Common.SessionModels;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DigiD.ViewModels
{
    public class MessageCenterViewModel : BaseNotificationsSettingsViewModel
    {
        public List<Message> Messages { get; set; }

        public bool HasMessages => Messages?.Count > 0;

#if A11YTEST
        public MessageCenterViewModel() : this ("a11ySessionId") { }
#endif

        public MessageCenterViewModel(MessageCenterResponse messages, bool canGoBack) : base(true)
        {
            PageId = "AP022";
            HeaderText = AppResources.AP022_Header;
            HasBackButton = canGoBack;

            if (!canGoBack)
            {
                NavCloseCommand = new AsyncCommand(async () =>
                {
                    await NavigationService.PopToRoot();
                });
            }

            LoadMessages(messages);
        }

        private void LoadMessages(MessageCenterResponse messages)
        {
            Messages = messages.Messages.OrderByDescending(x => x.DateTime).ToList();
            AppSession.AccountStatus.UnreadMessagesCount = 0;
        }
    }
}
