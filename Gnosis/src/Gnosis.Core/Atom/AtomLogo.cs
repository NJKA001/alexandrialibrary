using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomLogo
        : AtomCommon, IAtomLogo
    {
        public AtomLogo(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, Uri uri)
            : base(baseId, lang, extensions)
        {
            this.uri = uri;
        }

        private readonly Uri uri;

        #region IAtomLogo Members

        public Uri Uri
        {
            get { return uri; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            AppendStartTag(xml, "logo");

            if (uri != null)
                xml.Append(uri.ToString());

            AppendEndTag(xml, "logo");

            return xml.ToString();
        }
    }
}
