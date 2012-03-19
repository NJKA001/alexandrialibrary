using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Xspf
{
    public class XspfCreator
        : Element, IXspfCreator
    {
        public XspfCreator(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public string Content
        {
            get { return GetContentString(); }
        }
    }
}
