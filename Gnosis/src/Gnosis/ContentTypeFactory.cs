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
        public ContentTypeFactory(ILogger logger, ICharacterSetFactory characterSetFactory)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (characterSetFactory == null)
                throw new ArgumentNullException("characterSetFactory");

            this.logger = logger;
            this.characterSetFactory = characterSetFactory;

            defaultContentType = new ContentType(defaultMediaType);
        }

        private readonly ILogger logger;
        private readonly ICharacterSetFactory characterSetFactory;
        private readonly IContentType defaultContentType;

        private const string defaultMediaType = "application/unknown";
        private const string charSetFieldName = "charset=";
        private const string boundaryFieldName = "boundary=";

        private ICharacterSet GetCharacterSet(Stream stream, string name, ICharacterSet charSet)
        {
            if (name != null && name.Contains("xml"))
            {
                if (name.StartsWith("text/") && charSet == null)
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        var buffer = new char[20];
                        reader.Read(buffer, 0, 20);
                        return characterSetFactory.GetByEncoding(reader.CurrentEncoding);
                    }
                }
            }

            return charSet;
        }

        private Tuple<string, ICharacterSet> GetXmlExtendedType(Stream stream, string name, ICharacterSet charSet)
        {
            var newName = name;
            var newCharSet = charSet;

            //System.Diagnostics.Debug.WriteLine("GetXmlExtendedType. mediaType=" + mediaType);
            if (name.Contains("xml"))
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
                            if (declaration != null && declaration.Encoding != null)
                            {
                                newCharSet = characterSetFactory.GetByName(declaration.Encoding);
                            }
                            else
                            {
                                if (newName == name)
                                {
                                    var element = node as XmlElement;
                                    if (element != null)
                                    {
                                        //System.Diagnostics.Debug.WriteLine("elementName=" + element.Name);
                                        if (element.Name == "rss")
                                        {
                                            newName = "application/rss+xml";
                                        }
                                        else if (element.Name == "feed")
                                        {
                                            newName = "application/atom+xml";
                                        }
                                        else if (element.Name == "playlist")
                                        {
                                            newName = "application/xspf+xml";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    newName = "application/xml";
                }
            }

            return new Tuple<string, ICharacterSet>(newName, newCharSet);
        }

        private readonly IDictionary<byte[], string> byMagicNumber = new Dictionary<byte[], string>();

        private string GetByMagicNumber(byte[] header)
        {
            try
            {
                //TODO: Optimize this algorithm
                byte[] lookup = null;
                foreach (var pair in byMagicNumber)
                {
                    var keyLength = pair.Key.Length;
                    if (keyLength < header.Length)
                    {
                        lookup = new byte[keyLength];
                        Array.Copy(header, lookup, keyLength);
                    }
                    else lookup = header;

                    //System.Diagnostics.Debug.WriteLine("lookup=" + Encoding.UTF8.GetString(lookup) + " key=" + Encoding.UTF8.GetString(pair.Key));

                    //NOTE: In the case of GIF images, even when the byte arrays look identical,
                    //      they don't match unless I compare them as UTF-8 encoded strings.
                    //if (lookup == pair.Key || Encoding.UTF8.GetString(lookup) == Encoding.UTF8.GetString(pair.Key))
                    if (lookup.SequenceEqual(pair.Key))
                    {
                        System.Diagnostics.Debug.WriteLine("Found MediaType by magic number: " + pair.Value);
                        return pair.Value;
                    }
                }

                return "application/unknown";
            }
            catch (Exception ex)
            {
                logger.Error("  MediaTypeFactory.GetByMagicNumber", ex);
                return "application/unknown";
            }
        }

        private readonly IDictionary<string, IList<string>> byFileExtension = new Dictionary<string, IList<string>>();

        private IEnumerable<string> GetByFileExtension(string fileExtension)
        {
            if (fileExtension == null)
                throw new ArgumentNullException("fileExtension");

            return (byFileExtension.ContainsKey(fileExtension)) ?
                byFileExtension[fileExtension]
                : Enumerable.Empty<string>();
        }

        public IContentType Default
        {
            get { return defaultContentType; }
        }

        public IContentType GetByCode(string code)
        {
            if (code == null)
                throw new ArgumentNullException("code");

            var name = "application/default";
            ICharacterSet charSet = null;
            string boundary = null;

            var tokens = code.Split(new string[] { "; ", " ", ";" }, StringSplitOptions.RemoveEmptyEntries);

            var typeCode = tokens != null && tokens.Length > 0 && tokens[0] != null ? tokens[0].Trim() : string.Empty;
            name = typeCode;
            //mediaType = mediaTypeFactory.GetByCode(typeCode);

            if (tokens.Length > 1)
            {
                var token = string.Empty;
                for (var i = 1; i < tokens.Length; i++)
                {
                    token = tokens[i].Trim();
                    var charSetName = token.Contains(charSetFieldName) && token.Length > 8 ? token.Substring(8).Trim() : string.Empty;

                    if (!string.IsNullOrEmpty(charSetName))
                    {
                        charSet = characterSetFactory.GetByName(charSetName);
                    }
                    else if (token.Contains(boundaryFieldName) && token.Length > 9)
                    {
                        boundary = token.Substring(9);
                    }
                }
            }

            return new ContentType(name, charSet, boundary);
        }

        public IContentType GetByLocation(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            try
            {
                if (location == null)
                    return Default;

                var name = "application/default";
                ICharacterSet charSet = null;
                string boundary = null;

                if (location.IsFile)
                {
                    if (System.IO.Directory.Exists(location.LocalPath))
                        return new ContentType("application/vnd.gnosis.fs.dir");

                    if (!System.IO.File.Exists(location.LocalPath))
                        return Default;

                    var fileInfo = new FileInfo(location.LocalPath);
                    var header = fileInfo.ToHeader();
                    name = GetByMagicNumber(header);
                    if (name != null && name != "application/unknown")
                        return new ContentType(name);

                    if (location.ToString().EndsWith(".db"))
                        System.Diagnostics.Debug.WriteLine("+++GetContentType after magic number check");

                    var extension = location.ToFileExtension();
                    if (!string.IsNullOrEmpty(extension))
                    {
                        name = GetByFileExtension(extension).FirstOrDefault();
                    }

                    if (name != null && name != "application/xml-dtd") //MediaType.ApplicationXmlDtd)
                    {
                        try
                        {
                            using (var stream = new FileStream(location.LocalPath, FileMode.Open, FileAccess.Read))
                            {
                                charSet = GetCharacterSet(stream, name, charSet);
                                var ext = GetXmlExtendedType(stream, name, charSet);
                                name = ext.Item1;
                                charSet = ext.Item2;
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("ContentType.GetContentType - Could not open file to read content: " + location.ToString() + Environment.NewLine + ex.Message);
                        }
                    }

                    return new ContentType(name, charSet, boundary);
                }

                var request = HttpWebRequest.Create(location);
                var response = request.GetResponse();

                if (!string.IsNullOrEmpty(response.ContentType))
                {
                    var contentByCode = GetByCode(response.ContentType);
                    name = contentByCode.Name;
                    charSet = contentByCode.CharSet;
                    boundary = contentByCode.Boundary;

                    using (var stream = response.GetResponseStream())
                    {
                        charSet = GetCharacterSet(stream, name, charSet);
                        var ext = GetXmlExtendedType(stream, name, charSet);
                        name = ext.Item1;
                        charSet = ext.Item2;
                    }

                    return new ContentType(name, charSet, boundary);
                }
                else
                {
                    var header = response.GetResponseStream().ToHeader();
                    name = GetByMagicNumber(header);
                    if (name != "application/unknown")
                        return new ContentType(name, charSet, boundary);

                    var extension = location.ToFileExtension();
                    if (!string.IsNullOrEmpty(extension))
                    {
                        name = GetByFileExtension(extension).FirstOrDefault();
                    }
                }

                return new ContentType(name, charSet, boundary);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ContentType.GetContentType() Failed: " + ex.Message + Environment.NewLine + ex.StackTrace);
                return Default;
            }
        }
    }
}
