using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomLogo
        : AtomCommon, IAtomLogo
    {
        public AtomLogo(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, Uri location)
            : base(baseId, lang, extensions)
        {
            this.location = location;
        }

        private readonly Uri location;

        #region IAtomLogo Members

        public Uri Location
        {
            get { return location; }
        }

        #endregion
    }
}
