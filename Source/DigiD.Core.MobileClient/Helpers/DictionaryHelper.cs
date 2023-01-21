using System.Collections.Generic;

namespace DigiD.Helpers
{
    public static class DictionaryHelper
    {
        public static string GetValue(this Dictionary<string, string> dictionary, string key)
        {
            return dictionary.ContainsKey(key) ? dictionary[key] : null;
        }
    }
}
