using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomCategory
        : AtomCommon, IAtomCategory
    {
        public AtomCategory(Uri baseId, ILanguageTag lang, string term, Uri scheme, string label)
            : base(baseId, lang)
        {
            this.term = term;
            this.scheme = scheme;
            this.label = label;
        }

        private readonly string term;
        private readonly Uri scheme;
        private readonly string label;

        #region IAtomCategory Members

        public string Term
        {
            get { return term; }
        }

        public Uri Scheme
        {
            get { return scheme; }
        }

        public string Label
        {
            get { return label; }
        }

        #endregion
    }
}
