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
        public AtomPerson(Uri baseId, ILanguageTag lang, string name, Uri url, string email)
            : base(baseId, lang)
        {
            this.name = name;
            this.url = url;
            this.email = email;
        }

        private readonly string name;
        private readonly Uri url;
        private readonly string email;

        #region IAtomPerson Members

        public string Name
        {
            get { return name; }
        }

        public Uri Url
        {
            get { return url; }
        }

        public string Email
        {
            get { return email; }
        }

        #endregion
    }
}
