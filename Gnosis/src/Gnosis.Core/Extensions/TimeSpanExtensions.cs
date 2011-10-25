using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Tags;

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

        public static ITagTuple ToTagTuple(this TimeSpan self)
        {
            return new TagTuple(self.Days, self.Hours, self.Minutes, self.Seconds, self.Milliseconds);
        }
    }
}
