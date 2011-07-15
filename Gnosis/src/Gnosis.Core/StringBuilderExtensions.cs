using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public static class StringBuilderExtensions
    {
        public static void AppendEscapedTagIfNotNull(this StringBuilder self, string tag, object value)
        {
            self.AppendEscapedTagIfNotNull(tag, value, new Dictionary<string, string>());
        }

        public static void AppendDateIfNotMinValue(this StringBuilder self, string tag, DateTime date, Func<DateTime, string> toString)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            if (date == DateTime.MinValue)
                return;

            self.AppendFormat("<{0}>{1}</{0}>", tag, toString(date));
            self.AppendLine();
        }

        public static void AppendEscapedTagIfNotNull(this StringBuilder self, string tag, object value, IEnumerable<KeyValuePair<string, string>> attributes)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (tag == null)
                throw new ArgumentNullException("tag");

            if (value == null)
                return;

            self.AppendFormat("<{0}>{1}</{0}>", tag, value.ToString().ToXmlEscapedString());
            self.AppendLine();
        }
    }
}
