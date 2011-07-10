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
            var xml = new StringBuilder();

            xml.Append("<author");

            if (BaseId != null)
                xml.AppendFormat(" xml:base='{0}'", BaseId.ToString());

            if (Lang != null)
                xml.AppendFormat(" xml:lang='{0}'", Lang.ToString());

            xml.Append(">");

            xml.Append("</author>");

            return xml.ToString();
        }
    }
}
