using System;
using System.Collections.Generic;
using System.Linq;
using BerTlv;
using DigiD.Common.EID.Models;
using DigiD.Common.NFC.Helpers;

namespace DigiD.Common.EID.Helpers
{
    internal static class DataObjectHelper
    {
        internal static DataObject[] GetDataObjects(byte[] data)
        {
            var list = new List<DataObject>();

            try
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var bytesProcessed = 0;
                
                //Loop through the bytes and stop at last 2 bytes (which are the status bytes)
                while (data.Length > bytesProcessed)
                {
                    var totalBytes = data.Skip(bytesProcessed).ToArray();
                    var length = ByteHelper.GetLength(totalBytes);
                    var valueBytes = totalBytes.Take(length.Item1 + length.Item2).ToArray();

                    list.Add(GetDataObject(valueBytes));

                    bytesProcessed += valueBytes.Length;
                }

                return list.ToArray();
            }
            catch (Exception e)
            {
                Debugger.DumpInfo(e);
                throw;
            }
        }

        /// <summary>
        /// Extract Tag and Value from data
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        internal static DataObject GetDataObject(byte[] bytes)
        {
            try
            {
                var tlvs = Tlv.ParseTlv(bytes);
                var tlv = tlvs.First();

                var result = new DataObject
                {
                    Tag = new[]{ (byte)tlv.Tag },
                    Value = tlv.Value
                };

                if (result.Tag[0] == 0x30 || result.Tag[0] == 0x31 || result.Tag[0] == 0x61 || result.Tag[0] == 0x7C) // Check if tag is parenttag
                    result.Children = GetDataObjects(result.Value);

                return result;
            }
            catch (Exception e)
            {
                Debugger.DumpInfo(e);
                throw;
            }
        }
    }
}
