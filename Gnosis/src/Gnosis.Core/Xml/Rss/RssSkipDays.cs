using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssSkipDays
        : Element, IRssSkipDays
    {
        public RssSkipDays(INode parent, IEnumerable<INode> children, IQualifiedName name, IEnumerable<IAttribute> attributes)
            : base(parent, children, name, attributes)
        {
        }

        public IEnumerable<IRssDay> Days
        {
            get { return Children.OfType<IRssDay>(); }
        }
    }
}
