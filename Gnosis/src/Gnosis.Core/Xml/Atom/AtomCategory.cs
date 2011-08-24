using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Atom
{
    public class AtomCategory
        : AtomCommon, IAtomCategory
    {
        public AtomCategory(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        #region IAtomCategory Members

        public string Term
        {
            get { return GetAttributeString("term"); }
        }

        public Uri Scheme
        {
            get { return GetAttributeUri("scheme"); }
        }

        public string Label
        {
            get { return GetAttributeString("label"); }
        }

        #endregion
    }
}
