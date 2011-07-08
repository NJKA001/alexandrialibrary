using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public abstract class AtomCommon
        : IAtomCommon
    {
        protected AtomCommon(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions)
        {
            this.baseId = baseId;
            this.lang = lang;
            this.extensions = extensions;
        }

        private readonly Uri baseId;
        private readonly ILanguageTag lang;
        private readonly IEnumerable<IAtomExtension> extensions;

        #region IAtomCommon Members

        public Uri  BaseId
        {
	        get { return baseId; }
        }

        public ILanguageTag  Lang
        {
	        get { return lang; }
        }

        public IEnumerable<IAtomExtension> Extensions
        {
            get { return extensions; }
        }

        #endregion
    }
}
