using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace Gnosis.Core.W3c
{
    public class ContentType
        : IContentType
    {
        private ContentType(IMediaType type)
            : this(type, null, null)
        {
        }

        private ContentType(IMediaType type, ICharacterSet charSet)
            : this(type, charSet, null)
        {
        }

        private ContentType(IMediaType type, ICharacterSet charSet, string boundary)
        {
            this.type = type;
            this.boundary = boundary;
            this.charSet = charSet;
        }

        private readonly IMediaType type;
        private readonly ICharacterSet charSet;
        private readonly string boundary;

        private const string charSetFieldName = "charset=";
        private const string boundaryFieldName = "boundary=";

        #region IContentType Members

        public IMediaType Type
        {
            get { return type; }
        }

        public ICharacterSet CharSet
        {
            get { return charSet; }
        }

        public string Boundary
        {
            get { return boundary; }
        }

        #endregion

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(type.ToString());

            if (charSet != null && charSet != CharacterSet.Unknown)
                builder.AppendFormat("; {0}{1}", charSetFieldName, charSet.ToString());

            if (!string.IsNullOrEmpty(boundary))
                builder.AppendFormat("; {0}{1}", boundaryFieldName, boundary);

            return builder.ToString();
        }

        public static readonly IContentType Empty = new ContentType(MediaType.ApplicationUnknown);

        #region Public Static Methods

        private static ICharacterSet GetCharacterSet(Stream stream, IMediaType mediaType, ICharacterSet charSet)
        {
            if (mediaType != MediaType.ApplicationUnknown && !mediaType.SubType.Contains("xml"))
            {
                if (mediaType.Type == MediaType.TypeText && charSet == null)
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        var buffer = new char[20];
                        reader.Read(buffer, 0, 20);
                        return CharacterSet.GetCharacterSet(reader.CurrentEncoding);
                    }
                }

                //System.Diagnostics.Debug.WriteLine("response.ContentType=" + response.ContentType + " mediaType.SubType=" + mediaType.SubType);
                //return new ContentType(mediaType, charSet, boundary);
            }

            return charSet;
        }

        private static Tuple<IMediaType, ICharacterSet> GetXmlExtendedType(Stream stream, IMediaType mediaType, ICharacterSet charSet)
        {
            var newMediaType = mediaType;
            var newCharSet = charSet;

            //System.Diagnostics.Debug.WriteLine("GetXmlExtendedType. mediaType=" + mediaType);
            if (mediaType.SubType.Contains("xml"))
            {
                try
                {
                    var content = stream.ToContentString();
                    if (content != null)
                    {
                        var xml = new XmlDocument();
                        xml.LoadXml(content);

                        foreach (var node in xml.ChildNodes)
                        {
                            var declaration = node as XmlDeclaration;
                            if (declaration != null)
                            {
                                newCharSet = CharacterSet.Parse(declaration.Encoding);
                            }
                            else
                            {
                                if (newMediaType == mediaType)
                                {
                                    var element = node as XmlElement;
                                    if (element != null)
                                    {
                                        //System.Diagnostics.Debug.WriteLine("elementName=" + element.Name);
                                        if (element.Name == "rss")
                                        {
                                            newMediaType = MediaType.ApplicationRssXml;
                                        }
                                        else if (element.Name == "feed")
                                        {
                                            newMediaType = MediaType.ApplicationAtomXml;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    newMediaType = MediaType.ApplicationXml;
                }
            }

            return new Tuple<IMediaType, ICharacterSet>(newMediaType, newCharSet);
        }

        public static IContentType GetContentType(Uri location)
        {
            if (location == null)
                return ContentType.Empty;

            var mediaType = MediaType.ApplicationUnknown;
            ICharacterSet charSet = null;
            string boundary = null;

            if (location.IsFile)
            {
                if (!System.IO.File.Exists(location.LocalPath))
                    return ContentType.Empty;

                var fileInfo = new FileInfo(location.LocalPath);
                var header = fileInfo.GetHeader();
                mediaType = MediaType.GetMediaTypeByMagicNumber(header);
                if (mediaType != MediaType.ApplicationUnknown)
                    return new ContentType(mediaType);

                var extension = location.ToExtension();
                if (!string.IsNullOrEmpty(extension))
                {
                    mediaType = MediaType.GetMediaTypesByFileExtension(extension).FirstOrDefault();
                }

                using (var stream = new FileStream(location.LocalPath, FileMode.Open, FileAccess.Read))
                {
                    charSet = GetCharacterSet(stream, mediaType, charSet);
                    var ext = GetXmlExtendedType(stream, mediaType, charSet);
                    mediaType = ext.Item1;
                    charSet = ext.Item2;
                }

                return new ContentType(mediaType, charSet, boundary);
            }

            var request = HttpWebRequest.Create(location);
            var response = request.GetResponse();

            if (!string.IsNullOrEmpty(response.ContentType))
            {
                var tokens = response.ContentType.Split(new string[] { "; ", " ", ";" }, StringSplitOptions.RemoveEmptyEntries);
                mediaType = MediaType.Parse(tokens[0].Trim());
                
                if (tokens.Length > 1)
                {
                    var token = string.Empty;
                    for (var i = 1; i < tokens.Length; i++)
                    {
                        token = tokens[i].Trim();

                        if (token.Contains(charSetFieldName) && token.Length > 8)
                        {
                            charSet = CharacterSet.Parse(token.Substring(8).Trim());
                        }
                        else if (token.Contains(boundaryFieldName) && token.Length > 9)
                        {
                            boundary = token.Substring(9);
                        }
                    }
                }

                using (var stream = response.GetResponseStream())
                {
                    charSet = GetCharacterSet(stream, mediaType, charSet);
                    var ext = GetXmlExtendedType(stream, mediaType, charSet);
                    mediaType = ext.Item1;
                    charSet = ext.Item2;
                }

                return new ContentType(mediaType, charSet, boundary);
            }
            else
            {
                var header = response.GetResponseStream().GetHeader();
                mediaType = MediaType.GetMediaTypeByMagicNumber(header);
                if (mediaType != MediaType.ApplicationUnknown)
                    return new ContentType(mediaType, charSet, boundary);

                var extension = location.ToExtension();
                if (!string.IsNullOrEmpty(extension))
                {
                    mediaType = MediaType.GetMediaTypesByFileExtension(extension).FirstOrDefault();
                }
            }

            return new ContentType(mediaType, charSet, boundary);
        }

        #endregion
    }
}
