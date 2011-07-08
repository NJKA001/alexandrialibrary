using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomId
        : AtomCommon, IAtomId
    {
        public AtomId(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, Uri value)
            : base(baseId, lang, extensions)
        {
            this.value = value;
        }

        private readonly Uri value;

        #region IAtomId Members

        public Uri Value
        {
            get { return value; }
        }

        #endregion
    }
}
