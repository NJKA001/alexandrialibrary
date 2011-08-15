using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public static class TimeSpanExtensions
    {
        public static string ToFormattedString(this TimeSpan self)
        {
            if (self.Hours > 0)
                return string.Format("{0}:{1:00}:{2:00}", self.Hours, self.Minutes, self.Seconds);
            else return string.Format("{0}:{1:00}", self.Minutes, self.Seconds);
        }
    }
}
