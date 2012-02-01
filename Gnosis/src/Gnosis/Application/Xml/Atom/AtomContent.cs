using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Atom
{
    public class AtomContent
        : AtomTextConstruct, IAtomContent
    {
        public AtomContent(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri Src
        {
            get { return GetAttributeUri("src"); }
        }

        public string MediaType
        {
            get { return GetAttributeString("type"); }
        }
    }
}
