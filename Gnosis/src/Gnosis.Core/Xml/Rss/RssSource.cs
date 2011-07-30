using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssSource
        : XmlElement, IRssSource
    {
        public RssSource(IXmlNode parent, IEnumerable<IXmlNode> children, IXmlQualifiedName name, IEnumerable<IXmlAttribute> attributes)
            : base(parent, children, name, attributes)
        {
        }

        public Uri Url
        {
            get { return GetAttributeUri("url"); }
        }

        public string SourceName
        {
            get { return GetContentString(); }
        }
    }
}
