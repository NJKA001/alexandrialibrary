using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Atom
{
    public class AtomUpdated
        : AtomDateConstruct, IAtomUpdated
    {
        public AtomUpdated(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
