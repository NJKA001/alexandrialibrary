using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Xspf
{
    public class XspfTrackNum
        : Element, IXspfTrackNum
    {
        public XspfTrackNum(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public uint Content
        {
            get { return GetContentUInt32(0); }
        }
    }
}
