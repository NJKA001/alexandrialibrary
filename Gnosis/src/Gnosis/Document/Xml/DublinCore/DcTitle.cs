using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.DublinCore
{
    public class DcTitle
        : DublinCoreElement, IDcTitle
    {
        public DcTitle(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
