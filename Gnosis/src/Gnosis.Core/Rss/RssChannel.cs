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
            xml.AppendDateIfNotMinValue("pubDate", pubDate, x => x.ToRfc822String());
            xml.AppendDateIfNotMinValue("lastBuildDate", lastBuildDate, x => x.ToRfc822String());
            xml.AppendEscapedTagIfNotNull("generator", generator);
            xml.AppendEscapedTagIfNotNull("docs", docs);
            
            if (cloud != null)
                xml.AppendLine(cloud.ToString());
            if (ttl != TimeSpan.Zero)
                xml.AppendEscapedTagIfNotNull("ttl", ttl.TotalMinutes);
            if (image != null)
                xml.AppendLine(image.ToString());
            
            if (rating != null)
            {
                xml.AppendLine("<rating>");
                xml.AppendLine(rating.ToString().ToXmlEscapedString());
                xml.AppendLine("</rating>");
            }

            if (textInput != null)
                xml.AppendLine(textInput.ToString());
            
            if (skipHours != null && skipHours.Count() > 0)
            {
                xml.AppendLine("<skipHours>");

                foreach (var hour in skipHours)
                    xml.AppendLine("<hour>" + ((int)hour).ToString() + "</hour>");

                xml.AppendLine("</skipHours>");
            }

            if (skipDays != null && skipDays.Count() > 0)
            {
                xml.AppendLine("<skipDays>");

                foreach (var day in skipDays)
                    xml.AppendLine("<day>" + day.ToString() + "</day>");

                xml.AppendLine("</skipDays>");
            }

            foreach (var category in categories)
                xml.AppendLine(category.ToString());
            foreach (var item in items)
                xml.AppendLine(item.ToString());
            foreach (var extension in extensions)
                xml.AppendLine(extension.ToString());

            xml.AppendLine("</channel>");
            return xml.ToString();
        }
    }
}
