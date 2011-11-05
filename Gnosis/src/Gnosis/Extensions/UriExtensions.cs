using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;

namespace Gnosis
{
    public static class UriExtensions
    {
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

        public static byte[] ToContentData(this Uri self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            if (self.IsFile)
            {
                return File.ReadAllBytes(self.LocalPath);
            }
            else
            {
                var request = HttpWebRequest.Create(self);
                var response = request.GetResponse();
                return response.GetResponseStream().ToBuffer();
            }
        }

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

        private static XmlDocument ToXml(this Uri location)
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
