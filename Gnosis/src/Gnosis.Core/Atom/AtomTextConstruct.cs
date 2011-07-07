using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public abstract class AtomTextConstruct
        : AtomCommon, IAtomTextConstruct
    {
        protected AtomTextConstruct(Uri baseId, ILanguageTag lang, string content)
            : base(baseId, lang)
        {
            this.content = content;
        }

        protected AtomTextConstruct(Uri baseId, ILanguageTag lang, AtomTextType type, string content)
            : base(baseId, lang)
        {
            this.type = type;
            this.content = content;
        }

        private readonly AtomTextType type = AtomTextType.Text;
        private readonly string content;

        #region IAtomTextConstruct Members

        public AtomTextType Type
        {
            get { return type; }
        }

        public string Content
        {
            get { return content; }
        }

        #endregion
    }
}
