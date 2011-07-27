using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssLink
        : XmlElement
    {
        public RssLink(IXmlNode parent, IEnumerable<IXmlNode> children, IXmlQualifiedName name, IEnumerable<IXmlAttribute> attributes)
            : base(parent, children, name, attributes)
        {
        }

        public Uri Content
        {
            get
            {
                var child = Children.FirstOrDefault() as IXmlEscapedSection;

                return (child != null && child.Content != null) ? 
                    child.Content.ToUri() 
                    : null;
            }
        }
    }
}
