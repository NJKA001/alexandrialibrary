using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Gnosis
{
#if DEBUG
    public static class DebugExtensions
    {
        public static string ToFormattedString(this byte[] self)
        {
            if (self == null)
                return string.Empty;

            var builder = new StringBuilder();
            foreach (var b in self)
                builder.AppendFormat("{0} ", b);

            return builder.ToString().Trim();
        }

        public static void WriteDebugString(this byte[] self, string prefix)
        {
            if (self != null)
                System.Diagnostics.Debug.WriteLine(prefix + self.ToFormattedString());
        }
    }
#endif
}
