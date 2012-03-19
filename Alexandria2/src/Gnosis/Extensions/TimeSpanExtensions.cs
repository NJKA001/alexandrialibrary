using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Tags;

namespace Gnosis
{
    public static class TimeSpanExtensions
    {
        public static string ToFormattedString(this TimeSpan self)
        {
            if (self.TotalHours >= 1)
                return string.Format("{0}:{1:00}:{2:00}", Math.Floor(self.TotalHours), Math.Floor(self.TotalMinutes % 60), Math.Floor(self.TotalSeconds % 60));
            else if (self.TotalMinutes >= 1)
                return string.Format("{0}:{1:00}", Math.Floor(self.TotalMinutes), Math.Floor(self.TotalSeconds % 60));
            else
                return string.Format("{0:00}", Math.Floor(self.TotalSeconds));
        }
    }
}
