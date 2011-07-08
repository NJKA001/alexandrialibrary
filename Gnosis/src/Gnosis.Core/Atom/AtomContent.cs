using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public class AtomContent
        : AtomTextConstruct, IAtomContent
    {
        public AtomContent(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, string text, AtomTextType type, IMediaType mediaType, Uri src)
            : base(baseId, lang, extensions, text, type)
        {
            this.mediaType = mediaType;
            this.src = src;
        }

        private readonly IMediaType mediaType;
        private readonly Uri src;

        #region IAtomContent Members

        public IMediaType MediaType
        {
            get { return mediaType; }
        }

        public Uri Src
        {
            get { return src; }
        }

        #endregion
    }
}
