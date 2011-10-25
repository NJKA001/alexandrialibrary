using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Atom
{
    public class AtomRights
        : AtomTextConstruct, IAtomRights
    {
        public AtomRights(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
