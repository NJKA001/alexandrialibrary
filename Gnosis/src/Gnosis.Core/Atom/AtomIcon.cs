using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomIcon
        : AtomCommon, IAtomIcon
    {
        public AtomIcon(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, Uri uri)
            : base(baseId, lang, extensions)
        {
            this.uri = uri;
        }

        private readonly Uri uri;

        #region IAtomIcon Members

        public Uri Uri
        {
            get { return uri; }
        }

        #endregion
    }
}
