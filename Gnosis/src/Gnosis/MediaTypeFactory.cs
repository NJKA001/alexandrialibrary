using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application;
using Gnosis.Application.Pdf;
using Gnosis.Application.Vendor;
using Gnosis.Application.Xml;
using Gnosis.Application.Xml.Atom;
using Gnosis.Application.Xml.Rss;
using Gnosis.Application.Xml.Xhtml;
using Gnosis.Application.Xml.Xspf;
using Gnosis.Audio;
using Gnosis.Image;
using Gnosis.Metadata;
using Gnosis.Text;
using Gnosis.Video;

namespace Gnosis
{
    public class MediaTypeFactory
        : IMediaTypeFactory
    {
        public MediaTypeFactory(ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            this.logger = logger;
            this.defaultMediaType = ApplicationUnknown;

            ApplicationAtomXml = new MediaType(MediaSupertype.Application, "atom+xml", false, (uri, type) => new XmlDocument(uri, type, this),  new List<string> { ".atom", ".xml" });
            ApplicationRssXml = new MediaType(MediaSupertype.Application, "rss+xml", false, (uri, type) => new XmlDocument(uri, type, this), new List<string> { ".rss", ".xml" });
            ApplicationXspfXml = new MediaType(MediaSupertype.Application, "xspf+xml", false, (uri, type) => new XmlDocument(uri, type, this), new List<string> { ".xspf" });
            ApplicationXml = new MediaType(MediaSupertype.Application, "xml", false, (uri, type) => new XmlDocument(uri, type, this), new List<string> { ".xml" }, new List<string> { "text/xml" });
            TextXsl = new MediaType(MediaSupertype.Text, "xsl", false, (uri, type) => new XmlDocument(uri, type, this), new List<string> { ".xsl" });

            InitializeMediaTypes();
        }

        private ILogger logger;
        private IMediaType defaultMediaType;

        private readonly IList<IMediaType> all = new List<IMediaType>();
        private readonly IDictionary<string, IMediaType> byCode = new Dictionary<string, IMediaType>();
        private readonly IDictionary<byte, IList<IMediaType>> bySupertype = new Dictionary<byte, IList<IMediaType>>();
        private readonly IDictionary<string, IList<IMediaType>> byLegacyType = new Dictionary<string, IList<IMediaType>>();
        private readonly IDictionary<string, IList<IMediaType>> byFileExtension = new Dictionary<string, IList<IMediaType>>();
        private readonly IDictionary<byte[], IMediaType> byMagicNumber = new Dictionary<byte[], IMediaType>();

        #region Media Types

        private readonly IMediaType ApplicationAtomXml;
        private readonly IMediaType ApplicationRssXml;
        private readonly IMediaType ApplicationXspfXml;
        private readonly IMediaType ApplicationXml;
        private readonly IMediaType TextXsl;

        private readonly IMediaType ApplicationPdf = new MediaType(MediaSupertype.Application, "pdf", false, (uri, type) => new PdfDocument(uri, type), new List<string> { ".pdf" }, new List<string> { "application/x-pdf", "application/x-bzpdf", "application/x-gxpdf" }, new List<byte[]> { new byte[] { 0x25, 0x50, 0x44, 0x46 } });
        private readonly IMediaType ApplicationXhtmlXml = new MediaType(MediaSupertype.Application, "xhtml+xml", false, (uri, type) => new XhtmlDocument(uri, type), new List<string> { ".xhtml" }, new List<string> { "text/html" });
        private readonly IMediaType ApplicationXmlDtd = new MediaType(MediaSupertype.Application, "xml-dtd", false, (uri, type) => new XmlDtdDocument(uri, type), new List<string> { ".dtd", ".ent" });
        private readonly IMediaType ApplicationUnknown = new MediaType(MediaSupertype.Application, "unknown", true, (uri, type) => new UnknownApplication(uri, type));

        private readonly IMediaType ApplicationGnosisAlbum = new MediaType(MediaSupertype.Application, "vnd.gnosis.album", false);
        //private readonly IMediaType ApplicationGnosisAlbumThumbnail = new MediaType(MediaSupertype.Application, "vnd.gnosis.album.thumbnail", false);
        private readonly IMediaType ApplicationGnosisArtist = new MediaType(MediaSupertype.Application, "vnd.gnosis.artist", false);
        //private readonly IMediaType ApplicationGnosisArtistThumbnail = new MediaType(MediaSupertype.Application, "vnd.gnosis.artist.thumbnail", false);
        private readonly IMediaType ApplicationGnosisClip = new MediaType(MediaSupertype.Application, "vnd.gosis.clip", false);
        private readonly IMediaType ApplicationGnosisDoc = new MediaType(MediaSupertype.Application, "vnd.gosis.doc", false);
        private readonly IMediaType ApplicationGnosisFeed = new MediaType(MediaSupertype.Application, "vnd.gnosis.feed", false);
        private readonly IMediaType ApplicationGnosisFeedItem = new MediaType(MediaSupertype.Application, "vnd.gnosis.feed.item", false);
        private readonly IMediaType ApplicationGnosisFilesystemDirectory = new MediaType(MediaSupertype.Application, "vnd.gnosis.fs.dir", false, (uri, type) => new GnosisFilesystemDirectory(uri, type));
        //private readonly IMediaType ApplicationGnosisFilesystemFile = new MediaType(MediaSupertype.Application, "vnd.gnosis.fs.file", false);
        private readonly IMediaType ApplicationGnosisLink = new MediaType(MediaSupertype.Application, "vnd.gnosis.link", false);
        private readonly IMediaType ApplicationGnosisPic = new MediaType(MediaSupertype.Application, "vnd.gnosis.pic", false);
        private readonly IMediaType ApplicationGnosisPlaylist = new MediaType(MediaSupertype.Application, "vnd.gnosis.playlist", false);
        private readonly IMediaType ApplicationGnosisPlaylistItem = new MediaType(MediaSupertype.Application, "vnd.gnosis.playlist.item", false);
        private readonly IMediaType ApplicationGnosisProgram = new MediaType(MediaSupertype.Application, "vnd.gnosis.program", false);
        private readonly IMediaType ApplicationGnosisTag = new MediaType(MediaSupertype.Application, "vnd.gnosis.tag", false);
        private readonly IMediaType ApplicationGnosisTrack = new MediaType(MediaSupertype.Application, "vnd.gnosis.track", false);
        private readonly IMediaType ApplicationGnosisUser = new MediaType(MediaSupertype.Application, "vnd.gnosis.user", false);
        private readonly IMediaType ApplicationGnosisUserCatalog = new MediaType(MediaSupertype.Application, "vnd.gnosis.user.catalog", false);
        private readonly IMediaType ApplicationGnosisUserFolder = new MediaType(MediaSupertype.Application, "vnd.gnosis.user.folder", false);

        private readonly IMediaType ApplicationMicrosoftExecutable = new MediaType(MediaSupertype.Application, "x-winexe", false, (uri, type) => new MicrosoftExecutable(uri, type), new List<string> { ".exe" }, new List<string> { "x-exe", "x-msdownload", "dos-exe" }, new List<byte[]> { new byte[] { 0x4D, 0x5A } });
        private readonly IMediaType ApplicationMicrosoftShortcut = new MediaType(MediaSupertype.Application, "x-ms-shortcut", false, (uri, type) => new MicrosoftShortcut(uri, type), new List<string> { ".lnk" }, new List<string>(), new List<byte[]> { new byte[] { 0x4C, 0x00, 0x00, 0x00, 0x01, 0x14, 0x02 } });

        private readonly IMediaType AudioMpeg = new MediaType(MediaSupertype.Audio, "mpeg", false, (uri, type) => new MpegAudio(uri, type), new List<string> { ".mp3", ".mp2", ".mp1" }, new List<string> { "audio/mp3" }, new List<byte[]> { new byte[] { 0x49, 0x44, 0x33 } });

        private readonly IMediaType ImageBmp = new MediaType(MediaSupertype.Image, "x-bmp", false, (uri, type) => new BitmapImage(uri, type), new List<string> { ".bmp", ".dib" }, new List<string> { "image/x-ms_bmp", "image/bmp" }, new List<byte[]> { new byte[] { 66, 77 } });
        private readonly IMediaType ImageGif = new MediaType(MediaSupertype.Image, "gif", false, (uri, type) => new GifImage(uri, type), new List<string> { ".gif" }, new List<string>(), new List<byte[]> { new byte[] { 71, 73, 70, 56, 55, 97 }, new byte[] { 71, 73, 70, 56, 57, 97 } });
        private readonly IMediaType ImageJpeg = new MediaType(MediaSupertype.Image, "jpeg", false, (uri, type) => new JpegImage(uri, type), new List<string> { ".jpg", ".jpeg", ".jpe", ".jif", ".jfif", ".jfi" }, new List<string>(), new List<byte[]> { new byte[] { 255, 216, 255, 224 } });
        private readonly IMediaType ImagePng = new MediaType(MediaSupertype.Image, "png", false, (uri, type) => new PngImage(uri, type), new List<string> { ".png" }, new List<string> { "image/x-png" }, new List<byte[]> { new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 } });

        private readonly IMediaType TextCss = new MediaType(MediaSupertype.Text, "css", false, (uri, type) => new PlainText(uri, type), new List<string> { ".css" });
        private readonly IMediaType TextHtml = new MediaType(MediaSupertype.Text, "html", false, (uri, type) => new XhtmlDocument(uri, type), new List<string> { ".html", ".htm" }, new List<string> { "text/html" });
        private readonly IMediaType TextPlain = new MediaType(MediaSupertype.Text, "plain", false, (uri, type) => new PlainText(uri, type), new List<string> { ".txt", ".text", ".ini" });

        private readonly IMediaType VideoAvi = new MediaType(MediaSupertype.Video, "avi", false, (uri, type) => new AviVideo(uri, type), new List<string> { ".avi" }, new List<string> { "video/x-msvideo", "video/msvideo" }, new List<byte[]> { new byte[] { 0x52, 0x49, 0x46, 0x46 } });
        private readonly IMediaType VideoMpeg = new MediaType(MediaSupertype.Video, "mpeg", false, (uri, type) => new MpegVideo(uri, type), new List<string> { ".mpeg", ".mpe", ".mpg", ".mpga" }, new List<string>(), new List<byte[]> { new byte[] { 0x00, 0x00, 0x01 } });
        private readonly IMediaType VideoMpeg4 = new MediaType(MediaSupertype.Video, "mp4", false, (uri, type) => new Mpeg4Video(uri, type), new List<string> { ".mp4" });
        private readonly IMediaType VideoWmv = new MediaType(MediaSupertype.Video, "x-ms-wmv", false, (uri, type) => new WmvVideo(uri, type), new List<string> { ".wmv" }, new List<string>(), new List<byte[]> { new byte[] { 0x30, 0x26, 0xB2, 0x75 } }); //, 0x8E, 0x66, 0xCF, 0x11, 0xA6, 0xD9, 0x00, 0xAA, 0x00, 0x62, 0xCE, 0x6C } });

        #endregion

        #region InitializeMediaTypes

        private void InitializeMediaTypes()
        {
            AddMediaType(ApplicationAtomXml);
            AddMediaType(ApplicationPdf);
            AddMediaType(ApplicationRssXml);
            AddMediaType(ApplicationXhtmlXml);
            AddMediaType(ApplicationXspfXml);
            AddMediaType(ApplicationXml);
            AddMediaType(ApplicationXmlDtd);
            AddMediaType(ApplicationUnknown);
            AddMediaType(ApplicationGnosisAlbum);
            //AddMediaType(ApplicationGnosisAlbumThumbnail);
            AddMediaType(ApplicationGnosisArtist);
            //AddMediaType(ApplicationGnosisArtistThumbnail);
            AddMediaType(ApplicationGnosisClip);
            AddMediaType(ApplicationGnosisDoc);
            AddMediaType(ApplicationGnosisFeed);
            AddMediaType(ApplicationGnosisFeedItem);
            AddMediaType(ApplicationGnosisFilesystemDirectory);
            //AddMediaType(ApplicationGnosisFilesystemFile);
            AddMediaType(ApplicationGnosisLink);
            AddMediaType(ApplicationGnosisPic);
            AddMediaType(ApplicationGnosisPlaylist);
            AddMediaType(ApplicationGnosisPlaylistItem);
            AddMediaType(ApplicationGnosisProgram);
            AddMediaType(ApplicationGnosisTag);
            AddMediaType(ApplicationGnosisTrack);
            AddMediaType(ApplicationGnosisUser);
            AddMediaType(ApplicationGnosisUserCatalog);
            AddMediaType(ApplicationGnosisUserFolder);
            AddMediaType(ApplicationMicrosoftExecutable);
            AddMediaType(ApplicationMicrosoftShortcut);
            AddMediaType(AudioMpeg);
            AddMediaType(ImageBmp);
            AddMediaType(ImageGif);
            AddMediaType(ImageJpeg);
            AddMediaType(ImagePng);
            AddMediaType(TextCss);
            AddMediaType(TextHtml);
            AddMediaType(TextPlain);
            AddMediaType(TextXsl);
            AddMediaType(VideoAvi);
            AddMediaType(VideoMpeg);
            AddMediaType(VideoMpeg4);
            AddMediaType(VideoWmv);
        }

        #endregion

        #region IMediaTypeFactory Members

        public IMediaType Default
        {
            get { return defaultMediaType; }
        }

        public IMediaType GetByCode(string code)
        {
            if (code == null)
                throw new ArgumentNullException("code");

            var normalized = code.ToLower();
            if (byCode.ContainsKey(normalized))
                return byCode[normalized];

            return byLegacyType.ContainsKey(normalized) ? byLegacyType[normalized].First() : Default;
        }

        public IMediaType GetByLocation(Uri location, IContentTypeFactory contentTypeFactory)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (contentTypeFactory == null)
                throw new ArgumentNullException("contentTypeFactory");

            try
            {
                var contentType = contentTypeFactory.GetByLocation(location);
                return contentType != null ? contentType.Type : Default;
            }
            catch (Exception ex)
            {
                logger.Error("  MediaTypeFactory.GetByLocation", ex);
                return Default;
            }
        }

        public IMediaType GetByMagicNumber(byte[] header)
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
                        System.Diagnostics.Debug.WriteLine("Found MediaType by magic number: " + pair.Value.ToString());
                        return pair.Value;
                    }
                }

                return Default;
            }
            catch (Exception ex)
            {
                logger.Error("  MediaTypeFactory.GetByMagicNumber", ex);
                return Default;
            }
        }

        public IEnumerable<IMediaType> GetAll()
        {
            return all;
        }

        public IEnumerable<IMediaType> GetByFileExtension(string fileExtension)
        {
            if (fileExtension == null)
                throw new ArgumentNullException("fileExtension");

            return (byFileExtension.ContainsKey(fileExtension)) ?
                byFileExtension[fileExtension]
                : new List<IMediaType>();
        }

        public IEnumerable<IMediaType> GetBySupertype(MediaSupertype supertype)
        {
            return (bySupertype.ContainsKey((byte)supertype)) ?
                bySupertype[(byte)supertype]
                : Enumerable.Empty<IMediaType>();
        }

        public void AddMediaType(IMediaType mediaType)
        {
            if (mediaType == null)
                throw new ArgumentNullException("mediaType");

            all.Add(mediaType);

            byCode.Add(mediaType.ToString(), mediaType);

            if (!bySupertype.ContainsKey((byte)mediaType.Supertype))
                bySupertype.Add((byte)mediaType.Supertype, new List<IMediaType> { mediaType });
            else
                bySupertype[(byte)mediaType.Supertype].Add(mediaType);

            foreach (var legacyType in mediaType.LegacyTypes)
            {
                if (!byLegacyType.ContainsKey(legacyType))
                    byLegacyType.Add(legacyType, new List<IMediaType> { mediaType });
                else
                    byLegacyType[legacyType].Add(mediaType);
            }

            foreach (var fileExtension in mediaType.FileExtensions)
            {
                if (!byFileExtension.ContainsKey(fileExtension))
                    byFileExtension.Add(fileExtension, new List<IMediaType> { mediaType });
                else
                    byFileExtension[fileExtension].Add(mediaType);
            }

            foreach (var magicNumber in mediaType.MagicNumbers)
                byMagicNumber.Add(magicNumber, mediaType);
        }

        #endregion
    }
}
