using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Xspf
{
    public class XspfExtension
        : Element, IXspfExtension
    {
        public XspfExtension(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri Application
        {
            get { return GetAttributeUri("application"); }
        }
    }
}
