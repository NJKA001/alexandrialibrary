using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Gnosis.Logging;

namespace Gnosis
{
    public class MediaFactory
        : IMediaFactory
    {
        public MediaFactory(ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            this.logger = logger;

            defaultType = new MediaType(defaultMediaType);
            //defaultMedia = new UnknownApplication(Guid.Empty.ToUrn(), defaultType);

            InitializeFileExtensions();
            InitializeMagicNumbers();
            InitializeDefaultMappings();
        }

        private readonly ILogger logger;
        private readonly IMediaType defaultType;
        //private readonly IMedia defaultMedia;

        private readonly IDictionary<string, IList<string>> byFileExtension = new Dictionary<string, IList<string>>();
        private readonly IDictionary<string, string> byLegacyMediaType = new Dictionary<string, string>();
        private readonly IDictionary<byte[], string> byMagicNumber = new Dictionary<byte[], string>();
        //private readonly IDictionary<string, Func<Uri, IMediaType, IMedia>> createFunctions = new Dictionary<string, Func<Uri, IMediaType, IMedia>>();

        private const string defaultMediaType = mediaType_ApplicationUnknown;
        private const string charSetFieldName = "charset=";
        private const string boundaryFieldName = "boundary=";

        #region FileExtension Constants

        private const string fileExtension_Atom = ".atom";
        private const string fileExtension_Avi = ".avi";
        private const string fileExtension_Bmp = ".bmp";
        private const string fileExtension_Css = ".css";
        private const string fileExtension_Dib = ".dib";
        private const string fileExtension_Dtd = ".dtd";
        private const string fileExtension_Ent = ".ent";
        private const string fileExtension_Exe = ".exe";
        private const string fileExtension_Gif = ".gif";
        private const string fileExtension_Htm = ".htm";
        private const string fileExtension_Html = ".html";
        private const string fileExtension_Ini = ".ini";
        private const string fileExtension_Jpg = ".jpg";
        private const string fileExtension_Jpeg = ".jpeg";
        private const string fileExtension_Jpe = ".jpe";
        private const string fileExtension_Jif = ".jif";
        private const string fileExtension_Jfif = ".jfif";
        private const string fileExtension_Jfi = ".jfi";
        private const string fileExtension_Lnk = ".lnk";
        private const string fileExtension_M4a = ".m4a";
        private const string fileExtension_M4v = ".m4v";
        private const string fileExtension_Mp1 = ".mp1";
        private const string fileExtension_Mp2 = ".mp2";
        private const string fileExtension_Mp3 = ".mp3";
        private const string fileExtension_Mp4 = ".mp4";
        private const string fileExtension_Mpeg = ".mpeg";
        private const string fileExtension_Mpe = ".mpe";
        private const string fileExtension_Mpg = ".mpg";
        private const string fileExtension_Mpg4 = ".mpg4";
        private const string fileExtension_Mpga = ".mpga";
        private const string fileExtension_Pdf = ".pdf";
        private const string fileExtension_Png = ".png";
        private const string fileExtension_Rdf = ".rdf";
        private const string fileExtension_Rss = ".rss";
        private const string fileExtension_Text = ".text";
        private const string fileExtension_Txt = ".txt";
        private const string fileExtension_Wmv = ".wmv";
        private const string fileExtension_Xhtml = ".xhtml";
        private const string fileExtension_Xml = ".xml";
        private const string fileExtension_Xsl = ".xsl";
        private const string fileExtension_Xslt = ".xslt";
        private const string fileExtension_Xspf = ".xspf";

        #endregion

        #region MagicNumber Constants

        private readonly byte[] magicNumber_Avi = new byte[] { 0x52, 0x49, 0x46, 0x46 };
        private readonly byte[] magicNumber_Bmp = new byte[] { 66, 77 };
        private readonly byte[] magicNumber_Exe = new byte[] { 0x4D, 0x5A };
        private readonly byte[] magicNumber_Gif87 = new byte[] { 71, 73, 70, 56, 55, 97 };
        private readonly byte[] magicNumber_Gif89 = new byte[] { 71, 73, 70, 56, 57, 97 };
        private readonly byte[] magicNumber_Jpeg = new byte[] { 255, 216, 255, 224 };
        private readonly byte[] magicNumber_Lnk = new byte[] { 0x4C, 0x00, 0x00, 0x00, 0x01, 0x14, 0x02 };
        private readonly byte[] magicNumber_Mpeg = new byte[] { 0x00, 0x00, 0x01 };
        private readonly byte[] magicNumber_Mp3 = new byte[] { 0x49, 0x44, 0x33 };
        private readonly byte[] magicNumber_Pdf = new byte[] { 0x25, 0x50, 0x44, 0x46 };
        private readonly byte[] magicNumber_Png = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 };
        private readonly byte[] magicNumber_Wmv = new byte[] { 0x30, 0x26, 0xB2, 0x75 };

        #endregion

        #region MediaType Constants

        public const string mediaType_ApplicationAtomXml = "application/atom+xml";

        //public const string mediaType_ApplicationGnosisAlbum = "application/vnd.gnosis.album";
        //public const string mediaType_ApplicationGnosisArtist = "application/vnd.gnosis.artist";
        //public const string mediaType_ApplicationGnosisClip = "application/vnd.gnosis.clip";
        //public const string mediaType_ApplicationGnosisDoc = "application/vnd.gnosis.doc";
        //public const string mediaType_ApplicationGnosisFeed = "application/vnd.gnosis.feed";
        //public const string mediaType_ApplicationGnosisFeedItem = "application/vnd.gnosis.feed-item";
        public const string mediaType_ApplicationGnosisFilesystemDirectory = "application/vnd.gnosis.fs-dir";
        //public const string mediaType_ApplicationGnosisLink = "application/vnd.gnosis.link";
        //public const string mediaType_ApplicationGnosisPic = "application/vnd.gnosis.pic";
        //public const string mediaType_ApplicationGnosisPlaylist = "application/vnd.gnosis.playlist";
        //public const string mediaType_ApplicationGnosisPlaylistItem = "application/vnd.gnosis.playlist-item";
        //public const string mediaType_ApplicationGnosisProgram = "application/vnd.gnosis.program";
        //public const string mediaType_ApplicationGnosisTag = "application/vnd.gnosis.tag";
        //public const string mediaType_ApplicationGnosisTrack = "application/vnd.gnosis.track";
        //public const string mediaType_ApplicationGnosisUser = "application/vnd.gnosis.user";
        //public const string mediaType_ApplicationGnosisUserCatalog = "application/vnd.gnosis.user-catalog";
        //public const string mediaType_ApplicationGnosisUserFolder = "application/vnd.gnosis.user-folder";

        public const string mediaType_ApplicationDosExe = "application/dos-exe";
        public const string mediaType_ApplicationXExe = "application/x-exe";
        public const string mediaType_ApplicationXMsDownload = "application/x-msdownload";
        public const string mediaType_ApplicationXMsShortcut = "application/x-ms-shortcut";
        public const string mediaType_ApplicationXWinExe = "application/x-winexe";

        public const string mediaType_ApplicationPdf = "application/pdf";
        public const string mediaType_ApplicationXPdf = "application/x-pdf";
        public const string mediaType_ApplicationXbzPdf = "application/x-bzpdf";
        public const string mediaType_ApplicationXgxPdf = "application/x-gxpdf";

        public const string mediaType_ApplicationRdfXml = "application/rdf+xml";
        public const string mediaType_ApplicationRssXml = "application/rss+xml";
        public const string mediaType_ApplicationUnknown = "application/unknown";
        public const string mediaType_ApplicationXhtmlXml = "application/xhtml+xml";
        public const string mediaType_ApplicationXsl = "application/xsl";
        public const string mediaType_ApplicationXspfXml = "application/xspf+xml";
        public const string mediaType_ApplicationXml = "application/xml";
        public const string mediaType_ApplicationXmlDtd = "application/xml-dtd";

        public const string mediaType_AudioMp3 = "audio/mp3";
        public const string mediaType_AudioMp4 = "audio/mp4";
        public const string mediaType_AudioAac = "audio/aac";
        public const string mediaType_AudioMpeg = "audio/mpeg";

        public const string mediaType_ImageXBmp = "image/x-bmp";
        public const string mediaType_ImageXMsBmp = "image/x-ms-bmp";

        public const string mediaType_ImageGif = "image/gif";
        public const string mediaType_ImageJpeg = "image/jpeg";

        public const string mediaType_ImagePng = "image/png";
        public const string mediaType_ImageXPng = "image/x-png";

        public const string mediaType_TextCss = "text/css";
        public const string mediaType_TextHtml = "text/html";
        public const string mediaType_TextPlain = "text/plain";
        public const string mediaType_TextXml = "text/xml";

        public const string mediaType_ApplicationXsltXml = "application/xslt+xml";
        public const string mediaType_TextXsl = "text/xsl";

        public const string mediaType_VideoAvi = "video/avi";
        public const string mediaType_VideoXMsVideo = "video/x-msvideo";
        public const string mediaType_VideoMsVideo = "video/msvideo";

        public const string mediaType_VideoMpeg = "video/mpeg";
        public const string mediaType_VideoMpeg4 = "video/mp4";
        public const string mediaType_VideoWmv = "video/x-ms-wmv";

        #endregion

        #region Initialization Methods

        private void InitializeFileExtensions()
        {
            MapFileExtensions(mediaType_ApplicationAtomXml, new List<string> { fileExtension_Atom });

            //MapFileExtensions(mediaType_ApplicationDosExe, new List<string> { fileExtension_Exe });
            //MapFileExtensions(mediaType_ApplicationXMsDownload, new List<string> { fileExtension_Exe });
            //MapFileExtensions(mediaType_ApplicationXMsShortcut, new List<string> { fileExtension_Lnk });
            //MapFileExtensions(mediaType_ApplicationXExe, new List<string> { fileExtension_Exe });
            MapFileExtensions(mediaType_ApplicationXWinExe, new List<string> { fileExtension_Exe });

            //MapFileExtensions(mediaType_ApplicationXbzPdf, new List<string> { fileExtension_Pdf });
            //MapFileExtensions(mediaType_ApplicationXgxPdf, new List<string> { fileExtension_Pdf });
            //MapFileExtensions(mediaType_ApplicationXPdf, new List<string> { fileExtension_Pdf });
            MapFileExtensions(mediaType_ApplicationPdf, new List<string> { fileExtension_Pdf });

            MapFileExtensions(mediaType_ApplicationRdfXml, new List<string> { fileExtension_Rdf });

            MapFileExtensions(mediaType_ApplicationRssXml, new List<string> { fileExtension_Rss });

            MapFileExtensions(mediaType_TextHtml, new List<string> { fileExtension_Html, fileExtension_Htm });
            MapFileExtensions(mediaType_ApplicationXhtmlXml, new List<string> { fileExtension_Xhtml });

            MapFileExtensions(mediaType_ApplicationXspfXml, new List<string> { fileExtension_Xspf });

            //MapFileExtensions(mediaType_TextXml, new List<string> { fileExtension_Xml });
            MapFileExtensions(mediaType_ApplicationXml, new List<string> { fileExtension_Xml });

            MapFileExtensions(mediaType_ApplicationXmlDtd, new List<string> { fileExtension_Dtd, fileExtension_Ent });

            //MapFileExtensions(mediaType_TextXsl, new List<string> { fileExtension_Xsl, fileExtension_Xslt });
            MapFileExtensions(mediaType_ApplicationXsltXml, new List<string> { fileExtension_Xsl, fileExtension_Xslt });

            //MapFileExtensions(mediaType_AudioMp3, new List<string> { fileExtension_Mp3, fileExtension_Mp2, fileExtension_Mp1 });
            MapFileExtensions(mediaType_AudioMpeg, new List<string> { fileExtension_Mp3, fileExtension_Mp2, fileExtension_Mp1 });
            MapFileExtensions(mediaType_AudioMp4, new List<string> { fileExtension_M4a });

            //MapFileExtensions(mediaType_ImageXMsBmp, new List<string> { fileExtension_Bmp, fileExtension_Dib });
            MapFileExtensions(mediaType_ImageXBmp, new List<string> { fileExtension_Bmp, fileExtension_Dib });

            MapFileExtensions(mediaType_ImageGif, new List<string> { fileExtension_Gif });
            MapFileExtensions(mediaType_ImageJpeg, new List<string> { fileExtension_Jpg, fileExtension_Jpeg, fileExtension_Jpe, fileExtension_Jif, fileExtension_Jfif, fileExtension_Jfi });

            //MapFileExtensions(mediaType_ImageXPng, new List<string> { fileExtension_Png });
            MapFileExtensions(mediaType_ImagePng, new List<string> { fileExtension_Png });

            MapFileExtensions(mediaType_TextCss, new List<string> { fileExtension_Css });
            MapFileExtensions(mediaType_TextPlain, new List<string> { fileExtension_Txt, fileExtension_Text, fileExtension_Ini });

            //MapFileExtensions(mediaType_VideoMsVideo, new List<string> { fileExtension_Avi });
            //MapFileExtensions(mediaType_VideoXMsVideo, new List<string> { fileExtension_Avi });
            MapFileExtensions(mediaType_VideoAvi, new List<string> { fileExtension_Avi });

            MapFileExtensions(mediaType_VideoMpeg, new List<string> { fileExtension_Mpeg, fileExtension_Mpe, fileExtension_Mpg, fileExtension_Mpga });
            MapFileExtensions(mediaType_VideoMpeg4, new List<string> { fileExtension_M4v, fileExtension_Mp4, fileExtension_Mpg4 });

            MapFileExtensions(mediaType_VideoWmv, new List<string> { fileExtension_Wmv });
        }

        private void InitializeMagicNumbers()
        {
            //MapMagicNumbers(mediaType_ApplicationDosExe, magicNumber_Exe);
            //MapMagicNumbers(mediaType_ApplicationXMsDownload, magicNumber_Exe);
            //MapMagicNumbers(mediaType_ApplicationXMsShortcut, magicNumber_Lnk);
            //MapMagicNumbers(mediaType_ApplicationXExe, magicNumber_Exe);
            MapMagicNumbers(mediaType_ApplicationXWinExe, magicNumber_Exe);

            //MapMagicNumbers(mediaType_ApplicationXbzPdf, magicNumber_Pdf);
            //MapMagicNumbers(mediaType_ApplicationXgxPdf, magicNumber_Pdf);
            //MapMagicNumbers(mediaType_ApplicationXPdf, magicNumber_Pdf);
            MapMagicNumbers(mediaType_ApplicationPdf, magicNumber_Pdf);

            //MapMagicNumbers(mediaType_AudioMp3, magicNumber_Mp3);
            MapMagicNumbers(mediaType_AudioMpeg, magicNumber_Mp3);

            MapMagicNumbers(mediaType_ImageXBmp, magicNumber_Bmp);
            //MapMagicNumbers(mediaType_ImageXMsBmp, magicNumber_Bmp);

            MapMagicNumbers(mediaType_ImageGif, magicNumber_Gif87);
            MapMagicNumbers(mediaType_ImageGif, magicNumber_Gif89);

            MapMagicNumbers(mediaType_ImageJpeg, magicNumber_Jpeg);

            //MapMagicNumbers(mediaType_ImageXPng, magicNumber_Png);
            MapMagicNumbers(mediaType_ImagePng, magicNumber_Png);

            //MapMagicNumbers(mediaType_VideoMsVideo, magicNumber_Avi);
            //MapMagicNumbers(mediaType_VideoXMsVideo, magicNumber_Avi);
            MapMagicNumbers(mediaType_VideoAvi, magicNumber_Avi);

            MapMagicNumbers(mediaType_VideoMpeg, magicNumber_Mpeg);

            MapMagicNumbers(mediaType_VideoWmv, magicNumber_Wmv);
        }

        private void InitializeDefaultMappings()
        {
            /*
            MapCreateFunction(MediaFactory.mediaType_ApplicationAtomXml, (uri, type) => new XmlDocument(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationGnosisFilesystemDirectory, (uri, type) => new GnosisFilesystemDirectory(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationDosExe, (uri, type) => new MicrosoftExecutable(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationXMsDownload, (uri, type) => new MicrosoftExecutable(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationXMsShortcut, (uri, type) => new MicrosoftShortcut(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationXExe, (uri, type) => new MicrosoftExecutable(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationXWinExe, (uri, type) => new MicrosoftExecutable(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationPdf, (uri, type) => new PdfDocument(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationXbzPdf, (uri, type) => new PdfDocument(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationXgxPdf, (uri, type) => new PdfDocument(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationXPdf, (uri, type) => new PdfDocument(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationRdfXml, (uri, type) => new XmlDocument(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationRssXml, (uri, type) => new XmlDocument(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationXml, (uri, type) => new XmlDocument(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationXmlDtd, (uri, type) => new PlainText(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationXspfXml, (uri, type) => new XmlDocument(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ApplicationUnknown, (uri, type) => new UnknownApplication(uri, type));
            MapCreateFunction(MediaFactory.mediaType_AudioMp3, (uri, type) => new MpegAudio(uri, type));
            MapCreateFunction(MediaFactory.mediaType_AudioMpeg, (uri, type) => new MpegAudio(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ImageXBmp, (uri, type) => new BitmapImage(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ImageXMsBmp, (uri, type) => new BitmapImage(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ImageGif, (uri, type) => new GifImage(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ImageJpeg, (uri, type) => new JpegImage(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ImagePng, (uri, type) => new PngImage(uri, type));
            MapCreateFunction(MediaFactory.mediaType_ImageXPng, (uri, type) => new PngImage(uri, type));
            MapCreateFunction(MediaFactory.mediaType_TextHtml, (uri, type) => new XhtmlDocument(uri, type));
            MapCreateFunction(MediaFactory.mediaType_TextPlain, (uri, type) => new PlainText(uri, type));
            MapCreateFunction(MediaFactory.mediaType_TextXml, (uri, type) => new XmlDocument(uri, type));
            MapCreateFunction(MediaFactory.mediaType_VideoAvi, (uri, type) => new AviVideo(uri, type));
            MapCreateFunction(MediaFactory.mediaType_VideoMsVideo, (uri, type) => new AviVideo(uri, type));
            MapCreateFunction(MediaFactory.mediaType_VideoXMsVideo, (uri, type) => new AviVideo(uri, type));
            MapCreateFunction(MediaFactory.mediaType_VideoMpeg, (uri, type) => new MpegVideo(uri, type));
            MapCreateFunction(MediaFactory.mediaType_VideoMpeg4, (uri, type) => new Mpeg4Video(uri, type));
            MapCreateFunction(MediaFactory.mediaType_VideoWmv, (uri, type) => new WmvVideo(uri, type));
            */
        }

        #endregion

        private string GetCharacterSet(Stream stream, string name, string charSet)
        {
            if (name != null && name.Contains("xml"))
            {
                if (name.StartsWith("text/") && charSet == null)
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        var buffer = new char[20];
                        reader.Read(buffer, 0, 20);
                        return reader.CurrentEncoding != null && reader.CurrentEncoding.EncodingName != null ? reader.CurrentEncoding.EncodingName : charSet;
                    }
                }
            }

            return charSet;
        }

        /// <summary>
        /// Return the media type and encoding
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="name"></param>
        /// <param name="charSet"></param>
        /// <returns>A tuple of the media type followed by the encoding</returns>
        private Tuple<string, string> GetXmlExtendedType(Stream stream, string name, string charSet)
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
                        var xml = new System.Xml.XmlDocument();
                        xml.LoadXml(content);

                        foreach (var node in xml.ChildNodes)
                        {
                            var declaration = node as System.Xml.XmlDeclaration;
                            if (declaration != null && declaration.Encoding != null)
                            {
                                newCharSet = declaration.Encoding;
                            }
                            else
                            {
                                if (newName == name)
                                {
                                    var element = node as System.Xml.XmlElement;
                                    if (element != null && element.Name != null)
                                    {
                                        System.Diagnostics.Debug.WriteLine("elementName=" + element.LocalName.ToLower());
                                        if (element.Name.ToLower() == "rss")
                                        {
                                            newName = mediaType_ApplicationRssXml;
                                        }
                                        else if (element.Name.ToLower() == "feed")
                                        {
                                            newName = mediaType_ApplicationAtomXml;
                                        }
                                        else if (element.Name.ToLower() == "playlist")
                                        {
                                            newName = mediaType_ApplicationXspfXml;
                                        }
                                        else if (element.LocalName.ToLower() == "rdf")
                                        {
                                            newName = mediaType_ApplicationRdfXml;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    newName = mediaType_ApplicationXml;
                }
            }

            return new Tuple<string, string>(newName, newCharSet);
        }

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

                return defaultMediaType;
            }
            catch (Exception ex)
            {
                logger.Error("  MediaTypeFactory.GetByMagicNumber", ex);
                return defaultMediaType;
            }
        }

        private IEnumerable<string> GetByFileExtension(string fileExtension)
        {
            if (fileExtension == null)
                throw new ArgumentNullException("fileExtension");

            var key = fileExtension.ToLower();
            return (byFileExtension.ContainsKey(key)) ?
                byFileExtension[key]
                : Enumerable.Empty<string>();
        }

        private IMediaType DefaultType
        {
            get { return defaultType; }
        }

        private IMediaType GetTypeByCode(string code)
        {
            if (code == null)
                throw new ArgumentNullException("code");

            var name = defaultMediaType;
            string charSet = null;
            string boundary = null;

            var tokens = code.Split(new string[] { "; ", " ", ";" }, StringSplitOptions.RemoveEmptyEntries);

            var typeCode = tokens != null && tokens.Length > 0 && tokens[0] != null ? tokens[0].Trim() : string.Empty;
            name = typeCode;

            if (tokens.Length > 1)
            {
                var token = string.Empty;
                for (var i = 1; i < tokens.Length; i++)
                {
                    token = tokens[i].Trim();
                    var charSetName = token.Contains(charSetFieldName) && token.Length > 8 ? token.Substring(8).Trim() : string.Empty;

                    if (!string.IsNullOrEmpty(charSetName))
                    {
                        charSet = charSetName;
                    }
                    else if (token.Contains(boundaryFieldName) && token.Length > 9)
                    {
                        boundary = token.Substring(9);
                    }
                }
            }

            return new MediaType(name, charSet, boundary);
        }

        private IMediaType GetTypeByLocation(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            try
            {
                if (location == null)
                    return DefaultType;

                var name = defaultMediaType;
                string charSet = null;
                string boundary = null;

                if (location.IsFile)
                {
                    if (System.IO.Directory.Exists(location.LocalPath))
                        return new MediaType(mediaType_ApplicationGnosisFilesystemDirectory);

                    if (!System.IO.File.Exists(location.LocalPath))
                        return DefaultType;

                    var fileInfo = new FileInfo(location.LocalPath);
                    var header = fileInfo.ToHeader();
                    name = GetByMagicNumber(header);
                    if (name != null && name != defaultMediaType)
                        return new MediaType(name);

                    if (location.ToString().EndsWith(".db"))
                        System.Diagnostics.Debug.WriteLine("+++GetContentType after magic number check");

                    var extension = location.ToFileExtension();
                    if (!string.IsNullOrEmpty(extension))
                    {
                        name = GetByFileExtension(extension).FirstOrDefault();
                    }

                    if (name != null && name != mediaType_ApplicationXmlDtd)
                    {
                        try
                        {
                            using (var stream = new FileStream(location.LocalPath, FileMode.Open, FileAccess.Read))
                            {
                                charSet = GetCharacterSet(stream, name, charSet);
                                var ext = GetXmlExtendedType(stream, name, charSet);

                                if (ext.Item1 != defaultMediaType)
                                {
                                    name = ext.Item1;
                                }

                                charSet = ext.Item2;
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error("ContentType.GetContentType - Could not open file to read content: " + location.ToString(), ex);
                        }
                    }
                    else
                    {
                        if (name == null)
                            name = defaultMediaType;
                    }

                    return new MediaType(name, charSet, boundary);
                }

                var request = HttpWebRequest.Create(location);
                WebResponse response = null;

                try
                {
                    response = request.GetResponse();
                }
                catch (Exception)
                {
                    logger.Warn("  mediaFactory.GetByLocation: Web request failed for URL=" + location);
                    response = null;
                }

                if (response != null && !string.IsNullOrEmpty(response.ContentType))
                {
                    var contentByCode = GetTypeByCode(response.ContentType);
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

                    return new MediaType(name, charSet, boundary);
                }
                else
                {
                    if (response != null)
                    {
                        var header = response.GetResponseStream().ToHeader();
                        name = GetByMagicNumber(header);
                        if (name != defaultMediaType)
                            return new MediaType(name, charSet, boundary);
                    }

                    var extension = location.ToFileExtension();
                    if (!string.IsNullOrEmpty(extension))
                    {
                        name = GetByFileExtension(extension).FirstOrDefault();
                    }
                }

                return new MediaType(name, charSet, boundary);
            }
            catch (Exception ex)
            {
                logger.Error("  MediaTypeFactory.GetTypeByLocation", ex);
                return DefaultType;
            }
        }

        public IMediaType Default
        {
            get { return defaultType; }
        }

        //public IMediaType Create(Uri location)
        //{
        //    if (location == null)
        //        throw new ArgumentNullException("location");

        //    var type = GetTypeByLocation(location);
        //    if (type == null)
        //        throw new InvalidOperationException("Could not determine type for URL: " + location);

        //    var key = type.Name.ToLower();

        //    if (!createFunctions.ContainsKey(key))
        //        throw new InvalidOperationException("Cannot create media for type: " + key);

        //    return createFunctions[key](location, type);
        //}

        public IMedia GetMedia(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            var type = GetTypeByLocation(location);

            return new Media(location.ToString(), type);
        }

        public IMediaType GetMediaType(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            return GetTypeByCode(name);
        }

        //public void MapCreateFunction(string mediaType, Func<Uri, IMediaType, IMedia> createFunction)
        //{
        //    if (mediaType == null)
        //        throw new ArgumentNullException("mediaType");
        //    if (createFunction == null)
        //        throw new ArgumentNullException("createFunction");

        //    var key = mediaType.ToLower();
        //    createFunctions[key] = createFunction;
        //}

        public void MapFileExtensions(string mediaType, IEnumerable<string> fileExtensions)
        {
            if (mediaType == null)
                throw new ArgumentNullException("mediaType");
            if (fileExtensions == null)
                throw new ArgumentNullException("fileExtensions");

            foreach (var fileExtension in fileExtensions)
            {
                if (!byFileExtension.ContainsKey(fileExtension))
                {
                    byFileExtension[fileExtension] = new List<string> { mediaType };
                }
                else
                    byFileExtension[fileExtension].Add(mediaType);
            }
        }

        public void MapLegacyMediaTypes(string mediaType, IEnumerable<string> legacyMediaTypes)
        {
            if (mediaType == null)
                throw new ArgumentNullException("mediaType");
            if (legacyMediaTypes == null)
                throw new ArgumentNullException("legacyMediaTypes");
        }

        public void MapMagicNumbers(string mediaType, byte[] magicNumbers)
        {
            if (mediaType == null)
                throw new ArgumentNullException("mediaType");
            if (magicNumbers == null)
                throw new ArgumentNullException("magicNumbers");

            byMagicNumber[magicNumbers] = mediaType;
        }
    }
}
