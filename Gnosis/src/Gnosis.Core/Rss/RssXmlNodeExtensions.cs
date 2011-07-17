using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Gnosis.Core;
using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Rss
{
    public static class RssXmlNodeExtensions
    {
        #region Private Extension Methods

        private static IRssCategory ToRssCategory(this XmlNode node)
        {
            Uri domain = null;
            string name = null;

            if (node.Attributes != null)
            {
                foreach (var attrib in node.Attributes.Cast<XmlAttribute>())
                {
                    if (attrib != null && attrib.Name == "domain")
                        domain = new Uri(attrib.Value, UriKind.RelativeOrAbsolute);
                }
            }

            name = node.InnerText;

            return (name != null) ?
                new RssCategory(domain, name)
                : null;
        }

        private static IRssChannel ToRssChannel(this XmlNode node, IEnumerable<IXmlNamespace> namespaces)
        {
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

            var categories = new List<IRssCategory>();
            var items = new List<IRssItem>();
            var extensions = new List<IXmlExtension>();

            foreach (var child in node.ChildNodes.Cast<XmlNode>())
            {
                if (child == null)
                    continue;

                if (child.Name.Contains(':'))
                {
                    var extension = child.ToXmlExtension(namespaces);
                    if (extension != null)
                        extensions.Add(extension);
                }
                else
                {
                    switch (child.Name)
                    {
                        case "title":
                            title = child.InnerText;
                            break;
                        case "link":
                            link = child.InnerText.ToUri();
                            break;
                        case "description":
                            description = child.InnerText;
                            break;
                        case "language":
                            language = child.InnerText.ToLanguageTag();
                            break;
                        case "copyright":
                            copyright = child.InnerText;
                            break;
                        case "managingEditor":
                            managingEditor = child.InnerText;
                            break;
                        case "webMaster":
                            webMaster = child.InnerText;
                            break;
                        case "pubDate":
                            pubDate = child.InnerText.ToDateTime();
                            break;
                        case "lastBuildDate":
                            lastBuildDate = child.InnerText.ToDateTime();
                            break;
                        case "generator":
                            generator = child.InnerText;
                            break;
                        case "docs":
                            docs = child.InnerText.ToUri();
                            break;
                        case "cloud":
                            cloud = child.ToRssCloud();
                            break;
                        case "ttl":
                            ttl = child.ToRssTtl();
                            break;
                        case "image":
                            image = child.ToRssImage();
                            break;
                        case "rating":
                            rating = child.InnerText.ToPicsRating();
                            break;
                        case "textInput":
                            textInput = child.ToRssTextInput();
                            break;
                        case "skipHours":
                            skipHours = child.ToRssSkipHours();
                            break;
                        case "skipDays":
                            skipDays = child.ToRssSkipDays();
                            break;
                        case "category":
                            var category = child.ToRssCategory();
                            if (category != null)
                                categories.Add(category);
                            break;
                        case "item":
                            var item = child.ToRssItem(namespaces);
                            if (item != null)
                                items.Add(item);
                            break;
                        default:
                            break;
                    }
                }
            }

            return new RssChannel(title, link, description, language, copyright, managingEditor, webMaster, pubDate, lastBuildDate, generator, docs, cloud, ttl, image, rating, textInput, skipHours, skipDays, categories, items, extensions);
        }

        private static IRssCloud ToRssCloud(this XmlNode node)
        {
            if (node == null)
                return null;

            string domain = null;
            int port = -1;
            string path = null;
            string registerProcedure = null;
            var protocol = RssCloudProtocol.None;

            foreach (var attrib in node.Attributes.Cast<XmlAttribute>())
            {
                if (attrib != null)
                {
                    switch (attrib.Name)
                    {
                        case "domain":
                            domain = attrib.Value;
                            break;
                        case "port":
                            int.TryParse(attrib.Value, out port);
                            break;
                        case "path":
                            path = attrib.Value;
                            break;
                        case "registerProcedure":
                            registerProcedure = attrib.Value;
                            break;
                        case "protocol":
                            if (attrib.Value == "http-post")
                                protocol = RssCloudProtocol.HttpPost;
                            else if (attrib.Value == "soap")
                                protocol = RssCloudProtocol.Soap;
                            else if (attrib.Value == "xml-rpc")
                                protocol = RssCloudProtocol.XmlRpc;
                            break;
                        default:
                            break;
                    }
                }
            }

            return (domain != null && port > -1 && path != null && registerProcedure != null && protocol != RssCloudProtocol.None) ?
                new RssCloud(domain, port, path, registerProcedure, protocol)
                : null;
        }

        private static IRssEnclosure ToRssEnclosure(this XmlNode node)
        {
            if (node == null)
                return null;

            Uri url = null;
            IMediaType type = null;
            int length = 0;

            foreach (var attrib in node.Attributes.Cast<XmlAttribute>())
            {
                if (attrib != null)
                {
                    switch (attrib.Name)
                    {
                        case "url":
                            url = attrib.Value.ToUri();
                            break;
                        case "type":
                            type = MediaType.Parse(attrib.Value);
                            break;
                        case "length":
                            length = attrib.Value.ToInt32();
                            break;
                        default:
                            break;
                    }
                }
            }

            return (url != null && type != null) ?
                new RssEnclosure(url, type, length)
                : null;
        }

        private static IRssGuid ToRssGuid(this XmlNode node)
        {
            if (node == null)
                return null;

            string value = node.InnerText;
            var isPermaLink = true;

            var attrib = node.Attributes.Cast<XmlAttribute>().Where(x => x.Name == "isPermaLink").FirstOrDefault();
            if (attrib != null)
                bool.TryParse(attrib.Value, out isPermaLink);

            return value != null ?
                new RssGuid(value, isPermaLink)
                : null;
        }

        private static IRssImage ToRssImage(this XmlNode node)
        {
            if (node == null)
                return null;

            Uri url = null;
            string title = null;
            Uri link = null;
            int width = 0;
            int height = 0;
            string desciption = null;

            foreach (var child in node.ChildNodes.Cast<XmlNode>())
            {
                if (child != null)
                {
                    switch (child.Name)
                    {
                        case "url":
                            url = new Uri(child.InnerText, UriKind.RelativeOrAbsolute);
                            break;
                        case "title":
                            title = child.InnerText;
                            break;
                        case "link":
                            link = new Uri(child.InnerText, UriKind.RelativeOrAbsolute);
                            break;
                        case "width":
                            int.TryParse(child.InnerText, out width);
                            break;
                        case "height":
                            int.TryParse(child.InnerText, out height);
                            break;
                        case "description":
                            desciption = child.InnerText;
                            break;
                        default:
                            break;
                    }
                }
            }

            return (url != null && title != null && link != null) ?
                new RssImage(url, title, link, width, height, desciption)
                : null;
        }

        private static IRssItem ToRssItem(this XmlNode self, IEnumerable<IXmlNamespace> globalNamespaces)
        {
            if (self == null)
                return null;

            string title = null;
            Uri link = null;
            string description = null;
            string author = null;
            Uri comments = null;
            IRssEnclosure enclosure = null;
            IRssGuid guid = null;
            DateTime pubDate = DateTime.MinValue;
            IRssSource source = null;

            IList<IRssCategory> categories = new List<IRssCategory>();
            IList<IXmlExtension> extensions = new List<IXmlExtension>();
            var namespaces = self.ToXmlNamespaces();
            var primaryNamespace = namespaces.Where(x => x != null && string.IsNullOrEmpty(x.Prefix)).FirstOrDefault();

            foreach (var child in self.ChildNodes.Cast<XmlElement>())
            {
                if (child != null)
                {
                    if (child.Name.Contains(':'))
                    {
                        var extension = child.ToXmlExtension(globalNamespaces);
                        if (extension != null)
                            extensions.Add(extension);
                    }
                    else
                    {
                        switch (child.Name)
                        {
                            case "title":
                                title = child.InnerText;
                                break;
                            case "link":
                                link = child.InnerText.ToUri();
                                break;
                            case "description":
                                description = child.InnerText;
                                break;
                            case "author":
                                author = child.InnerText;
                                break;
                            case "comments":
                                comments = child.InnerText.ToUri();
                                break;
                            case "enclosure":
                                enclosure = child.ToRssEnclosure();
                                break;
                            case "guid":
                                guid = child.ToRssGuid();
                                break;
                            case "pubDate":
                                pubDate = child.InnerText.ToDateTime();
                                break;
                            case "source":
                                source = child.ToRssSource();
                                break;
                            case "category":
                                var category = child.ToRssCategory();
                                if (category != null)
                                    categories.Add(category);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return (title != null || description != null) ?
                new RssItem(extensions, namespaces, primaryNamespace, title, link, description, author, comments, enclosure, guid, pubDate, source, categories)
                : null;
        }

        private static IEnumerable<RssDay> ToRssSkipDays(this XmlNode node)
        {
            var skipDays = new List<RssDay>();
            if (node == null)
                return skipDays;

            foreach (var child in node.ChildNodes.Cast<XmlNode>())
            {
                if (child != null && child.Name == "day")
                {
                    switch (child.InnerText)
                    {
                        case "Sunday":
                            skipDays.Add(RssDay.Sunday);
                            break;
                        case "Monday":
                            skipDays.Add(RssDay.Monday);
                            break;
                        case "Tuesday":
                            skipDays.Add(RssDay.Tuesday);
                            break;
                        case "Wednesday":
                            skipDays.Add(RssDay.Wednesday);
                            break;
                        case "Thursday":
                            skipDays.Add(RssDay.Thursday);
                            break;
                        case "Friday":
                            skipDays.Add(RssDay.Friday);
                            break;
                        case "Saturday":
                            skipDays.Add(RssDay.Saturday);
                            break;
                        default:
                            break;
                    }
                }
            }

            return skipDays;
        }

        private static IEnumerable<RssHour> ToRssSkipHours(this XmlNode node)
        {
            var skipHours = new List<RssHour>();
            if (node == null)
                return skipHours;

            foreach (var child in node.ChildNodes.Cast<XmlNode>())
            {
                if (child != null && child.Name == "hour")
                {
                    var hour = -1;
                    if (int.TryParse(child.InnerText, out hour))
                    {
                        if (hour >= 0 && hour <= 23)
                            skipHours.Add((RssHour)hour);
                    }
                }
            }

            return skipHours;
        }

        private static IRssSource ToRssSource(this XmlNode node)
        {
            if (node == null)
                return null;

            Uri url = null;
            string name = node.InnerText;

            var attrib = node.FindAttribute("url");
            if (attrib != null && attrib.Value != null)
                url = attrib.Value.ToUri();

            return name != null && url != null ?
                new RssSource(url, name)
                : null;
        }

        private static IRssTextInput ToRssTextInput(this XmlNode node)
        {
            if (node == null)
                return null;

            string title = null;
            string description = null;
            string name = null;
            Uri link = null;

            foreach (var child in node.ChildNodes.Cast<XmlNode>())
            {
                if (child != null)
                {
                    switch (child.Name)
                    {
                        case "title":
                            title = child.InnerText;
                            break;
                        case "description":
                            description = child.InnerText;
                            break;
                        case "name":
                            name = child.InnerText;
                            break;
                        case "link":
                            link = child.InnerText.ToUri();
                            break;
                        default:
                            break;
                    }
                }
            }

            return (title != null && description != null && name != null && link != null) ?
                new RssTextInput(title, description, name, link)
                : null;
        }

        private static TimeSpan ToRssTtl(this XmlNode node)
        {
            if (node == null)
                return TimeSpan.Zero;

            var minutes = 0;
            return int.TryParse(node.InnerText, out minutes) ?
                new TimeSpan(0, minutes, 0)
                : TimeSpan.Zero;
        }

        #endregion

        public static IRssFeed ToRssFeed(this XmlNode self, ICharacterSet encoding, IEnumerable<IXmlNamespace> namespaces, IEnumerable<IXmlStyleSheet> styleSheets)
        {
            IRssChannel channel = null;
            var version = self.GetAttributeString("version");
            var baseId = self.GetAttributeUri("xml:base");
            var extensions = new List<IXmlExtension>();

            foreach (var extNode in self.ChildNodes.Cast<XmlNode>().Where(x => x.Name != null && x.Name.Contains(":")))
            {
                var extension = extNode.ToXmlExtension(namespaces);
                if (extension != null)
                    extensions.Add(extension);
            }

            var child = self.FindChild("channel");
            if (child != null)
                channel = child.ToRssChannel(namespaces);

            return channel != null ?
                new RssFeed(channel, version, baseId, encoding, extensions, namespaces, styleSheets)
                : null;
        }
    }
}
