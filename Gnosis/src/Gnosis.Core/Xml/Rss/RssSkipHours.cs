using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssSkipHours
        : Element, IRssSkipHours
    {
        public RssSkipHours(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public IEnumerable<IRssHour> Hours
        {
            get { return Children.OfType<IRssHour>(); }
        }
    }
}
