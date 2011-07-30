using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssHour
        : XmlElement, IRssHour
    {
        public RssHour(IXmlNode parent, IEnumerable<IXmlNode> children, IXmlQualifiedName name, IEnumerable<IXmlAttribute> attributes)
            : base(parent, children, name, attributes)
        {
        }

        public Hour Value
        {
            get
            {
                var child = Children.FirstOrDefault() as IXmlCharacterData;

                return (child != null && child.Content != null && Enum.IsDefined(typeof(Hour), child.Content)) ?
                    (Hour)Enum.Parse(typeof(Hour), child.Content)
                    : Hour.Unknown;
            }
        }
    }
}
