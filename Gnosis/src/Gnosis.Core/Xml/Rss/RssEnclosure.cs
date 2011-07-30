using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml.Rss
{
    public class RssEnclosure
        : XmlElement, IRssEnclosure
    {
        public RssEnclosure(IXmlNode parent, IEnumerable<IXmlNode> children, IXmlQualifiedName name, IEnumerable<IXmlAttribute> attributes)
            : base(parent, children, name, attributes)
        {
        }

        public Uri Url
        {
            get { return GetAttributeUri("url"); }
        }

        public IMediaType Type
        {
            get { return MediaType.Parse(GetAttributeString("type")); }
        }

        public int Length
        {
            get { return GetAttributeInt32("length"); }
        }
    }
}
