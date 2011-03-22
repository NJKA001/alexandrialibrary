using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Utilities
{
    public static class ObjectExtensions
    {
        public static IArtistRepository ArtistRepository { get; set; }
        public static ICountryRepository CountryRepository { get; set; }

        public static bool IsDefined(this object value)
        {
            return (value != null && value != DBNull.Value);
        }

        public static bool AsBoolean(this object value)
        {
            if (value == null || value == DBNull.Value)
                return false;

            var boolResult = false;
            if (bool.TryParse(value.ToString(), out boolResult))
                return boolResult;

            var result = 0;
            int.TryParse(value.ToString(), out result);
            return result == 1;
        }

        public static string AsString(this object value)
        {
            return value.IsDefined() ? value.ToString() : string.Empty;
        }

        public static byte AsByte(this object value)
        {
            return value.IsDefined() ? byte.Parse(value.ToString()) : (byte)0;
        }

        public static short AsShort(this object value)
        {
            return value.IsDefined() ? short.Parse(value.ToString()) : (short) 0;
        }

        public static int AsInteger(this object value)
        {
            return value.IsDefined() ? int.Parse(value.ToString()) : 0;
        }

        public static long AsLong(this object value)
        {
            return value.IsDefined() ? long.Parse(value.ToString()) : 0L;
        }

        public static float AsFloat(this object value)
        {
            return value.IsDefined() ? float.Parse(value.ToString()) : 0f;
        }

        public static double AsDouble(this object value)
        {
            return value.IsDefined() ? double.Parse(value.ToString()) : 0d;
        }

        public static decimal AsDecimal(this object value)
        {
            return value.IsDefined() ? decimal.Parse(value.ToString()) : 0m;
        }

        public static DateTime AsDateTime(this object value)
        {
            return value.IsDefined() ? DateTime.Parse(value.ToString()) : DateTime.MinValue;
        }

        public static Uri AsUri(this object value)
        {
            return value.IsDefined() ? new Uri(value.ToString()) : new Uri("file:///");
        }

        public static IArtist AsArtist(this object value)
        {
            return value.IsDefined() ? ArtistRepository.GetOne(value) : null;
        }

        public static ICountry AsCountry(this object value)
        {
            return value.IsDefined() ? CountryRepository.GetOne(value) : null;
        }

        public static string AsAffinity(this object value)
        {
            var type = (value != null) ? value.GetType().Name : "String";

            switch (type)
            {
                case "Boolean":
                case "Byte":
                case "Int16":
                case "Int32":
                case "Int64":
                    return "INTEGER";
                case "Single":
                case "Double":
                case "Decimal":
                    return "REAL";
                default:
                    return "TEXT";
            }
        }
    }
}
