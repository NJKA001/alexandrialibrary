using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public class RssGuid
        : IRssGuid
    {
        public RssGuid(string value)
            : this(value, true)
        {
        }

        public RssGuid(string value, bool isPermaLink)
        {
            this.value = value;
            this.isPermaLink = isPermaLink;
        }

        private readonly string value;
        private readonly bool isPermaLink;

        #region IRssGuid Members

        public string Value
        {
            get { return value; }
        }

        public bool IsPermaLink
        {
            get { return isPermaLink; }
        }

        #endregion
    }
}
