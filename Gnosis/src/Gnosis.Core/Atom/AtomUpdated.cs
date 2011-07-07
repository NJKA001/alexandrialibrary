using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomUpdated
        : AtomDateConstruct, IAtomUpdated
    {
        public AtomUpdated(Uri baseId, ILanguageTag lang, DateTime date)
            : base(baseId, lang, date)
        {
        }
    }
}