using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssSkipHours
        : XmlElement, IRssSkipHours
    {
        public RssSkipHours(IXmlNode parent, IEnumerable<IXmlNode> children, IXmlQualifiedName name, IEnumerable<IXmlAttribute> attributes)
            : base(parent, children, name, attributes)
        {
        }

        public IEnumerable<IRssHour> Hours
        {
            get { return Children.OfType<IRssHour>(); }
        }
    }
}
