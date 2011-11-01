using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Atom
{
    public abstract class AtomPerson
        : AtomCommon, IAtomPerson
    {
        protected AtomPerson(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        #region IAtomPerson Members

        public string PersonName
        {
            get { return GetChildString("name"); }
        }

        public Uri Uri
        {
            get { return GetChildUri("uri"); }
        }

        public string Email
        {
            get { return GetChildString("email"); }
        }

        #endregion
    }
}
