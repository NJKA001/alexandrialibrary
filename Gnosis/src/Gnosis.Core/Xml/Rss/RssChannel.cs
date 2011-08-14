using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml.Rss
{
    public class RssChannel
        : Element, IRssChannel
    {
        public RssChannel(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public string Title
        {
            get { return GetChildString("title"); }
        }

        public Uri Link
        {
            get { return GetChildUri("link"); }
        }

        public string Description
        {
            get { return GetChildString("description"); }
        }

        public ILanguageTag Language
        {
            get
            {
                var lang = GetChildString("language");
                return lang != null ?
                    LanguageTag.Parse(lang)
                    : null;
            }
        }

        public string Copyright
        {
            get { return GetChildString("copyright"); }
        }

        public string ManagingEditor
        {
            get { return GetChildString("managingEditor"); }
        }

        public string WebMaster
        {
            get { return GetChildString("webMaster"); }
        }

        public DateTime PubDate
        {
            get { return GetChildDateTime("pubDate"); }
        }

        public DateTime LastBuildDate
        {
            get { return GetChildDateTime("lastBuildDate"); }
        }

        public string Generator
        {
            get { return GetChildString("generator"); }
        }

        public Uri Docs
        {
            get { return GetChildUri("docs"); }
        }

        public IRssCloud Cloud
        {
            get { return Children.OfType<IRssCloud>().FirstOrDefault(); }
        }

        public TimeSpan Ttl
        {
            get { return new TimeSpan(0, GetChildInt32("ttl", 0), 0); }
        }

        public IRssImage Image
        {
            get { return Children.OfType<IRssImage>().FirstOrDefault(); }
        }

        public string Rating
        {
            get { return GetChildString("rating"); }
        }

        public IRssTextInput TextInput
        {
            get { return Children.OfType<IRssTextInput>().FirstOrDefault(); }
        }

        public IRssSkipHours SkipHours
        {
            get { return Children.OfType<IRssSkipHours>().FirstOrDefault(); }
        }

        public IRssSkipDays SkipDays
        {
            get { return Children.OfType<IRssSkipDays>().FirstOrDefault(); }
        }

        public IEnumerable<IRssCategory> Categories
        {
            get { return Children.OfType<IRssCategory>(); }
        }

        public IEnumerable<IRssItem> Items
        {
            get { return Children.OfType<IRssItem>(); }
        }
    }
}
