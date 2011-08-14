using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public class AtomSource
        : AtomCommon, IAtomSource
    {
        public AtomSource(Uri baseId, ILanguageTag lang, IEnumerable<IXmlExtension> extensions, IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, IEnumerable<IAtomAuthor> authors, IAtomId id, IEnumerable<IAtomLink> links, IAtomTitle title, IAtomUpdated updated, IEnumerable<IAtomCategory> categories, IEnumerable<IAtomContributor> contributors, IAtomGenerator generator, IAtomIcon icon, IAtomLogo logo, IAtomRights rights, IAtomSubtitle subtitle)
            : base(baseId, lang, extensions, namespaces, primaryNamespace)
        {
            this.authors = authors;
            this.id = id;
            this.links = links;
            this.title = title;
            this.updated = updated;
            this.categories = categories;
            this.contributors = contributors;
            this.generator = generator;
            this.icon = icon;
            this.logo = logo;
            this.rights = rights;
            this.subtitle = subtitle;
        }

        private readonly IEnumerable<IAtomAuthor> authors;
        private readonly IAtomId id;
        private readonly IEnumerable<IAtomLink> links;
        private readonly IAtomTitle title;
        private readonly IAtomUpdated updated;
        private readonly IEnumerable<IAtomCategory> categories;
        private readonly IEnumerable<IAtomContributor> contributors;
        private readonly IAtomGenerator generator;
        private readonly IAtomIcon icon;
        private readonly IAtomLogo logo;
        private readonly IAtomRights rights;
        private readonly IAtomSubtitle subtitle;

        #region IAtomSource Members

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

        public override string ToString()
        {
            var xml = new StringBuilder();

            AppendStartTag(xml, "source");

            foreach (var author in authors)
                xml.AppendLine(author.ToString());

            xml.AppendLine(id.ToString());

            foreach (var link in links)
                xml.AppendLine(link.ToString());

            xml.AppendLine(title.ToString());
            xml.AppendLine(updated.ToString());

            foreach (var category in categories)
                xml.AppendLine(category.ToString());

            foreach (var contributor in contributors)
                xml.AppendLine(contributor.ToString());

            if (generator != null)
                xml.AppendLine(generator.ToString());

            if (icon != null)
                xml.AppendLine(icon.ToString());

            if (rights != null)
                xml.AppendLine(rights.ToString());

            if (subtitle != null)
                xml.AppendLine(subtitle.ToString());

            AppendEndTag(xml, "source");

            return xml.ToString();
        }
    }
}
