using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Xspf
{
    public class XspfInfo
        : Element, IXspfInfo
    {
        public XspfInfo(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri Content
        {
            get { return GetContentUri(); }
        }
    }
}
