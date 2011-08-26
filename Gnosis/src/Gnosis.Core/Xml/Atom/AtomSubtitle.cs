using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Atom
{
    public class AtomSubtitle
        : AtomTextConstruct, IAtomSubtitle
    {
        public AtomSubtitle(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
