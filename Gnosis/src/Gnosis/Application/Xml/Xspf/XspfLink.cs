using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Xspf
{
    public class XspfLink
        : Element, IXspfLink
    {
        public XspfLink(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri Rel
        {
            get { return GetAttributeUri("rel"); }
        }

        public Uri Content
        {
            get { return GetContentUri(); }
        }
    }
}
