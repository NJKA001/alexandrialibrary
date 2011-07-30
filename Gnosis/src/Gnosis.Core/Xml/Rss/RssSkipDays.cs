using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssSkipDays
        : XmlElement, IRssSkipDays
    {
        public RssSkipDays(IXmlNode parent, IEnumerable<IXmlNode> children, IXmlQualifiedName name, IEnumerable<IXmlAttribute> attributes)
            : base(parent, children, name, attributes)
        {
        }

        public IEnumerable<IRssDay> Days
        {
            get { return Children.OfType<IRssDay>(); }
        }
    }
}
