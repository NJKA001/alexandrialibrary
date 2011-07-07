using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomGenerator
        : AtomCommon, IAtomGenerator
    {
        public AtomGenerator(Uri baseId, ILanguageTag lang, string name, Uri uri, string version)
            : base(baseId, lang)
        {
            this.name = name;
            this.uri = uri;
            this.version = version;
        }

        private readonly string name;
        private readonly Uri uri;
        private readonly string version;

        #region IAtomGenerator Members

        public string Name
        {
            get { return name; }
        }

        public Uri Uri
        {
            get { return uri; }
        }

        public string Version
        {
            get { return version; }
        }

        #endregion
    }
}
