using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public class AtomFeed
        : AtomCommon, IAtomFeed
    {
        public AtomFeed(ICharacterSet encoding, IEnumerable<IXmlNamespace> namespaces, IEnumerable<IXmlStyleSheet> styleSheets, Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, IEnumerable<IAtomPerson> authors, IAtomId id, IEnumerable<IAtomLink> links, IAtomTitle title, IAtomUpdated updated, IEnumerable<IAtomCategory> categories, IEnumerable<IAtomPerson> contributors, IEnumerable<IAtomEntry> entries, IAtomGenerator generator, IAtomIcon icon, IAtomLogo logo, IAtomRights rights, IAtomSubtitle subtitle)
            : base(baseId, lang, extensions)
        {
            this.encoding = encoding;
            this.namespaces = namespaces;
            this.styleSheets = styleSheets;

            this.authors = authors;
            this.id = id;
            this.links = links;
            this.title = title;
            this.updated = updated;
            this.categories = categories;
            this.contributors = contributors;
            this.entries = entries;
            this.generator = generator;
            this.icon = icon;
            this.logo = logo;
            this.rights = rights;
            this.subtitle = subtitle;
        }

        private readonly ICharacterSet encoding;
        private readonly IEnumerable<IXmlNamespace> namespaces;
        private readonly IEnumerable<IXmlStyleSheet> styleSheets;

        private readonly IEnumerable<IAtomPerson> authors;
        private readonly IAtomId id;
        private readonly IEnumerable<IAtomLink> links;
        private readonly IAtomTitle title;
        private readonly IAtomUpdated updated;
        private readonly IEnumerable<IAtomCategory> categories;
        private readonly IEnumerable<IAtomPerson> contributors;
        private readonly IEnumerable<IAtomEntry> entries;
        private readonly IAtomGenerator generator;
        private readonly IAtomIcon icon;
        private readonly IAtomLogo logo;
        private readonly IAtomRights rights;
        private readonly IAtomSubtitle subtitle;

        #region IAtomFeed Members

        public IEnumerable<IAtomPerson> Authors
        {
            get { return authors; }
        }

        public IAtomId Id
        {
            get { return id; }
        }

        public IEnumerable<IAtomLink> Links
        {
            get { return links; }
        }

        public IAtomTitle Title
        {
            get { return title; }
        }

        public IAtomUpdated Updated
        {
            get { return updated; }
        }

        public IEnumerable<IAtomCategory> Categories
        {
            get { return categories; }
        }

        public IEnumerable<IAtomPerson> Contributors
        {
            get { return contributors; }
        }

        public IEnumerable<IAtomEntry> Entries
        {
            get { return entries; }
        }

        public IAtomGenerator Generator
        {
            get { return generator; }
        }

        public IAtomIcon Icon
        {
            get { return icon; }
        }

        public IAtomLogo Logo
        {
            get { return logo; }
        }

        public IAtomRights Rights
        {
            get { return rights; }
        }

        public IAtomSubtitle Subtitle
        {
            get { return subtitle; }
        }

        #endregion

        #region IXmlDocument Members

        public ICharacterSet Encoding
        {
            get { return encoding; }
        }

        public IEnumerable<IXmlNamespace> Namespaces
        {
            get { return namespaces; }
        }

        public IEnumerable<IXmlStyleSheet> StyleSheets
        {
            get { return styleSheets; }
        }

        #endregion
    }
}
