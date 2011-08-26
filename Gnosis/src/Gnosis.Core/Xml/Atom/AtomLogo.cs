using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Atom
{
    public class AtomLogo
        : AtomCommon, IAtomLogo
    {
        public AtomLogo(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
        
        public Uri Uri
        {
            get { return GetContentUri(); }
        }
    }
}
