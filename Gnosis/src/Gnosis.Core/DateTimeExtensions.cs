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
            return self.ToString("yyyy-MM-ddTHH:mm:ss.fff") + "Z";
        }
    }
}
