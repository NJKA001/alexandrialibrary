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
        public AtomIcon(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, Uri location)
            : base(baseId, lang, extensions)
        {
            this.location = location;
        }

        private readonly Uri location;

        #region IAtomIcon Members

        public Uri Location
        {
            get { return location; }
        }

        #endregion
    }
}
