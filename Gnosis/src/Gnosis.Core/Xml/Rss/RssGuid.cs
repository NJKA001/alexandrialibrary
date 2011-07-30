using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssGuid
        : XmlElement, IRssGuid
    {
        public RssGuid(IXmlNode parent, IEnumerable<IXmlNode> children, IXmlQualifiedName name, IEnumerable<IXmlAttribute> attributes)
            : base(parent, children, name, attributes)
        {
        }

        public string Value
        {
            get { return GetContentString(); }
        }

        public bool IsPermaLink
        {
            get { return GetAttributeBoolean("isPermaLink", true); }
        }
    }
}
