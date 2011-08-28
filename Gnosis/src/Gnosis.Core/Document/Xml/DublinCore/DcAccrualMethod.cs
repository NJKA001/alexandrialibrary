using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.DublinCore
{
    public class DcAccrualMethod
        : DublinCoreElement, IDcAccrualMethod
    {
        public DcAccrualMethod(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
