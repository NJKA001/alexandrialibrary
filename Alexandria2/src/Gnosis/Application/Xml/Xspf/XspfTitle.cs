using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Xspf
{
    public class XspfTitle
        : Element, IXspfTitle
    {
        public XspfTitle(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public string Content
        {
            get { return GetContentString(); }
        }
    }
}
