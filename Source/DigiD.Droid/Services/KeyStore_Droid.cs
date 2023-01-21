﻿using System;
using System.Text;
using Android.Content;
using Android.Runtime;
using DigiD.Common.Models;
using DigiD.Common.Services;
using DigiD.Droid.Services;
using Java.IO;
using Javax.Crypto;
using Newtonsoft.Json;

[assembly: Xamarin.Forms.Dependency(typeof(KeyStore))]
namespace DigiD.Droid.Services
{
    internal class KeyStore : IKeyStore
    {
        private readonly Java.Security.KeyStore _keyStore;
        private readonly Java.Security.KeyStore.PasswordProtection _keyStoreProtection;
        private IntPtr _idLoadLjavaIoInputStreamArrayC;

        public static readonly object FileLock = new object();
        public const string FileName = "DigiD.KeyStore.Data";
        private static readonly char[] Password = new ObfuscatedString("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS", "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS").ToString().ToCharArray();
        
        public KeyStore()
        {
            _keyStore = Java.Security.KeyStore.GetInstance(Java.Security.KeyStore.DefaultType);
            _keyStoreProtection = new Java.Security.KeyStore.PasswordProtection(Password);

            try
            {
                lock (FileLock)
                {
                    using (var s = Xamarin.Essentials.Platform.CurrentActivity.OpenFileInput(FileName))
                    {
                        _keyStore.Load(s, Password);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                LoadEmptyKeyStore(Password);
            }
        }

        #region IKeyStore implementation

        public string FindValueForKey(string key, string defaultValue = "")
        {

            try
            {
                if (_keyStore.GetEntry(key, _keyStoreProtection) is Java.Security.KeyStore.SecretKeyEntry e)
                {
                    var bytes = e.SecretKey.GetEncoded();
                    var data = Encoding.UTF8.GetString(bytes);

                    return data;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public void Insert(string value, string key)
        {
            var secretKey = new SecretAccount<string>(value);
            var entry = new Java.Security.KeyStore.SecretKeyEntry(secretKey);
            _keyStore.SetEntry(key, entry, _keyStoreProtection);
        }

        public bool Save()
        {
            lock (FileLock)
            {
                try
                {
                    using (var s = Xamarin.Essentials.Platform.CurrentActivity.OpenFileOutput(FileName, FileCreationMode.Private | FileCreationMode.MultiProcess))
                    {
                        _keyStore.Store(s, Password);
                    }

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
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
                value = (T)Convert.ChangeType(stored.Replace("\"", string.Empty), typeof(string));
            }
            else
            {
                var deserialized = JsonConvert.DeserializeObject<T>(stored);
                try
                {
                    value = deserialized;

                }
                catch
                {
                    value = (T)Convert.ChangeType(deserialized, typeof(T));
                }
            }

            return true;
        }

        public void SetValue<T>(string key, T value)
        {
            if (typeof(T) == typeof(string))
            {
                var svalue = value as string;
                Insert(svalue, key);
            }
            else
            {
                var secretKey = new SecretAccount<T>(value);
                var entry = new Java.Security.KeyStore.SecretKeyEntry(secretKey);
                _keyStore.SetEntry(key, entry, _keyStoreProtection);
            }

            Save();
        }

        public bool Delete(string key)
        {
            try
            {
                _keyStore.DeleteEntry(key);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Work around Bug https://bugzilla.xamarin.com/show_bug.cgi?id=6766
        /// </summary>
        private void LoadEmptyKeyStore(char[] password)
        {
            if (_idLoadLjavaIoInputStreamArrayC == IntPtr.Zero)
            {
                _idLoadLjavaIoInputStreamArrayC = JNIEnv.GetMethodID(_keyStore.Class.Handle, "load", "(Ljava/io/InputStream;[C)V");
            }

            var intPtr = IntPtr.Zero;
            var intPtr2 = JNIEnv.NewArray(password);
            JNIEnv.CallVoidMethod(_keyStore.Handle, _idLoadLjavaIoInputStreamArrayC, new JValue(intPtr), new JValue(intPtr2));
            JNIEnv.DeleteLocalRef(intPtr);
            if (password != null)
            {
                JNIEnv.CopyArray(intPtr2, password);
                JNIEnv.DeleteLocalRef(intPtr2);
            }
        }

        #endregion

        private class SecretAccount<T> : Java.Lang.Object, ISecretKey
        {
            private readonly byte[] _bytes;
            public SecretAccount(T value)
            {
                if (typeof(T) == typeof(string))
                    _bytes = System.Text.Encoding.UTF8.GetBytes(value as string);
                else
                    _bytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));
            }
            public byte[] GetEncoded()
            {
                return _bytes;
            }
            public string Algorithm => "RAW";

            public string Format => "RAW";
        }
    }
}

