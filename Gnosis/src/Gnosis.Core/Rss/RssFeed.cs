using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Rss
{
    public class RssFeed
        : IRssFeed
    {
        public RssFeed(IRssChannel channel, ICharacterSet encoding, string version, IEnumerable<IXmlNamespace> namespaces, IEnumerable<IXmlStyleSheet> styleSheets)
        {
            this.channel = channel;
            this.encoding = encoding;
            this.version = version;
            this.namespaces = namespaces;
            this.styleSheets = styleSheets;
        }

        private readonly IRssChannel channel;
        private readonly ICharacterSet encoding;
        private readonly string version;
        private readonly IEnumerable<IXmlNamespace> namespaces;
        private readonly IEnumerable<IXmlStyleSheet> styleSheets;

        #region IRssFeed Members

        public IRssChannel Channel
        {
            get { return channel; }
        }

        public ICharacterSet Encoding
        {
            get { return encoding; }
        }

        public string Version
        {
            get { return version; }
        }

        public IEnumerable<IXmlNamespace> Namespaces
        {
            get { return namespaces; }
        }

        public IEnumerable<IXmlStyleSheet> StyleSheets
        {
            get { return styleSheets; }
        }

        #endregion
    }
}
