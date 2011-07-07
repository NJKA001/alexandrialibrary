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
        protected AtomCommon(Uri baseId, ILanguageTag lang)
        {
            this.baseId = baseId;
            this.lang = lang;
        }

        private readonly Uri baseId;
        private readonly ILanguageTag lang;

        #region IAtomCommon Members

        public Uri  BaseId
        {
	        get { return baseId; }
        }

        public ILanguageTag  Lang
        {
	        get { return lang; }
        }

        #endregion
    }
}
