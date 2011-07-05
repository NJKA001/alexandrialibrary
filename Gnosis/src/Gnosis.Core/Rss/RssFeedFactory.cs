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
        private static string ProcessRssNode(XmlElement rssNode, IList<IXmlNamespace> namespaces)
        {
            string version = null;
            if (rssNode.Attributes != null)
            {
                foreach (var attrib in rssNode.Attributes)
                {
                    var attribute = attrib as XmlAttribute;
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

        private static void AddExtension(XmlNode node, IList<IRssExtension> extensions)
        {
        }

        private static void AddCategory(XmlNode node, IList<IRssCategory> categories)
        {
        }

        private static void AddItem(XmlNode node, IList<IRssItem> items)
        {
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
            var skipHours = new List<RssHour>();
            var skipDays = new List<RssDay>();

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
                    AddExtension(child, extensions);
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
                        //TODO: Add logic for cloud, ttl, image, rating, textInput, skipHours, skipDays
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
