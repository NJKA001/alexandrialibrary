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
        public RssFeed(IRssChannel channel, string version, ICharacterSet encoding, IEnumerable<IXmlNamespace> namespaces, IEnumerable<IXmlStyleSheet> styleSheets)
        {
            this.channel = channel;
            this.version = version;
            this.encoding = encoding;
            this.namespaces = namespaces;
            this.styleSheets = styleSheets;
        }

        private readonly IRssChannel channel;
        private readonly string version;
        private readonly ICharacterSet encoding;
        private readonly IEnumerable<IXmlNamespace> namespaces;
        private readonly IEnumerable<IXmlStyleSheet> styleSheets;

        #region IRssFeed Members

        public IRssChannel Channel
        {
            get { return channel; }
        }

        public string Version
        {
            get { return version; }
        }

        #endregion

        #region IXmlDocument Members

        public ICharacterSet Encoding
        {
            get { return encoding; }
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
