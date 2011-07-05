using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public class RssItem
        : IRssItem
    {
        public RssItem(string title, Uri link, string description, string author, Uri comments, IRssEnclosure enclosure, IRssGuid guid, DateTime pubDate, IRssSource source, IEnumerable<IRssCategory> categories, IEnumerable<IRssExtension> extensions)
        {
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
            this.extensions = extensions;
        }

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
        private readonly IEnumerable<IRssExtension> extensions;

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

        public IEnumerable<IRssExtension> Extensions
        {
            get { return extensions; }
        }

        #endregion
    }
}
