using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.DublinCore
{
    public abstract class DublinCoreElement
        : Element
    {
        protected DublinCoreElement(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public string Content
        {
            get { return GetContentString(); }
        }
    }
}
