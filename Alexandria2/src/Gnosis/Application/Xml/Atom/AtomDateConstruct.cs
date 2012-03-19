using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Atom
{
    public abstract class AtomDateConstruct
        : AtomCommon, IAtomDateConstruct
    {
        protected AtomDateConstruct(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public DateTime Date
        {
            get { return GetContentDateTime(DateTime.MinValue).ToUniversalTime(); }
        }
    }
}
