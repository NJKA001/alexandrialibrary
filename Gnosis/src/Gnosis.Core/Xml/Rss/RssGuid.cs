using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssGuid
        : Element, IRssGuid
    {
        public RssGuid(INode parent, IEnumerable<INode> children, IQualifiedName name, IEnumerable<IAttribute> attributes)
            : base(parent, children, name, attributes)
        {
        }

        public string Value
        {
            get { return GetContentString(); }
        }

        public bool IsPermaLink
        {
            get { return GetAttributeBoolean("isPermaLink", true); }
        }
    }
}
