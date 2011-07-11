using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomSubtitle
        : AtomTextConstruct, IAtomSubtitle
    {
        public AtomSubtitle(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, string text, AtomTextType type)
            : base(baseId, lang, extensions, text, type)
        {
        }

        public override string ToString()
        {
            return ToString("subtitle");
        }
    }
}