using System.Collections.Generic;
using System.Linq;
using Foundation;

namespace DigiD.iOS.Helpers
{
    internal static class DictionaryHelper
    {
        internal static NSDictionary ToNSDictionary (this Dictionary<string,NSObject>  dic)
        {
            return NSDictionary.FromObjectsAndKeys(dic.Values.ToArray(), dic.Keys.ToArray());
        }
    }
}
