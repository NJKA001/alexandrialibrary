using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public static class DateTimeExtensions
    {
        public static string ToRfc3339String(this DateTime self)
        {
            return self.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fff") + "Z";
        }

        public static string ToRfc822String(this DateTime self)
        {
            return Time.Rfc822DateTime.ToString(self.ToUniversalTime());
        }

        public static TagTuple ToTagTuple(this DateTime self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            return new TagTuple(self.Year, self.Month, self.Day);
        }
    }
}
