using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Xspf
{
    public class XspfMeta
        : Element, IXspfMeta
    {
        public XspfMeta(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri Rel
        {
            get { return GetAttributeUri("rel"); }
        }

        public string Content
        {
            get { return GetContentString(); }
        }
    }
}
