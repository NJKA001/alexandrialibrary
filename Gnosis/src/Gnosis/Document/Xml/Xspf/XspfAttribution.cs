using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Xspf
{
    public class XspfAttribution
        : Element, IXspfAttribution
    {
        public XspfAttribution(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public IEnumerable<IXspfAttributable> Items
        {
            get { return Children.OfType<IXspfAttributable>(); }
        }
    }
}
