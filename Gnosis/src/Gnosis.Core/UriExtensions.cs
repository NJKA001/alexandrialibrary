using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

using Gnosis.Core.Atom;
using Gnosis.Core.Rss;
using Gnosis.Core.W3c;

namespace Gnosis.Core
{
    public static class UriExtensions
    {
        public static IAtomFeed ToAtomFeed(this Uri location)
        {
            var contentType = location.ToContentType();
            if (contentType.Type != MediaType.ApplicationAtomXml)
                throw new InvalidOperationException("The resource at this location is not a valid Atom feed");

            IAtomFeed feed = null;
            var encoding = CharacterSet.Utf8;
            IEnumerable<IXmlNamespace> namespaces = new List<IXmlNamespace>();
            var styleSheets = new List<IXmlStyleSheet>();

            var xml = location.ToXml();
            foreach (var child in xml.ChildNodes.Cast<XmlNode>())
            {
                if (child == null)
                    continue;

                switch (child.NodeType)
                {
                    case XmlNodeType.XmlDeclaration:
                        encoding = child.ToEncoding();
                        break;
                    case XmlNodeType.ProcessingInstruction:
                        styleSheets.AddIfNotNull(child.ToXmlStyleSheet());
                        break;
                    case XmlNodeType.Element:
                        if (child.Name != "feed")
                            break;

                        namespaces = child.ToXmlNamespaces();
                        feed = child.ToAtomFeed(encoding, namespaces, styleSheets);
                        break;
                    default:
                        break;
                }
            }

            return feed;
        }

        public static IContentType ToContentType(this Uri location)
        {
            if (location == null)
                return ContentType.Empty;

            try
            {
                return ContentType.GetContentType(location);
            }
            catch
            {
                return ContentType.Empty;
            }
        }

        public static string ToFileExtension(this Uri location)
        {
            if (location == null)
                return string.Empty;

            var dotIndex = location.AbsolutePath.LastIndexOf('.');
            var slashIndex = location.AbsolutePath.LastIndexOf('/');
            if (dotIndex > slashIndex)
                return location.AbsolutePath.Substring(dotIndex);
            
            return string.Empty;
        }

        public static string ToMd5Hash(this Uri location)
        {
            try
            {
                if (location.IsFile)
                {
                    using (var file = new FileStream(location.LocalPath, FileMode.Open))
                    {
                        return file.ToMd5Hash();
                    }
                }
                else
                {
                    var request = HttpWebRequest.Create(location);
                    using (var response = request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            return stream.ToMd5Hash();
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static IMediaType ToMediaType(this Uri location)
        {
            if (location == null)
                return MediaType.ApplicationUnknown;

            try
            {
                var contentType = location.ToContentType();
                return contentType.Type;
            }
            catch
            {
                return MediaType.ApplicationUnknown;
            }
        }

        public static IRssFeed ToRssFeed(this Uri location)
        {
            var contentType = location.ToContentType();
            if (contentType.Type != MediaType.ApplicationRssXml)
                throw new InvalidOperationException("The resource at this location is not a valid RSS feed");

            IRssChannel channel = null;
            string version = null;
            var encoding = CharacterSet.Utf8;
            IEnumerable<IXmlNamespace> namespaces = new List<IXmlNamespace>();
            var styleSheets = new List<IXmlStyleSheet>();

            var xml = location.ToXml();
            foreach (var child in xml.ChildNodes.Cast<XmlNode>())
            {
                if (child == null)
                    continue;

                switch (child.NodeType)
                {
                    case XmlNodeType.XmlDeclaration:
                        encoding = child.ToEncoding();
                        break;
                    case XmlNodeType.ProcessingInstruction:
                        styleSheets.AddIfNotNull(child.ToXmlStyleSheet());
                        break;
                    case XmlNodeType.Element:
                        if (child.Name != "rss")
                            break;

                        version = child.ToRssVersion();
                        namespaces = child.ToXmlNamespaces();
                        channel = child.FindChild("channel").ToRssChannel(namespaces);
                        break;
                    default:
                        break;
                }
            }

            return (channel != null) ?
                new RssFeed(channel, version, encoding, namespaces, styleSheets)
                : null;
        }

        public static XmlDocument ToXml(this Uri location)
        {
            var xml = new XmlDocument();

            if (location.IsFile)
            {
                using (var reader = XmlReader.Create(location.LocalPath))
                {
                    xml.Load(reader);
                }
            }
            else
            {
                var request = HttpWebRequest.Create(location);
                var response = request.GetResponse();
                var content = response.GetResponseStream().ToContentString();
                xml.LoadXml(content);
            }

            return xml;
        }
    }
}
