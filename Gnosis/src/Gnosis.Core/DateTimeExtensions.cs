using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public static class DateTimeExtensions
    {
        public const string EmptyDateString = "0001-01-01T00:00:00";

        public static bool IsEmpty(this DateTime self)
        {
            return self == DateTime.Parse(EmptyDateString);
        }
    }
}
