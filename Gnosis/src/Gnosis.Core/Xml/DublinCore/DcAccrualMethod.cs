using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.DublinCore
{
    public class DcAccrualMethod
        : DublinCoreElement, IQualifiedDublinCoreElement
    {
        public DcAccrualMethod(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
