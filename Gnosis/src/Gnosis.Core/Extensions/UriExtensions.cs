using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

using Gnosis.Core.Document.Xml;
using Gnosis.Core.Document.Xml.Xhtml;

namespace Gnosis.Core
{
    public static class UriExtensions
    {
        /*
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
            foreach (var child in xml.ChildNodes.Cast<XmlNode>().Where(node => node != null))
            {
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

                        feed = child.ToAtomFeed(encoding, styleSheets);
                        break;
                    default:
                        break;
                }
            }

            return feed;
        }
        */

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

        public static IXmlDocument ToXmlDocument(this Uri self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var xml = self.ToContentString();
            return XmlDocument.Parse(xml);
        }

        public static IXmlDocument ToXhtmlDocument(this Uri self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var html = self.ToContentString();
            return XhtmlDocument.Parse(html);
        }

        /*
        public static IRssFeed ToRssFeed(this Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            var contentType = location.ToContentType();
            if (contentType.Type != MediaType.ApplicationRssXml)
                throw new InvalidOperationException("The resource at this location is not a valid RSS feed");

            IRssFeed feed = null;
            var encoding = CharacterSet.Utf8;
            IEnumerable<IXmlNamespace> namespaces = new List<IXmlNamespace>();
            var styleSheets = new List<IXmlStyleSheet>();

            var xml = location.ToXml();
            foreach (var child in xml.ChildNodes.Cast<XmlNode>().Where(node => node != null))
            {
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

                        namespaces = child.ToXmlNamespaces();
                        feed = child.ToRssFeed(encoding, namespaces, styleSheets);
                        break;
                    default:
                        break;
                }
            }

            return feed;
        }
        */

        public static string ToContentString(this Uri self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            if (self.IsFile)
            {
                using (var reader = new StreamReader(self.LocalPath))
                {
                    return reader.ReadToEnd();
                }
            }
            else
            {
                var request = HttpWebRequest.Create(self);
                var response = request.GetResponse();
                return response.GetResponseStream().ToContentString();
            }
        }

        public static System.Xml.XmlDocument ToXml(this Uri location)
        {
            var xml = new System.Xml.XmlDocument();

            if (location.IsFile)
            {
                using (var reader = System.Xml.XmlReader.Create(location.LocalPath))
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

        public static string ToXmlEscapedString(this Uri location)
        {
            if (location == null)
                return null;

            return location.ToString().ToXmlEscapedString();
        }

        public static bool TryParse(string value, out Uri result)
        {
            try
            {
                var uri = new Uri(value, UriKind.RelativeOrAbsolute);
                result = uri;
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }
    }
}
