using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace Gnosis
{
    public class ContentTypeFactory
        : IContentTypeFactory
    {
        public ContentTypeFactory(ILogger logger, IMediaTypeFactory mediaTypeFactory)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (mediaTypeFactory == null)
                throw new ArgumentNullException("mediaTypeFactory");

            this.logger = logger;
            this.mediaTypeFactory = mediaTypeFactory;
            defaultContentType = new ContentType(mediaTypeFactory.Default);
        }

        private readonly ILogger logger;
        private readonly IMediaTypeFactory mediaTypeFactory;
        private readonly IContentType defaultContentType;

        private const string charSetFieldName = "charset=";
        private const string boundaryFieldName = "boundary=";

        private ICharacterSet GetCharacterSet(Stream stream, IMediaType mediaType, ICharacterSet charSet)
        {
            if (mediaType != mediaTypeFactory.Default && !mediaType.Subtype.Contains("xml"))
            {
                if (mediaType.Supertype == MediaSupertype.Text && charSet == null)
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

        private Tuple<IMediaType, ICharacterSet> GetXmlExtendedType(Stream stream, IMediaType mediaType, ICharacterSet charSet)
        {
            var newMediaType = mediaType;
            var newCharSet = charSet;

            //System.Diagnostics.Debug.WriteLine("GetXmlExtendedType. mediaType=" + mediaType);
            if (mediaType.Subtype.Contains("xml"))
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
                                            newMediaType = mediaTypeFactory.GetByCode("application/rss+xml");
                                        }
                                        else if (element.Name == "feed")
                                        {
                                            newMediaType = mediaTypeFactory.GetByCode("application/atom+xml");
                                        }
                                        else if (element.Name == "playlist")
                                        {
                                            newMediaType = mediaTypeFactory.GetByCode("application/xspf+xml");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    newMediaType = mediaTypeFactory.GetByCode("application/xml");
                }
            }

            return new Tuple<IMediaType, ICharacterSet>(newMediaType, newCharSet);
        }

        public IContentType Default
        {
            get { return defaultContentType; }
        }

        public IContentType GetByLocation(Uri location)
        {
            try
            {
                if (location == null)
                    return Default;

                var mediaType = mediaTypeFactory.Default;
                ICharacterSet charSet = null;
                string boundary = null;

                if (location.IsFile)
                {
                    if (!System.IO.File.Exists(location.LocalPath))
                        return Default;

                    var fileInfo = new FileInfo(location.LocalPath);
                    var header = fileInfo.ToHeader();
                    mediaType = mediaTypeFactory.GetByMagicNumber(header);
                    if (mediaType != null && mediaType != mediaTypeFactory.Default)
                        return new ContentType(mediaType);

                    if (location.ToString().EndsWith(".db"))
                        System.Diagnostics.Debug.WriteLine("+++GetContentType after magic number check");

                    var extension = location.ToFileExtension();
                    if (!string.IsNullOrEmpty(extension))
                    {
                        mediaType = mediaTypeFactory.GetByFileExtension(extension).FirstOrDefault();
                    }

                    if (mediaType != null && mediaType.ToString() != "application/xml-dtd") //MediaType.ApplicationXmlDtd)
                    {
                        try
                        {
                            using (var stream = new FileStream(location.LocalPath, FileMode.Open, FileAccess.Read))
                            {
                                charSet = GetCharacterSet(stream, mediaType, charSet);
                                var ext = GetXmlExtendedType(stream, mediaType, charSet);
                                mediaType = ext.Item1;
                                charSet = ext.Item2;
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("ContentType.GetContentType - Could not open file to read content: " + location.ToString() + Environment.NewLine + ex.Message);
                        }
                    }

                    return new ContentType(mediaType, charSet, boundary);
                }

                var request = HttpWebRequest.Create(location);
                var response = request.GetResponse();

                if (!string.IsNullOrEmpty(response.ContentType))
                {
                    var tokens = response.ContentType.Split(new string[] { "; ", " ", ";" }, StringSplitOptions.RemoveEmptyEntries);

                    var typeCode = tokens != null && tokens.Length > 0 && tokens[0] != null ? tokens[0].Trim() : string.Empty;
                    mediaType = mediaTypeFactory.GetByCode(typeCode);

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
                    var header = response.GetResponseStream().ToHeader();
                    mediaType = mediaTypeFactory.GetByMagicNumber(header);
                    if (mediaType != mediaTypeFactory.Default)
                        return new ContentType(mediaType, charSet, boundary);

                    var extension = location.ToFileExtension();
                    if (!string.IsNullOrEmpty(extension))
                    {
                        mediaType = mediaTypeFactory.GetByFileExtension(extension).FirstOrDefault();
                    }
                }

                return new ContentType(mediaType, charSet, boundary);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ContentType.GetContentType() Failed: " + ex.Message + Environment.NewLine + ex.StackTrace);
                return Default;
            }
        }
    }
}
