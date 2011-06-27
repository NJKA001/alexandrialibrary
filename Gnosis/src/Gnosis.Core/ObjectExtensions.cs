﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.Iso;
using Gnosis.Core.UN;

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

        public static byte[] ToBytes(this object self)
        {
            return (byte[])self;
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
            return DateTime.Parse(self.ToString());
        }

        public static T ToEnum<T>(this object self)
            where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new InvalidOperationException("T must be an enum type");

            return (T)Enum.Parse(typeof(T), self.ToString());
        }

        public static int ToInt32(this object self)
        {
            return int.Parse(self.ToString());
        }

        public static long ToInt64(this object self)
        {
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
            return uint.Parse(self.ToString());
        }

        public static ulong ToUInt64(this object self)
        {
            return ulong.Parse(self.ToString());
        }

        public static Uri ToUri(this object self)
        {
            return new Uri(self.ToString());
        }
    }
}