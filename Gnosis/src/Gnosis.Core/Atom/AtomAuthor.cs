using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomAuthor
        : AtomPerson, IAtomAuthor
    {
        public AtomAuthor(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, string name, Uri uri, string email)
            : base(baseId, lang, extensions, name, uri, email)
        {
        }

        public override string ToString()
        {
            return ToString("author");
        }
    }
}
