using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core
{
    public static class DateTimeExtensions
    {
        //public const string EmptyDateString = "0001-01-01T00:00:00";

        public static readonly DateTime EmptyDateTime = new DateTime(1, 1, 1, 0, 0, 0);

        public static bool IsEmpty(this DateTime self)
        {
            return self == EmptyDateTime;
        }

        public static string ToRfc822String(this DateTime self)
        {
            return Rfc822DateTime.ToString(self);
        }
    }
}
