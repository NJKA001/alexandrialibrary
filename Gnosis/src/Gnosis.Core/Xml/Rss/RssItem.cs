using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssItem
        : Element, IRssItem
    {
        public RssItem(INode parent, IQualifiedName name)
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

        public string Author
        {
            get { return GetChildString("author"); }
        }

        public Uri CommentsLink
        {
            get { return GetChildUri("comments"); }
        }

        public IRssEnclosure Enclosure
        {
            get { return Children.OfType<IRssEnclosure>().FirstOrDefault(); }
        }

        public IRssGuid Guid
        {
            get { return Children.OfType<IRssGuid>().FirstOrDefault(); }
        }

        public DateTime PubDate
        {
            get { return GetChildDateTime("pubDate"); }
        }

        public IRssSource Source
        {
            get { return Children.OfType<IRssSource>().FirstOrDefault(); }
        }

        public IEnumerable<IRssCategory> Categories
        {
            get { return Children.OfType<IRssCategory>(); }
        }
    }
}
