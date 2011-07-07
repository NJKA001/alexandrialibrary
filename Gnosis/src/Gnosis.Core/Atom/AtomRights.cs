using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomRights
        : AtomTextConstruct, IAtomRights
    {
        public AtomRights(Uri baseId, ILanguageTag lang, string text, AtomTextType type)
            : base(baseId, lang, text, type)
        {
        }
    }
}
