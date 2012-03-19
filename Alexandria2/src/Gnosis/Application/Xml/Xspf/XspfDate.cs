using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Xspf
{
    public class XspfDate
        : Element, IXspfDate
    {
        public XspfDate(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public DateTime Content
        {
            get { return GetContentDateTime(DateTime.MinValue); }
        }
    }
}
