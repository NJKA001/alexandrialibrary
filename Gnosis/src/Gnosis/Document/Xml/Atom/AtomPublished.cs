using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Atom
{
    public class AtomPublished
        : AtomDateConstruct, IAtomPublished
    {
        public AtomPublished(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
