using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Rss
{
    public class RssFeed
        : IRssFeed
    {
        public RssFeed(IRssChannel channel, string version, Uri baseId, ICharacterSet encoding, IEnumerable<IXmlExtension> extensions, IEnumerable<IXmlNamespace> namespaces, IEnumerable<IXmlStyleSheet> styleSheets)
        {
            if (channel == null)
                throw new ArgumentNullException("channel");

            this.channel = channel;
            this.version = version ?? "2.0";
            this.baseId = baseId;
            this.encoding = encoding ?? CharacterSet.Utf8;
            this.extensions = extensions;
            this.namespaces = namespaces;
            this.primaryNamespace = namespaces.Where(x => x != null && string.IsNullOrEmpty(x.Prefix)).FirstOrDefault();
            this.styleSheets = styleSheets;
        }

        private readonly IRssChannel channel;
        private readonly string version;
        private readonly Uri baseId;
        private readonly ICharacterSet encoding;
        private readonly IEnumerable<IXmlExtension> extensions;
        private readonly IEnumerable<IXmlNamespace> namespaces;
        private readonly IXmlNamespace primaryNamespace;
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

        public Uri BaseId
        {
            get { return baseId; }
        }

        #endregion

        #region IXmlDocument Members

        public ICharacterSet Encoding
        {
            get { return encoding; }
        }

        public IEnumerable<IXmlExtension> Extensions
        {
            get { return extensions; }
        }

        public IEnumerable<IXmlNamespace> Namespaces
        {
            get { return namespaces; }
        }

        public IXmlNamespace PrimaryNamespace
        {
            get { return namespaces.Where(x => x != null && string.IsNullOrEmpty(x.Prefix)).FirstOrDefault(); }
        }

        public IEnumerable<IXmlStyleSheet> StyleSheets
        {
            get { return styleSheets; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            xml.AppendFormat("<?xml version='1.0' encoding='{0}'?>", encoding.ToString());

            foreach (var styleSheet in styleSheets)
                xml.AppendLine(styleSheet.ToString());

            xml.AppendLine();
            xml.AppendFormat("<rss version='{0}'", version.ToXmlEscapedString());

            if (baseId != null)
                xml.AppendFormat(" xml:base='{0}'", baseId.ToXmlEscapedString());

            foreach (var ns in namespaces)
                xml.AppendFormat(" {0}", ns.ToString());

            xml.Append(">");

            xml.AppendLine(channel.ToString());

            xml.Append("</rss>");

            return xml.ToString();
        }
    }
}
