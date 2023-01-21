using System;

namespace DigiD.Common.Mobile.Interfaces
{
    public interface ILocalNotifications
    {
        void Show(string title, string message, int id, DateTimeOffset notifyTime);
        void Cancel(int id);
    }
}
