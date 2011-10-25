using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Rss
{
    public class RssLink
        : Element, IRssLink
    {
        public RssLink(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri Content
        {
            get { return GetContentUri(); }
        }
    }
}
