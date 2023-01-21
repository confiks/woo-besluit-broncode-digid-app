namespace DigiD.Common.Services
{
    public interface IKeyStore
    {
        string FindValueForKey(string key, string defaultValue = "");
        void Insert(string value, string key);
        bool Delete(string key);
        bool Save();

        bool TryGetValue<T>(string key, out T value);
        void SetValue<T>(string key, T value);
    }
}

