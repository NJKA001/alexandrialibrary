using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Rss
{
    public class RssChannel
        : IRssChannel
    {
        public RssChannel(string title, Uri link, string description, ILanguageTag language, string copyright, string managingEditor, string webMaster, DateTime pubDate, DateTime lastBuildDate, string generator, Uri docs, IRssCloud cloud, TimeSpan ttl, IRssImage image, IPicsRating rating, IRssTextInput textInput, IEnumerable<RssHour> skipHours, IEnumerable<RssDay> skipDays, IEnumerable<IRssCategory> categories, IEnumerable<IRssItem> items, IEnumerable<IRssExtension> extensions)
        {
            this.title = title;
            this.link = link;
            this.description = description;
            this.language = language;
            this.copyright = copyright;
            this.managingEditor = managingEditor;
            this.webMaster = webMaster;
            this.pubDate = pubDate;
            this.lastBuildDate = lastBuildDate;
            this.generator = generator;
            this.docs = docs;
            this.cloud = cloud;
            this.ttl = ttl;
            this.image = image;
            this.rating = rating;
            this.textInput = textInput;
            this.skipHours = skipHours;
            this.skipDays = skipDays;
            this.categories = categories;
            this.items = items;
            this.extensions = extensions;
        }

        private readonly string title;
        private readonly Uri link;
        private readonly string description;
        private readonly ILanguageTag language;
        private readonly string copyright;
        private readonly string managingEditor;
        private readonly string webMaster;
        private readonly DateTime pubDate;
        private readonly DateTime lastBuildDate;
        private readonly string generator;
        private readonly Uri docs;
        private readonly IRssCloud cloud;
        private readonly TimeSpan ttl;
        private readonly IRssImage image;
        private readonly IPicsRating rating;
        private readonly IRssTextInput textInput;
        private readonly IEnumerable<RssHour> skipHours;
        private readonly IEnumerable<RssDay> skipDays;

        private readonly IEnumerable<IRssCategory> categories;
        private readonly IEnumerable<IRssItem> items;
        private readonly IEnumerable<IRssExtension> extensions;

        #region IRssChannel Members

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

        public ILanguageTag Language
        {
            get { return language; }
        }

        public string Copyright
        {
            get { return copyright; }
        }

        public string ManagingEditor
        {
            get { return managingEditor; }
        }

        public string WebMaster
        {
            get { return webMaster; }
        }

        public DateTime PubDate
        {
            get { return pubDate; }
        }

        public DateTime LastBuildDate
        {
            get { return lastBuildDate; }
        }

        public string Generator
        {
            get { return generator; }
        }

        public Uri Docs
        {
            get { return docs; }
        }

        public IRssCloud Cloud
        {
            get { return cloud; }
        }

        public TimeSpan Ttl
        {
            get { return ttl; }
        }

        public IRssImage Image
        {
            get { return image; }
        }

        public IPicsRating Rating
        {
            get { return rating; }
        }

        public IRssTextInput TextInput
        {
            get { return textInput; }
        }

        public IEnumerable<RssHour> SkipHours
        {
            get { return skipHours; }
        }

        public IEnumerable<RssDay> SkipDays
        {
            get { return skipDays; }
        }

        public IEnumerable<IRssCategory> Categories
        {
            get { return categories; }
        }

        public IEnumerable<IRssItem> Items
        {
            get { return items; }
        }

        public IEnumerable<IRssExtension> Extensions
        {
            get { return extensions; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();
            xml.AppendLine("<channel>");

            xml.AppendEscapedTagIfNotNull("title", title);
            xml.AppendEscapedTagIfNotNull("link", link);
            xml.AppendEscapedTagIfNotNull("description", description);
            xml.AppendEscapedTagIfNotNull("language", language);
            xml.AppendEscapedTagIfNotNull("copyright", copyright);
            xml.AppendEscapedTagIfNotNull("managingEditor", managingEditor);
            xml.AppendEscapedTagIfNotNull("webMaster", webMaster);

            xml.AppendLine("</channel>");
            return xml.ToString();
        }
    }
}
