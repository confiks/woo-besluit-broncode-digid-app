using System.Reflection;
using System.Runtime.CompilerServices;
using DigiD.Common.Services;
using Xamarin.Forms;

namespace DigiD.Common.BaseClasses
{
    public class BaseAppSettings
    {
        protected IKeyStore Store { get; }

        public BaseAppSettings()
        {
            Store = DependencyService.Get<IKeyStore>();
        }

        public T GetValue<T>(T defaultValue, [CallerMemberName]string key = "")
        {
            if (Store.TryGetValue(key, out T result))
                return result;

            return defaultValue;
        }

        public void SetValue<T>(T value, [CallerMemberName]string key = "")
        {
            if (value == null)
                Store.Delete(key);
            else
                Store.SetValue(key, value);
        }

        public void Save()
        {
            Store.Save();
        }

        public virtual void Reset()
        {
            foreach (var p in GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                Store.Delete(p.Name);
            }

            Save();
        }
    }
}
