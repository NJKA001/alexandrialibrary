using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Rss
{
    public class RssFeed
        : Element, IRssFeed
    {
        public RssFeed(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public string Version
        {
            get { return GetAttributeString("version"); }
        }

        public IRssChannel Channel
        {
            get { return Children.OfType<IRssChannel>().FirstOrDefault(); }
        }
    }
}
