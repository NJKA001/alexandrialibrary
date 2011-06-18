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

        public static IIso639Language ToIso639Language(this object self)
        {
            if (self == null)
                return Iso639Language.Undetermined;

            return Iso639Language.GetLanguageByCode(self.ToString());
        }

        public static IEnumerable<IIso639Language> ToIso639Languages(this object self)
        {
            if (self == null)
                return new List<IIso639Language>();

            return self.ToNames().Select(code => Iso639Language.GetLanguageByCode(code));
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
            if (self is IIso639Language)
                return new Parameter(name, self as IIso639Language);
            if (self is IEnumerable<IIso639Language>)
                return new Parameter(name, self as IEnumerable<IIso639Language>);
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
