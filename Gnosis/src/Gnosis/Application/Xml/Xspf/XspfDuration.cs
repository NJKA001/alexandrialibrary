using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Xspf
{
    public class XspfDuration
        : Element, IXspfDuration
    {
        public XspfDuration(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public uint Content
        {
            get { return GetContentUInt32(0); }
        }
    }
}
