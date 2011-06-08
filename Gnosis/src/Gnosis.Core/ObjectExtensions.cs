using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        public static Guid ToGuid(this object self)
        {
            return new Guid(self.ToString());
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
