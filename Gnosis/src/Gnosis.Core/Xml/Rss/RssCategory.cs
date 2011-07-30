using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssCategory
        : XmlElement, IRssCategory
    {
        public RssCategory(IXmlNode parent, IEnumerable<IXmlNode> children, IXmlQualifiedName name, IEnumerable<IXmlAttribute> attributes)
            : base(parent, children, name, attributes)
        {
        }

        public Uri Domain
        {
            get { return GetAttributeUri("domain"); }
        }

        public new string Name
        {
            get { return GetContentString(); }
        }
    }
}
