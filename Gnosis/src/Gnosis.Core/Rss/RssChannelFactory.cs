using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

using Gnosis.Core;
using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Rss
{
    public class RssChannelFactory
        : IRssChannelFactory
    {
        #region IRssChannelFactory Members

        public IRssChannel Create(Uri location)
        {
            var contentType = location.ToContentType();
            if (contentType.Type != MediaType.RssFeed)
                throw new InvalidOperationException("The resource at this location is not a valid RSS feed");

            string title = null;
            Uri link = null;
            string description = null;
            ILanguageTag language = null;
            string copyright = null;
            string managingEditor = null;
            string webMaster = null;
            DateTime pubDate = DateTime.MinValue;
            DateTime lastBuildDate = DateTime.MinValue;
            string generator = null;
            Uri docs = null;
            IRssCloud cloud = null;
            TimeSpan ttl = TimeSpan.Zero;
            IRssImage image = null;
            IPicsRating rating = null;
            IRssTextInput textInput = null;
            IEnumerable<RssHour> skipHours = new List<RssHour>();
            IEnumerable<RssDay> skipDays = new List<RssDay>();

            IEnumerable<INamespace> namespaces = new List<INamespace>();
            IEnumerable<IRssCategory> categories = new List<IRssCategory>();
            IEnumerable<IRssItem> items = new List<IRssItem>();
            IEnumerable<IRssExtension> extensions = new List<IRssExtension>();

            var xml = location.ToXml();
            foreach (var child in xml.ChildNodes)
            {
                var node = child as XmlNode;
                if (node != null)
                    System.Diagnostics.Debug.WriteLine("node. name=" + node.Name + " type=" + node.NodeType);
            }

            return new RssChannel(title, link, description, language, copyright, managingEditor, webMaster, pubDate, lastBuildDate, generator, docs, cloud, ttl, image, rating, textInput, skipHours, skipDays, namespaces, categories, items, extensions);
        }

        #endregion
    }
}
