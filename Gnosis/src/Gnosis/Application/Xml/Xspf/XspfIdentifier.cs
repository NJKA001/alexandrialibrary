using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Xspf
{
    public class XspfIdentifier
        : Element, IXspfIdentifier
    {
        public XspfIdentifier(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri Content
        {
            get { return GetContentUri(); }
        }
    }
}
