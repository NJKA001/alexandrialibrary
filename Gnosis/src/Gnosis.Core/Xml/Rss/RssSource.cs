using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssSource
        : Element, IRssSource
    {
        public RssSource(INode parent, IEnumerable<INode> children, IQualifiedName name, IEnumerable<IAttribute> attributes)
            : base(parent, children, name, attributes)
        {
        }

        public Uri Url
        {
            get { return GetAttributeUri("url"); }
        }

        public string SourceName
        {
            get { return GetContentString(); }
        }
    }
}
