using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomPerson
        : AtomCommon, IAtomPerson
    {
        public AtomPerson(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, string name, Uri uri, string email)
            : base(baseId, lang, extensions)
        {
            this.name = name;
            this.uri = uri;
            this.email = email;
        }

        private readonly string name;
        private readonly Uri uri;
        private readonly string email;

        #region IAtomPerson Members

        public string Name
        {
            get { return name; }
        }

        public Uri Uri
        {
            get { return uri; }
        }

        public string Email
        {
            get { return email; }
        }

        #endregion
    }
}
