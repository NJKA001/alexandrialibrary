using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomPublished
        : AtomDateConstruct, IAtomPublished
    {
        public AtomPublished(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, DateTime date)
            : base(baseId, lang, extensions, date)
        {
        }
    }
}
