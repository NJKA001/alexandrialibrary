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
                builder.AppendFormat(";{0}{1}", charSetFieldName, charSet.ToString());

            if (!string.IsNullOrEmpty(boundary))
                builder.AppendFormat(";{0}{1}", boundaryFieldName, boundary);

            return builder.ToString();
        }

        public static readonly IContentType Empty = new ContentType(MediaType.Unknown);

        #region Public Static Methods

        public static IContentType GetContentType(Uri location)
        {
            if (location == null)
                return ContentType.Empty;

            var mediaType = MediaType.Unknown;
            ICharacterSet charSet = null;
            string boundary = null;

            if (location.IsFile)
            {
                if (!System.IO.File.Exists(location.LocalPath))
                    return ContentType.Empty;

                var fileInfo = new FileInfo(location.LocalPath);
                var header = fileInfo.GetHeader();
                mediaType = MediaType.GetMediaTypeByMagicNumber(header);
                if (mediaType != MediaType.Unknown)
                    return new ContentType(mediaType);

                var extension = location.ToExtension();
                if (!string.IsNullOrEmpty(extension))
                {
                    mediaType = MediaType.GetMediaTypesByFileExtension(extension).FirstOrDefault();
                }

                return new ContentType(mediaType);
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

                if (mediaType != MediaType.Unknown && mediaType != MediaType.XmlDoc)
                {
                    if (mediaType.Type == MediaType.TypeText && charSet == null)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream(), true))
                        {
                            var buffer = new char[20];
                            reader.Read(buffer, 0, 20);
                            charSet = CharacterSet.GetCharacterSet(reader.CurrentEncoding);
                        }
                    }

                    return new ContentType(mediaType, charSet, boundary);
                }

                if (mediaType == MediaType.XmlDoc)
                {
                    try
                    {
                        var content = response.GetResponseStream().ToContentString();
                        if (content != null)
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(content);

                            foreach (var node in xml.ChildNodes)
                            {
                                var declaration = node as XmlDeclaration;
                                if (declaration != null)
                                {
                                    charSet = CharacterSet.Parse(declaration.Encoding);
                                }
                                else
                                {
                                    var element = node as XmlElement;
                                    if (element != null)
                                    {
                                        //System.Diagnostics.Debug.WriteLine(element.Name);
                                        if (element.Name == "rss")
                                        {
                                            mediaType = MediaType.RssFeed;
                                        }
                                        else if (element.Name == "feed")
                                        {
                                            mediaType = MediaType.AtomFeed;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        return new ContentType(MediaType.XmlDoc, charSet, boundary);
                    }
                }
            }
            else
            {
                var header = response.GetResponseStream().GetHeader();
                mediaType = MediaType.GetMediaTypeByMagicNumber(header);
                if (mediaType != MediaType.Unknown)
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
