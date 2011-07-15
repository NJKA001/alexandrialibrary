using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Rss
{
    public class RssEnclosure
        : IRssEnclosure
    {
        public RssEnclosure(Uri url, IMediaType type, int length)
        {
            if (url == null)
                throw new ArgumentNullException("url");
            if (type == null)
                throw new ArgumentNullException("type");

            this.url = url;
            this.type = type;
            this.length = length;
        }

        private readonly Uri url;
        private readonly IMediaType type;
        private readonly int length;

        #region IRssEnclosure Members

        public Uri Url
        {
            get { return url; }
        }

        public IMediaType Type
        {
            get { return type; }
        }

        public int Length
        {
            get { return length; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            xml.AppendFormat("<enclosure url='{0}' length='{1}' type='{2}' />", url.ToXmlEscapedString(), length, type.ToString());
            xml.AppendLine();

            return xml.ToString();
        }
    }
}
