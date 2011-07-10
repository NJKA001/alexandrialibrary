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
        public AtomFeed(ICharacterSet encoding, IEnumerable<IXmlNamespace> namespaces, IEnumerable<IXmlStyleSheet> styleSheets, Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, IEnumerable<IAtomAuthor> authors, IAtomId id, IEnumerable<IAtomLink> links, IAtomTitle title, IAtomUpdated updated, IEnumerable<IAtomCategory> categories, IEnumerable<IAtomContributor> contributors, IEnumerable<IAtomEntry> entries, IAtomGenerator generator, IAtomIcon icon, IAtomLogo logo, IAtomRights rights, IAtomSubtitle subtitle)
            : base(baseId, lang, extensions)
        {
            this.encoding = encoding ?? CharacterSet.Utf8;
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

        private readonly IEnumerable<IAtomAuthor> authors;
        private readonly IAtomId id;
        private readonly IEnumerable<IAtomLink> links;
        private readonly IAtomTitle title;
        private readonly IAtomUpdated updated;
        private readonly IEnumerable<IAtomCategory> categories;
        private readonly IEnumerable<IAtomContributor> contributors;
        private readonly IEnumerable<IAtomEntry> entries;
        private readonly IAtomGenerator generator;
        private readonly IAtomIcon icon;
        private readonly IAtomLogo logo;
        private readonly IAtomRights rights;
        private readonly IAtomSubtitle subtitle;

        #region IAtomFeed Members

        public IEnumerable<IAtomAuthor> Authors
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

        public IEnumerable<IAtomContributor> Contributors
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

        public string ToXml()
        {
            var xml = new StringBuilder();

            xml.AppendFormat("<?xml version='1.0' encoding='{0}'?>", encoding.ToString());
            xml.AppendLine();

            foreach (var styleSheet in styleSheets)
                xml.AppendLine(styleSheet.ToString());

            xml.Append("<feed");

            foreach (var ns in namespaces)
                xml.AppendFormat(" {0}", ns.ToString());

            if (BaseId != null)
                xml.AppendFormat(" xml:base='{0}'", BaseId.ToString());

            if (Lang != null)
                xml.AppendFormat(" xml:lang='{0}'", Lang.ToString());

            xml.Append(">");
            xml.AppendLine();

            foreach (var author in authors)
                xml.AppendLine(author.ToString());

            return xml.ToString();
        }

        #endregion
    }
}
