using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Atom
{
    public class AtomAuthor
        : AtomPerson, IAtomAuthor
    {
        public AtomAuthor(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
