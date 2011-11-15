using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public static class DataRecordExtensions
    {
        public static bool GetBoolean(this IDataRecord record, string name)
        {
            return int.Parse(record[name].ToString()) > 0;
        }

        public static byte GetByte(this IDataRecord record, string name)
        {
            return byte.Parse(record[name].ToString());
        }

        public static byte[] GetBytes(this IDataRecord record, string name)
        {
            var ordinal = record.GetOrdinal(name);
            var length = record.GetBytes(ordinal, 0, null, 0, 0);
            byte[] buffer = new byte[length];
            record.GetBytes(ordinal, 0, buffer, 0, (int)length);
            return buffer;
        }

        public static DateTime GetDateTime(this IDataRecord record, string name)
        {
            return DateTime.Parse(record[name].ToString());
        }

        public static Guid GetGuid(this IDataRecord record, string name)
        {
            return new Guid(record[name].ToString());
        }

        public static int GetInt32(this IDataRecord record, string name)
        {
            return int.Parse(record[name].ToString());
        }

        public static T GetInt32Lookup<T>(this IDataRecord record, string name, Func<int, T> lookup)
        {
            return lookup(record.GetInt32(name));
        }

        public static long GetInt64(this IDataRecord record, string name)
        {
            return long.Parse(record[name].ToString());
        }

        public static T GetInt64Lookup<T>(this IDataRecord record, string name, Func<long, T> lookup)
        {
            return lookup(record.GetInt64(name));
        }

        public static string GetString(this IDataRecord record, string name)
        {
            var value = record[name];
            return (value != null) ?
                value.ToString()
                : null;
        }

        public static T GetStringLookup<T>(this IDataRecord record, string name, Func<string, T> lookup)
        {
            return lookup(record.GetString(name));
        }

        public static TimeSpan GetTimeSpan(this IDataRecord record, string name)
        {
            return new TimeSpan(long.Parse(record[name].ToString()));
        }

        public static uint GetUInt32(this IDataRecord record, string name)
        {
            return uint.Parse(record[name].ToString());
        }

        public static Uri GetUri(this IDataRecord record, string name)
        {
            var value = record[name];
            return (value != null && value != DBNull.Value && value.ToString() != string.Empty) ?
                new Uri(value.ToString(), UriKind.RelativeOrAbsolute)
                : null;
        }

        public static T GetUriLookup<T>(this IDataRecord record, string name, Func<Uri, T> lookup)
        {
            return lookup(record.GetUri(name));
        }
    }
}
