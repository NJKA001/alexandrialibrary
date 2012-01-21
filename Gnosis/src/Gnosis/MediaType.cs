using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace Gnosis
{
    public class MediaType
        : IMediaType
    {
        private MediaType(MediaSupertype supertype, string subtype)
            : this(supertype, subtype, new List<string>(), new List<string>(), new List<byte[]>())
        {
        }

        private MediaType(MediaSupertype supertype, string subtype, IEnumerable<string> fileExtensions)
            : this(supertype, subtype, fileExtensions, new List<string>(), new List<byte[]>())
        {
        }

        private MediaType(MediaSupertype supertype, string subtype, IEnumerable<string> fileExtensions, IEnumerable<string> legacyTypes)
            : this(supertype, subtype, fileExtensions, legacyTypes, new List<byte[]>())
        {
        }

        private MediaType(MediaSupertype supertype, string subtype, IEnumerable<string> fileExtensions, IEnumerable<string> legacyTypes, IEnumerable<byte[]> magicNumbers)
        {
            if (supertype == null)
                throw new ArgumentNullException("supertype");
            if (subtype == null)
                throw new ArgumentNullException("subtype");
            if (fileExtensions == null)
                throw new ArgumentNullException("fileExtensions");
            if (legacyTypes == null)
                throw new ArgumentNullException("legacyTypes");
            if (magicNumbers == null)
                throw new ArgumentNullException("magicNumbers");

            this.supertype = supertype;
            this.subtype = subtype;
            this.fileExtensions = fileExtensions;
            this.legacyTypes = legacyTypes;
            this.magicNumbers = magicNumbers;
        }

        private readonly MediaSupertype supertype;
        private readonly string subtype;
        private readonly IEnumerable<string> fileExtensions;
        private readonly IEnumerable<string> legacyTypes;
        private readonly IEnumerable<byte[]> magicNumbers;

        #region IMediaType Members

        public MediaSupertype Supertype
        {
            get { return supertype; }
        }

        public string Subtype
        {
            get { return subtype; }
        }

        public IEnumerable<string> FileExtensions
        {
            get { return fileExtensions; }
        }

        public IEnumerable<string> LegacyTypes
        {
            get { return legacyTypes; }
        }

        public IEnumerable<byte[]> MagicNumbers
        {
            get { return magicNumbers; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}/{1}", supertype.ToString().ToLower(), subtype.ToLower());
        }

        static MediaType()
        {
            InitializeMediaTypes();

            foreach (var mediaType in all)
            {
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
        }

        private static readonly IList<IMediaType> all = new List<IMediaType>();
        private static readonly IDictionary<string, IMediaType> byCode = new Dictionary<string, IMediaType>();
        private static readonly IDictionary<byte, IList<IMediaType>> bySupertype = new Dictionary<byte, IList<IMediaType>>();
        private static readonly IDictionary<string, IList<IMediaType>> byLegacyType = new Dictionary<string, IList<IMediaType>>();
        private static readonly IDictionary<string, IList<IMediaType>> byFileExtension = new Dictionary<string, IList<IMediaType>>();
        private static readonly IDictionary<byte[], IMediaType> byMagicNumber = new Dictionary<byte[], IMediaType>();

        #region InitializeMediaTypes

        private static void InitializeMediaTypes()
        {
            all.Add(ApplicationAtomXml);
            all.Add(ApplicationPdf);
            all.Add(ApplicationRssXml);
            all.Add(ApplicationXhtmlXml);
            all.Add(ApplicationXspfXml);
            all.Add(ApplicationXml);
            all.Add(ApplicationXmlDtd);
            all.Add(ApplicationUnknown);
            all.Add(ApplicationGnosisAlbum);
            all.Add(ApplicationGnosisAlbumThumbnail);
            all.Add(ApplicationGnosisArtist);
            all.Add(ApplicationGnosisArtistThumbnail);
            all.Add(ApplicationGnosisClip);
            all.Add(ApplicationGnosisDoc);
            all.Add(ApplicationGnosisFeed);
            all.Add(ApplicationGnosisFeedItem);
            all.Add(ApplicationGnosisFilesystemDirectory);
            all.Add(ApplicationGnosisFilesystemFile);
            all.Add(ApplicationGnosisLink);
            all.Add(ApplicationGnosisPic);
            all.Add(ApplicationGnosisPlaylist);
            all.Add(ApplicationGnosisPlaylistItem);
            all.Add(ApplicationGnosisProgram);
            all.Add(ApplicationGnosisTag);
            all.Add(ApplicationGnosisTrack);
            all.Add(ApplicationGnosisUser);
            all.Add(ApplicationGnosisUserCatalog);
            all.Add(ApplicationGnosisUserFolder);
            all.Add(ApplicationMicrosoftExecutable);
            all.Add(ApplicationMicrosoftShortcut);
            all.Add(AudioMpeg);
            all.Add(ImageBmp);
            all.Add(ImageGif);
            all.Add(ImageJpeg);
            all.Add(ImagePng);
            all.Add(TextCss);
            all.Add(TextHtml);
            all.Add(TextPlain);
            all.Add(TextXsl);
            all.Add(VideoAvi);
            all.Add(VideoMpeg);
            all.Add(VideoMpeg4);
            all.Add(VideoWmv);
        }

        #endregion

        #region Public Static Methods

        public static IMediaType GetMediaType(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            try
            {
                var contentType = ContentType.GetContentType(location);
                return contentType.Type;
            }
            catch
            {
                return MediaType.ApplicationUnknown;
            }
        }

        public static IMediaType Parse(string value)
        {
            if (byCode.ContainsKey(value))
                return byCode[value];
            
            return byLegacyType.ContainsKey(value) ? byLegacyType[value].First() : ApplicationUnknown;
        }

        public static IEnumerable<IMediaType> GetMediaTypesBySupertype(MediaSupertype supertype)
        {
            return (bySupertype.ContainsKey((byte)supertype)) ?
                bySupertype[(byte)supertype]
                : new List<IMediaType>();
        }

        public static IEnumerable<IMediaType> GetMediaTypesByFileExtension(string fileExtension)
        {
            return (byFileExtension.ContainsKey(fileExtension)) ?
                byFileExtension[fileExtension]
                : new List<IMediaType>();
        }

        public static IMediaType GetMediaTypeByMagicNumber(byte[] header)
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

                return MediaType.ApplicationUnknown;
            }
            catch (Exception)
            {
                return MediaType.ApplicationUnknown;
            }
        }

        public static IEnumerable<IMediaType> GetMediaTypes()
        {
            return all;
        }

        #endregion

        #region Media Types

        public static readonly IMediaType ApplicationAtomXml = new MediaType(MediaSupertype.Application, "atom+xml", new List<string> { ".atom", ".xml" });
        public static readonly IMediaType ApplicationPdf = new MediaType(MediaSupertype.Application, "pdf", new List<string> { ".pdf" }, new List<string> { "application/x-pdf", "application/x-bzpdf", "application/x-gxpdf" }, new List<byte[]> { new byte[] { 0x25, 0x50, 0x44, 0x46 } });
        public static readonly IMediaType ApplicationRssXml = new MediaType(MediaSupertype.Application, "rss+xml", new List<string> { ".rss", ".xml" });
        public static readonly IMediaType ApplicationXhtmlXml = new MediaType(MediaSupertype.Application, "xhtml+xml", new List<string> { ".xhtml" }, new List<string> { "text/html" });
        public static readonly IMediaType ApplicationXspfXml = new MediaType(MediaSupertype.Application, "xspf+xml", new List<string> { ".xspf" });
        public static readonly IMediaType ApplicationXml = new MediaType(MediaSupertype.Application, "xml", new List<string> { ".xml" }, new List<string> { "text/xml" });
        public static readonly IMediaType ApplicationXmlDtd = new MediaType(MediaSupertype.Application, "xml-dtd", new List<string> { ".dtd", ".ent" });
        public static readonly IMediaType ApplicationUnknown = new MediaType(MediaSupertype.Application, "unknown");
        
        public static readonly IMediaType ApplicationGnosisAlbum = new MediaType(MediaSupertype.Application, "vnd.gnosis.album");
        public static readonly IMediaType ApplicationGnosisAlbumThumbnail = new MediaType(MediaSupertype.Application, "vnd.gnosis.album.thumbnail");
        public static readonly IMediaType ApplicationGnosisArtist = new MediaType(MediaSupertype.Application, "vnd.gnosis.artist");
        public static readonly IMediaType ApplicationGnosisArtistThumbnail = new MediaType(MediaSupertype.Application, "vnd.gnosis.artist.thumbnail");
        public static readonly IMediaType ApplicationGnosisClip = new MediaType(MediaSupertype.Application, "vnd.gosis.clip");
        public static readonly IMediaType ApplicationGnosisDoc = new MediaType(MediaSupertype.Application, "vnd.gosis.doc");
        public static readonly IMediaType ApplicationGnosisFeed = new MediaType(MediaSupertype.Application, "vnd.gnosis.feed");
        public static readonly IMediaType ApplicationGnosisFeedItem = new MediaType(MediaSupertype.Application, "vnd.gnosis.feed.item");
        public static readonly IMediaType ApplicationGnosisFilesystemDirectory = new MediaType(MediaSupertype.Application, "vnd.gnosis.fs.dir");
        public static readonly IMediaType ApplicationGnosisFilesystemFile = new MediaType(MediaSupertype.Application, "vnd.gnosis.fs.file");
        public static readonly IMediaType ApplicationGnosisLink = new MediaType(MediaSupertype.Application, "vnd.gnosis.link");
        public static readonly IMediaType ApplicationGnosisPic = new MediaType(MediaSupertype.Application, "vnd.gnosis.pic");
        public static readonly IMediaType ApplicationGnosisPlaylist = new MediaType(MediaSupertype.Application, "vnd.gnosis.playlist");
        public static readonly IMediaType ApplicationGnosisPlaylistItem = new MediaType(MediaSupertype.Application, "vnd.gnosis.playlist.item");
        public static readonly IMediaType ApplicationGnosisProgram = new MediaType(MediaSupertype.Application, "vnd.gnosis.program");
        public static readonly IMediaType ApplicationGnosisTag = new MediaType(MediaSupertype.Application, "vnd.gnosis.tag");
        public static readonly IMediaType ApplicationGnosisTrack = new MediaType(MediaSupertype.Application, "vnd.gnosis.track");
        public static readonly IMediaType ApplicationGnosisUser = new MediaType(MediaSupertype.Application, "vnd.gnosis.user");
        public static readonly IMediaType ApplicationGnosisUserCatalog = new MediaType(MediaSupertype.Application, "vnd.gnosis.user.catalog");
        public static readonly IMediaType ApplicationGnosisUserFolder = new MediaType(MediaSupertype.Application, "vnd.gnosis.user.folder");
        
        public static readonly IMediaType ApplicationMicrosoftExecutable = new MediaType(MediaSupertype.Application, "x-winexe", new List<string> { ".exe" }, new List<string> { "x-exe", "x-msdownload", "dos-exe" }, new List<byte[]> { new byte[] { 0x4D, 0x5A } });
        public static readonly IMediaType ApplicationMicrosoftShortcut = new MediaType(MediaSupertype.Application, "x-ms-shortcut", new List<string> { ".lnk" }, new List<string>(), new List<byte[]> { new byte[] { 0x4C, 0x00, 0x00, 0x00, 0x01, 0x14, 0x02 } });

        public static readonly IMediaType AudioMpeg = new MediaType(MediaSupertype.Audio, "mpeg", new List<string> { ".mp3", ".mp2", ".mp1" }, new List<string> { "audio/mp3" }, new List<byte[]> { new byte[] { 0x49, 0x44, 0x33 }});

        public static readonly IMediaType ImageBmp = new MediaType(MediaSupertype.Image, "x-bmp", new List<string> { ".bmp", ".dib" }, new List<string> { "image/x-ms_bmp", "image/bmp" }, new List<byte[]> { new byte[] { 66, 77 } });
        public static readonly IMediaType ImageGif = new MediaType(MediaSupertype.Image, "gif", new List<string> { ".gif" }, new List<string>(), new List<byte[]> { new byte[] { 71, 73, 70, 56, 55, 97 }, new byte[] { 71, 73, 70, 56, 57, 97 } });        
        public static readonly IMediaType ImageJpeg = new MediaType(MediaSupertype.Image, "jpeg", new List<string> { ".jpg", ".jpeg", ".jpe", ".jif", ".jfif", ".jfi" }, new List<string>(), new List<byte[]> { new byte[] { 255, 216, 255, 224 } });
        public static readonly IMediaType ImagePng = new MediaType(MediaSupertype.Image, "png", new List<string> { ".png" }, new List<string> { "image/x-png" }, new List<byte[]> { new byte[] { 137, 80, 78, 71, 13, 10, 26, 10} });

        public static readonly IMediaType TextCss = new MediaType(MediaSupertype.Text, "css", new List<string> { ".css" });
        public static readonly IMediaType TextHtml = new MediaType(MediaSupertype.Text, "html", new List<string> { ".html", ".htm" }, new List<string> { "text/html" });
        public static readonly IMediaType TextPlain = new MediaType(MediaSupertype.Text, "plain", new List<string> { ".txt", ".text", ".ini" });
        public static readonly IMediaType TextXsl = new MediaType(MediaSupertype.Text, "xsl", new List<string> { ".xsl" });

        public static readonly IMediaType VideoAvi = new MediaType(MediaSupertype.Video, "avi", new List<string> { ".avi" }, new List<string> { "video/x-msvideo", "video/msvideo" }, new List<byte[]> { new byte[] { 0x52, 0x49, 0x46, 0x46 } });
        public static readonly IMediaType VideoMpeg = new MediaType(MediaSupertype.Video, "mpeg", new List<string> { ".mpeg", ".mpe", ".mpg", ".mpga" }, new List<string>(), new List<byte[]> { new byte[] { 0x00, 0x00, 0x01 } } );
        public static readonly IMediaType VideoMpeg4 = new MediaType(MediaSupertype.Video, "mp4", new List<string> { ".mp4" });
        public static readonly IMediaType VideoWmv = new MediaType(MediaSupertype.Video, "x-ms-wmv", new List<string> { ".wmv" }, new List<string>(), new List<byte[]> { new byte[] { 0x30, 0x26, 0xB2, 0x75 } }); //, 0x8E, 0x66, 0xCF, 0x11, 0xA6, 0xD9, 0x00, 0xAA, 0x00, 0x62, 0xCE, 0x6C } });

        #endregion
    }
}
