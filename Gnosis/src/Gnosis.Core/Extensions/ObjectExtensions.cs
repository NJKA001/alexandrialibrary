using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Core.Culture;
using Gnosis.Core.Document;
using Gnosis.Core.Geography;

namespace Gnosis.Core
{
    public static class ObjectExtensions
    {
        public static bool ToBoolean(this object self)
        {
            return int.Parse(self.ToString()) > 0;
        }

        public static byte ToByte(this object self)
        {
            return byte.Parse(self.ToString());
        }

        public static byte[] ToByteArray(this object self)
        {
            return (byte[])self;
        }

        public static ICharacterSet ToCharacterSet(this object self)
        {
            if (self == null)
                return CharacterSet.Unknown;

            return CharacterSet.Parse(self.ToString());
        }

        public static ICountry ToCountry(this object self)
        {
            if (self == null)
                return Country.Unknown;

            return Country.GetCountryByCode(self.ToString());
        }

        public static IEnumerable<ICountry> ToCountries(this object self)
        {
            if (self == null)
                return new List<ICountry>();

            return self.ToNames().Select(code => Country.GetCountryByCode(code));
        }

        public static DateTime ToDateTime(this object self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            if (self is DateTime)
                return (DateTime)self;

            var date = DateTime.MinValue;
            var s = self.ToString();
            if (Time.Rfc822DateTime.TryParse(s, out date))
                return date;

            return DateTime.Parse(s);
        }

        public static T ToEnum<T>(this object self)
            where T : struct
        {
            return self.ToEnum<T>(default(T));
        }

        public static T ToEnum<T>(this object self, T defaultValue)
            where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Generic type must be a valid enum type");

            var result = defaultValue;
            Enum.TryParse<T>(self.ToString(), out result);
            return result;
        }

        public static int ToInt32(this object self)
        {
            if (self is int)
                return (int)self;

            return int.Parse(self.ToString());
        }

        public static long ToInt64(this object self)
        {
            if (self is long)
                return (long)self;

            return long.Parse(self.ToString());
        }

        public static Guid ToGuid(this object self)
        {
            return new Guid(self.ToString());
        }

        public static ILanguage ToLanguage(this object self)
        {
            if (self == null)
                return Language.Undetermined;

            return Language.GetLanguageByCode(self.ToString());
        }

        public static ILanguageTag ToLanguageTag(this object self)
        {
            if (self == null)
                return LanguageTag.Empty;

            return LanguageTag.Parse(self.ToString());
        }

        public static IEnumerable<ILanguage> ToLanguages(this object self)
        {
            if (self == null)
                return new List<ILanguage>();

            return self.ToNames().Select(code => Language.GetLanguageByCode(code));
        }

        public static IEnumerable<string> ToNames(this object self)
        {
            if (self == null)
                return new List<string>();

            return ((string)self.ToString()).ToNames();
        }

        public static IRegion ToRegion(this object self)
        {
            if (self == null)
                return Region.Unknown;

            var code = -1;
            int.TryParse(self.ToString(), out code);
            
            return Region.GetRegionByCode(code);
        }

        public static TimeSpan ToTimeSpan(this object self)
        {
            return new TimeSpan((long)self);
        }

        public static uint ToUInt32(this object self)
        {
            if (self is uint)
                return (uint)self;

            return uint.Parse(self.ToString());
        }

        public static ulong ToUInt64(this object self)
        {
            if (self is ulong)
                return (ulong)self;

            return ulong.Parse(self.ToString());
        }

        public static Uri ToUri(this object self)
        {
            if (self is Uri)
                return (Uri)self;

            try
            {
                return new Uri(self.ToString(), UriKind.RelativeOrAbsolute);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string[] ToStringArray(this object self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            if (self is string[])
                return (string[])self;
            else if (self is string)
                return new string[] { (string)self };

            return new string[0];
        }

        public static bool IsByteArray(this object self)
        {
            return (self != null && self is byte[]);
        }

        public static bool IsDateTime(this object self)
        {
            return (self != null && self is DateTime);
        }

        public static bool IsTimeSpan(this object self)
        {
            return (self != null && self is TimeSpan);
        }

        public static bool IsString(this object self)
        {
            return (self != null && self is string);
        }

        public static bool IsStringArray(this object self)
        {
            return (self != null && self is string[]);
        }

        public static bool IsUInt32(this object self)
        {
            return (self != null && self is uint);
        }

        public static bool IsEnum<T>(this object self)
            where T : struct
        {
            if (self == null || !typeof(T).IsEnum)
                return false;

            if (self is T)
                return true;

            var result = default(T);
            return Enum.TryParse(self.ToString(), out result);
        }
    }
}
