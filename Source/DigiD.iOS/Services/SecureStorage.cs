﻿using System;
using DigiD.Common.Constants;
using DigiD.Common.Services;
using DigiD.iOS.Services;
using Foundation;
using Newtonsoft.Json;
using Security;

[assembly: Xamarin.Forms.Dependency(typeof(SecureStorage))]
namespace DigiD.iOS.Services
{
    internal class SecureStorage : IKeyStore
    {
        private readonly object _lock = new object();

        private static SecRecord GetQuery(string key)
        {
            return new SecRecord(SecKind.GenericPassword)
            {
                Account = key,
                Service = StorageConstants.ServiceId,
                Label = key,
            };
        }
        public string FindValueForKey(string key, string defaultValue = "")
        {
            lock (_lock)
            {
                var result = defaultValue;

                var record = GetQuery(key);

                var data = SecKeyChain.QueryAsRecord(record, out var resultCode);

                if (resultCode == SecStatusCode.Success)
                {
                    result = NSString.FromData(data.ValueData, NSStringEncoding.UTF8);
                }

                return result;
            }
        }

        public void Insert(string value, string key)
        {
            lock (_lock)
            {
                var record = GetQuery(key);

                SecKeyChain.QueryAsRecord(record, out var resultCode);

                if (resultCode == SecStatusCode.Success && SecKeyChain.Remove(record) != SecStatusCode.Success)
                {
                    return;
                }

                SecKeyChain.Add(
                    new SecRecord(SecKind.GenericPassword)
                    {
                        Account = key,
                        Service = StorageConstants.ServiceId,
                        Label = key,
                        Accessible = SecAccessible.WhenUnlockedThisDeviceOnly,
                        Synchronizable = false,
                        ValueData = NSData.FromString(value, NSStringEncoding.UTF8)
                    });
            }
        }

        public bool Delete(string key)
        {
            var record = GetQuery(key);
            return SecKeyChain.Remove(record) == SecStatusCode.Success;
        }

        public bool Save()
        {
            return true;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            var stored = FindValueForKey(key, string.Empty);
            if (string.IsNullOrEmpty(stored))
            {
                value = default(T);
                return false;
            }

            // strings doen we gewoon lekker snel zonder JsonConvert
            if (typeof(T) == typeof(string))
            {
                value = (T)Convert.ChangeType(stored, typeof(string));
            }
            else
            {
                //fix for 1.1.1 to 1.3.0
                if (typeof(T) == typeof(bool))
                {
                    stored = stored.ToLowerInvariant();
                }

                try
                {
                    var deserialized = JsonConvert.DeserializeObject<T>(stored);
                    value = deserialized;
                }
                catch (Exception)
                {
                    value = (T)Convert.ChangeType(stored, typeof(T));
                }
            }

            return true;
        }

        public void SetValue<T>(string key, T value)
        {
            if (typeof(T) == typeof(string))
            {
                Insert(value as string, key);
            }
            else
            {
                var serializedValue = JsonConvert.SerializeObject(value);
                Insert(serializedValue, key);
            }
        }
    }
}

