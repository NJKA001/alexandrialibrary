using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Atom
{
    public class AtomSummary
        : AtomTextConstruct, IAtomSummary
    {
        public AtomSummary(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
