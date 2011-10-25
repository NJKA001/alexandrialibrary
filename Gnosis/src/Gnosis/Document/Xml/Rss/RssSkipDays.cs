using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Rss
{
    public class RssSkipDays
        : Element, IRssSkipDays
    {
        public RssSkipDays(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public IEnumerable<IRssDay> Days
        {
            get { return Children.OfType<IRssDay>(); }
        }
    }
}
