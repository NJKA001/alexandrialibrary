using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml.Rss
{
    public class RssEnclosure
        : Element, IRssEnclosure
    {
        public RssEnclosure(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri Url
        {
            get { return GetAttributeUri("url"); }
        }

        public IMediaType Type
        {
            get { return MediaType.Parse(GetAttributeString("type")); }
        }

        public int Length
        {
            get { return GetAttributeInt32("length"); }
        }
    }
}
