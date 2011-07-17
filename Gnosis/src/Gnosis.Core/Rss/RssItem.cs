using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Rss
{
    public class RssItem
        : IRssItem
    {
        public RssItem(IEnumerable<IXmlExtension> extensions, IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, string title, Uri link, string description, string author, Uri comments, IRssEnclosure enclosure, IRssGuid guid, DateTime pubDate, IRssSource source, IEnumerable<IRssCategory> categories)
        {
            this.extensions = extensions;
            this.namespaces = namespaces;
            this.primaryNamespace = primaryNamespace;

            this.title = title;
            this.link = link;
            this.description = description;
            this.author = author;
            this.comments = comments;
            this.enclosure = enclosure;
            this.guid = guid;
            this.pubDate = pubDate;
            this.source = source;
            this.categories = categories;            
        }

        private readonly IEnumerable<IXmlExtension> extensions;
        private readonly IEnumerable<IXmlNamespace> namespaces;
        private readonly IXmlNamespace primaryNamespace;

        private readonly string title;
        private readonly Uri link;
        private readonly string description;
        private readonly string author;
        private readonly Uri comments;
        private readonly IRssEnclosure enclosure;
        private readonly IRssGuid guid;
        private readonly DateTime pubDate;
        private readonly IRssSource source;
        private readonly IEnumerable<IRssCategory> categories;

        #region IRssItem Members

        public string Title
        {
            get { return title; }
        }

        public Uri Link
        {
            get { return link; }
        }

        public string Description
        {
            get { return description; }
        }

        public string Author
        {
            get { return author; }
        }

        public Uri Comments
        {
            get { return comments; }
        }

        public IRssEnclosure Enclosure
        {
            get { return enclosure; }
        }

        public IRssGuid Guid
        {
            get { return guid; }
        }

        public DateTime PubDate
        {
            get { return pubDate; }
        }

        public IRssSource Source
        {
            get { return source; }
        }

        public IEnumerable<IRssCategory> Categories
        {
            get { return categories; }
        }

        #endregion

        #region IXmlElement Members

        public IEnumerable<IXmlExtension> Extensions
        {
            get { return extensions; }
        }

        public IEnumerable<IXmlNamespace> Namespaces
        {
            get { return namespaces; }
        }

        public IXmlNamespace PrimaryNamespace
        {
            get { return primaryNamespace; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            xml.AppendLine("<item>");
            
            xml.AppendEscapedTagIfNotNull("title", title);
            xml.AppendEscapedTagIfNotNull("link", link);
            xml.AppendEscapedTagIfNotNull("description", description);
            xml.AppendEscapedTagIfNotNull("author", author);
            xml.AppendEscapedTagIfNotNull("comments", comments);

            if (enclosure != null)
                xml.AppendLine(enclosure.ToString());
            if (guid != null)
                xml.AppendLine(guid.ToString());
            xml.AppendDateIfNotMinValue("pubDate", pubDate, x => x.ToRfc822String());
            if (source != null)
                xml.AppendLine(source.ToString());

            foreach (var category in categories)
                xml.AppendLine(category.ToString());

            foreach (var extension in extensions)
                xml.AppendLine(extension.ToString());

            xml.AppendLine("</item>");

            return xml.ToString();
        }
    }
}
