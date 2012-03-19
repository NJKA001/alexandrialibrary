using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.DublinCore
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
