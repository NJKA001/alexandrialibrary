using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public static class DataRecordExtensions
    {
        public static bool GetBoolean(this IDataRecord record, string name)
        {
            return int.Parse(record[name].ToString()) > 0;
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

        public static string GetString(this IDataRecord record, string name)
        {
            return record[name].ToString();
        }

        public static uint GetUInt32(this IDataRecord record, string name)
        {
            return uint.Parse(record[name].ToString());
        }

        public static Uri GetUri(this IDataRecord record, string name)
        {
            return new Uri(record[name].ToString());
        }
    }
}
