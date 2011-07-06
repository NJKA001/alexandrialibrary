using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

using Gnosis.Core.Rss;
using Gnosis.Core.W3c;

namespace Gnosis.Core
{
    public static class UriExtensions
    {
        public const string EmptyUriPath = "urn:empty";

        public static readonly Uri EmptyUri = new Uri(EmptyUriPath);

        public static bool IsEmpty(this Uri self)
        {
            if (self == null)
                return false;

            return self.AbsolutePath == EmptyUriPath;
        }

        private static string GetMd5Hash(Uri url)
        {
            var request = HttpWebRequest.Create(url);
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    return stream.AsMd5Hash();
                }
            }
        }

        private static string GetMd5Hash(string fileName)
        {
            using (var file = new FileStream(fileName, FileMode.Open))
            {
                return file.AsMd5Hash();
            }
        }

        public static string ToMd5Hash(this Uri location)
        {
            try
            {
                if (location.IsFile)
                {
                    return GetMd5Hash(location.LocalPath);
                }
                else
                {
                    return GetMd5Hash(location);
                }
            }
            catch
            {
                return null;
            }
        }

        public static string ToExtension(this Uri location)
        {
            if (location == null)
                return string.Empty;

            var dotIndex = location.AbsolutePath.LastIndexOf('.');
            var slashIndex = location.AbsolutePath.LastIndexOf('/');
            if (dotIndex > slashIndex)
                return location.AbsolutePath.Substring(dotIndex);
            
            return string.Empty;
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

        public static XmlDocument ToXml(this Uri location)
        {
            var request = HttpWebRequest.Create(location);
            var response = request.GetResponse();
            var xml = new XmlDocument();
            var content = response.GetResponseStream().ToContentString();
            xml.LoadXml(content);

            return xml;
        }

        public static IRssFeed ToRssFeed(this Uri location)
        {
            var contentType = location.ToContentType();
            if (contentType.Type != MediaType.ApplicationRssXml)
                throw new InvalidOperationException("The resource at this location is not a valid RSS feed");

            IRssChannel channel = null;
            var encoding = CharacterSet.Utf8;
            string version = null;
            IEnumerable<IXmlNamespace> namespaces = new List<IXmlNamespace>();
            var styleSheets = new List<IXmlStyleSheet>();

            var xml = location.ToXml();
            foreach (var child in xml.ChildNodes.Cast<XmlNode>())
            {
                if (child != null)
                {
                    switch (child.NodeType)
                    {
                        case XmlNodeType.XmlDeclaration:
                            encoding = child.ToEncoding();
                            break;
                        case XmlNodeType.ProcessingInstruction:
                            var styleSheet = child.ToXmlStyleSheet();
                            if (styleSheet != null)
                                styleSheets.Add(styleSheet);
                            break;
                        case XmlNodeType.Element:
                            if (child.Name == "rss")
                            {
                                version = child.ToRssVersion();
                                namespaces = child.ToXmlNamespaces();
                                var channelNode = child.FindChild("channel");
                                channel = (channelNode != null) ? channelNode.ToRssChannel(namespaces) : null;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return (channel != null) ?
                new RssFeed(channel, encoding, version, namespaces, styleSheets)
                : null;
        }
    }
}
