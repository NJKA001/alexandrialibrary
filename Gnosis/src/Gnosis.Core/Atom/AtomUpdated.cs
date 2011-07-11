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
        public AtomUpdated(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, DateTime date)
            : base(baseId, lang, extensions, date)
        {
        }

        public override string ToString()
        {
            return ToString("updated");
        }
    }
}