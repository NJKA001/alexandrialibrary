using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public class AtomAuthor
        : AtomPerson, IAtomAuthor
    {
        public AtomAuthor(Uri baseId, ILanguageTag lang, IEnumerable<IXmlExtension> extensions, IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, string name, Uri uri, string email)
            : base(baseId, lang, extensions, namespaces, primaryNamespace, name, uri, email)
        {
        }

        public override string ToString()
        {
            return ToString("author");
        }
    }
}
