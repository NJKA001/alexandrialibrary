using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomContributor
        : AtomPerson, IAtomContributor
    {
        public AtomContributor(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, string name, Uri uri, string email)
            : base(baseId, lang, extensions, name, uri, email)
        {
        }

        public override string ToString()
        {
            var xml = new StringBuilder();

            xml.Append("<contributor");

            if (BaseId != null)
                xml.AppendFormat(" xml:base='{0}'", BaseId.ToString());

            if (Lang != null)
                xml.AppendFormat(" xml:lang='{0}'", Lang.ToString());

            xml.Append(">");

            xml.Append("</contributor>");

            return xml.ToString();
        }
    }
}
