using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Core.Commands;
using Gnosis.Core.Iso;

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

        public static IParameter ToParameter(this object self)
        {
            var name = "@" + Guid.NewGuid().ToString().Replace("-", string.Empty);

            if (self == null)
                return new Parameter(name);

            if (self is IEntity)
                return new Parameter(name, self as IEntity);
            if (self is IValue)
                return new Parameter(name, self as IValue);
            if (self is ILanguage)
                return new Parameter(name, self as ILanguage);
            if (self is ICountry)
                return new Parameter(name, self as ICountry);
            if (self is IEnumerable<ILanguage>)
                return new Parameter(name, self as IEnumerable<ILanguage>);
            if (self is IEnumerable<ILanguage>)
                return new Parameter(name, self as IEnumerable<ICountry>);
            if (self is IEnumerable<string>)
                return new Parameter(name, self as IEnumerable<string>);
            if (self.GetType() == typeof(bool))
                return new Parameter(name, (bool)self);
            if (self.GetType() == typeof(Guid))
                return new Parameter(name, (Guid)self);
            if (self.GetType() == typeof(Uri))
                return new Parameter(name, self as Uri);
            if (self.GetType() == typeof(DateTime))
                return new Parameter(name, (DateTime)self);
            if (self.GetType() == typeof(TimeSpan))
                return new Parameter(name, (TimeSpan)self);
            if (self.GetType().IsEnum)
                return new Parameter(name, (int)self, false);

            return new Parameter(name, self, false);
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
