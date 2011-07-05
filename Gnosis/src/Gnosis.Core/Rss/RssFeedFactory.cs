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
    public class RssFeedFactory
        : IRssFeedFactory
    {
        private static XmlNode FindChannelNode(XmlElement parent)
        {
            if (parent != null && parent.HasChildNodes)
            {
                foreach (var child in parent.ChildNodes)
                {
                    var childNode = child as XmlNode;
                    if (childNode != null && childNode.Name == "channel")
                    {
                        return childNode;
                    }
                }
            }

            return null;
        }

        private static void AddExtension(XmlNode node, IList<IRssExtension> extensions, IList<IXmlNamespace> namespaces)
        {
            if (node.Attributes != null)
            {
                foreach (var attrib in node.Attributes.Cast<XmlAttribute>())
                {
                    if (attrib != null && attrib.Name.StartsWith("xmlns"))
                    {
                        if (attrib.Name.Contains(':'))
                        {
                        }
                    }
                }
            }
        }

        private static void AddCategory(XmlNode node, IList<IRssCategory> categories)
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

            if (name != null)
            {
                categories.Add(new RssCategory(domain, name));
            }
        }

        private static void AddItem(XmlNode node, IList<IRssItem> items)
        {
        }

        private static IRssCloud GetCloud(XmlNode node)
        {
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

        private static TimeSpan GetTtl(XmlNode node)
        {
            var minutes = 0;
            return int.TryParse(node.InnerText, out minutes) ?
                new TimeSpan(0, minutes, 0)
                : TimeSpan.Zero;
        }

        private static IRssImage GetImage(XmlNode node)
        {
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

        private static IRssTextInput GetTextInput(XmlNode node)
        {
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
                            link = new Uri(child.InnerText, UriKind.RelativeOrAbsolute);
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

        private static IEnumerable<RssDay> GetSkipDays(XmlNode node)
        {
            var skipDays = new List<RssDay>();

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

        private static IEnumerable<RssHour> GetSkipHours(XmlNode node)
        {
            var skipHours = new List<RssHour>();

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

        private static IRssChannel GetChannel(XmlElement rssNode, IList<IXmlNamespace> namespaces)
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
            var extensions = new List<IRssExtension>();

            var channelNode = FindChannelNode(rssNode);
            if (channelNode == null)
                return null;

            foreach (var node in channelNode.ChildNodes)
            {
                var child = node as XmlNode;
                if (child.Name.Contains(':'))
                {
                    AddExtension(child, extensions, namespaces);
                }
                else
                {
                    switch (child.Name)
                    {
                        case "title":
                            title = child.InnerText;
                            break;
                        case "link":
                            link = new Uri(child.InnerText, UriKind.RelativeOrAbsolute);
                            break;
                        case "description":
                            description = child.InnerText;
                            break;
                        case "language":
                            language = LanguageTag.Parse(child.InnerText);
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
                            pubDate = Rfc822DateTime.Parse(child.InnerText);
                            break;
                        case "lastBuildDate":
                            lastBuildDate = Rfc822DateTime.Parse(child.InnerText);
                            break;
                        case "generator":
                            generator = child.InnerText;
                            break;
                        case "docs":
                            docs = new Uri(child.InnerText, UriKind.RelativeOrAbsolute);
                            break;
                        case "cloud":
                            cloud = GetCloud(child);
                            break;
                        case "ttl":
                            ttl = GetTtl(child);
                            break;
                        case "image":
                            image = GetImage(child);
                            break;
                        case "rating":
                            rating = new PicsRating(child.InnerText);
                            break;
                        case "textInput":
                            textInput = GetTextInput(child);
                            break;
                        case "skipHours":
                            skipHours = GetSkipHours(child);
                            break;
                        case "skipDays":
                            skipDays = GetSkipDays(child);
                            break;
                        case "category":
                            AddCategory(child, categories);
                            break;
                        case "item":
                            AddItem(child, items);
                            break;
                        default:
                            break;
                    }
                }
                //System.Diagnostics.Debug.WriteLine("channel child node. name=" + child.Name + " type=" + child.NodeType);
            }

            return new RssChannel(title, link, description, language, copyright, managingEditor, webMaster, pubDate, lastBuildDate, generator, docs, cloud, ttl, image, rating, textInput, skipHours, skipDays, categories, items, extensions);
        }

        #region AddXmlStyleSheet

        private static void AddXmlStyleSheet(XmlProcessingInstruction node, IList<IXmlStyleSheet> styleSheets)
        {
            var type = MediaType.TextCss;
            var media = Media.All;
            Uri href = null;

            var tokens = node.InnerText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var token in tokens)
            {
                if (token != null && token.Contains('='))
                {
                    var pieces = token.Split('=');
                    var name = pieces[0];
                    var value = pieces[1].Replace("'", string.Empty).Replace("\"", string.Empty);

                    switch (name.ToLower())
                    {
                        case "type":
                            type = MediaType.Parse(value);
                            break;
                        case "media":
                            media = Media.Parse(value);
                            break;
                        case "href":
                            href = new Uri(value, UriKind.RelativeOrAbsolute);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (href != null)
            {
                styleSheets.Add(new XmlStyleSheet(type, media, href));
            }
        }

        #endregion

        #region ProcessRssNode

        private static string ProcessRssNode(XmlElement rssNode, IList<IXmlNamespace> namespaces)
        {
            string version = null;
            if (rssNode.Attributes != null)
            {
                foreach (var attribute in rssNode.Attributes.Cast<XmlAttribute>())
                {
                    if (attribute != null)
                    {
                        if (attribute.Name == "version")
                            version = attribute.Value;
                        else if (attribute.Name.StartsWith("xmlns"))
                        {
                            var identifier = new Uri(attribute.Value, UriKind.RelativeOrAbsolute);

                            if (attribute.Name == "xmlns")
                            {
                                namespaces.Add(new XmlNamespace(identifier));
                            }
                            else if (attribute.Name.Contains(':'))
                            {
                                var tokens = attribute.Name.Split(':');
                                if (tokens != null && tokens.Length == 2)
                                {
                                    namespaces.Add(new XmlNamespace(identifier, tokens[1]));
                                }
                            }
                        }
                    }
                }
            }

            return version;
        }

        #endregion

        #region IRssFeedFactory Members

        public IRssFeed Create(Uri location)
        {
            var contentType = location.ToContentType();
            if (contentType.Type != MediaType.ApplicationRssXml)
                throw new InvalidOperationException("The resource at this location is not a valid RSS feed");

            IRssChannel channel = null;
            string version = null;
            var namespaces = new List<IXmlNamespace>();
            var styleSheets = new List<IXmlStyleSheet>();

            var xml = location.ToXml();
            foreach (var child in xml.ChildNodes)
            {
                var node = child as XmlNode;
                if (node != null)
                {
                    if (node.NodeType == XmlNodeType.XmlDeclaration)
                    {
                    }
                    else if (node.NodeType == XmlNodeType.ProcessingInstruction)
                    {
                        if (node.Name == "xml-stylesheet")
                            AddXmlStyleSheet(node as XmlProcessingInstruction, styleSheets);
                    }
                    else if (node.NodeType == XmlNodeType.Element)
                    {
                        if (node.Name == "rss")
                        {
                            var rssNode = node as XmlElement;
                            version = ProcessRssNode(rssNode, namespaces);
                            channel = GetChannel(rssNode, namespaces);
                        }
                    }
                }
            }

            return (channel != null) ?
                new RssFeed(channel, version, namespaces, styleSheets)
                : null;

        }

        #endregion
    }
}
