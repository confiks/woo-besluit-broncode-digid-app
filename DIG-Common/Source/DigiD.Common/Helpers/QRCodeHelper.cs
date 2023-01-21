using System;
using System.Collections.Generic;
using DigiD.Common.Models;

namespace DigiD.Common.Helpers
{
    public static class QRCodeHelper
    {
        public static QRScanResult ProcessScan(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            try
            {
                var result = new QRScanResult { Identifier = uri.Scheme };  

                var values = new Dictionary<string, string>();

                foreach (var kv in uri.PathAndQuery.Split('&'))
                {
                    var indexOfSplit = kv.IndexOf('=');
                    var key = kv.Substring(0, indexOfSplit);
                    var value = kv.Substring(indexOfSplit + 1);
                    values.Add(key, value);
                }

                if (!values.ContainsKey("app_session_id") && !values.ContainsKey("vPUK"))
                    return null;

                result.Properties = values;

                return result;
            }
            catch (Exception ex)
            {
                AppCenterHelper.TrackError(ex);
                return null;
            }
        }
    }
}
