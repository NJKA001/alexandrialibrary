using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Atom
{
    public class AtomId
        : AtomCommon, IAtomId
    {
        public AtomId(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri Content
        {
            get { return GetContentUri(); }
        }
    }
}
