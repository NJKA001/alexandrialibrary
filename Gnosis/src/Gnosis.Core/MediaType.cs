﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace Gnosis.Core
{
    public class MediaType
        : IMediaType
    {
        private MediaType(string type, string subType)
            : this(type, subType, new List<string>(), new List<string>(), new List<byte[]>())
        {
        }

        private MediaType(string type, string subType, IEnumerable<string> fileExtensions)
            : this(type, subType, fileExtensions, new List<string>(), new List<byte[]>())
        {
        }

        private MediaType(string type, string subType, IEnumerable<string> fileExtensions, IEnumerable<string> legacyTypes)
            : this(type, subType, fileExtensions, legacyTypes, new List<byte[]>())
        {
        }

        private MediaType(string type, string subType, IEnumerable<string> fileExtensions, IEnumerable<string> legacyTypes, IEnumerable<byte[]> magicNumbers)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (subType == null)
                throw new ArgumentNullException("subType");
            if (fileExtensions == null)
                throw new ArgumentNullException("fileExtensions");
            if (legacyTypes == null)
                throw new ArgumentNullException("legacyTypes");
            if (magicNumbers == null)
                throw new ArgumentNullException("magicNumbers");

            this.type = type;
            this.subType = subType;
            this.fileExtensions = fileExtensions;
            this.legacyTypes = legacyTypes;
            this.magicNumbers = magicNumbers;
        }

        private readonly string type;
        private readonly string subType;
        private readonly IEnumerable<string> fileExtensions;
        private readonly IEnumerable<string> legacyTypes;
        private readonly IEnumerable<byte[]> magicNumbers;

        #region IMediaType Members

        public string Type
        {
            get { return type; }
        }

        public string SubType
        {
            get { return subType; }
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
            return string.Format("{0}/{1}", type, subType);
        }

        static MediaType()
        {
            InitializeMediaTypes();

            foreach (var mediaType in mediaTypes)
            {
                byCode.Add(mediaType.ToString(), mediaType);

                if (!byType.ContainsKey(mediaType.Type))
                    byType.Add(mediaType.Type, new List<IMediaType> { mediaType });
                else
                    byType[mediaType.Type].Add(mediaType);

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

        private static readonly IList<IMediaType> mediaTypes = new List<IMediaType>();
        private static readonly IDictionary<string, IMediaType> byCode = new Dictionary<string, IMediaType>();
        private static readonly IDictionary<string, IList<IMediaType>> byType = new Dictionary<string, IList<IMediaType>>();
        private static readonly IDictionary<string, IList<IMediaType>> byLegacyType = new Dictionary<string, IList<IMediaType>>();
        private static readonly IDictionary<string, IList<IMediaType>> byFileExtension = new Dictionary<string, IList<IMediaType>>();
        private static readonly IDictionary<byte[], IMediaType> byMagicNumber = new Dictionary<byte[], IMediaType>();

        #region InitializeMediaTypes

        private static void InitializeMediaTypes()
        {
            mediaTypes.Add(ApplicationAtomXml);
            mediaTypes.Add(ApplicationRssXml);
            mediaTypes.Add(ApplicationXhtmlXml);
            mediaTypes.Add(ApplicationXspfXml);
            mediaTypes.Add(ApplicationXml);
            mediaTypes.Add(ApplicationUnknown);
            mediaTypes.Add(AudioMpeg);
            mediaTypes.Add(ImageBmp);
            mediaTypes.Add(ImageGif);
            mediaTypes.Add(ImageJpeg);
            mediaTypes.Add(ImagePng);
            mediaTypes.Add(TextCss);
            mediaTypes.Add(TextHtml);
            mediaTypes.Add(TextXsl);
        }

        #endregion

        #region Public Static Methods

        public static IMediaType Parse(string value)
        {
            if (byCode.ContainsKey(value))
                return byCode[value];
            
            return byLegacyType.ContainsKey(value) ? byLegacyType[value].First() : ApplicationUnknown;
        }

        public static IEnumerable<IMediaType> GetMediaTypesByType(string type)
        {
            return (byType.ContainsKey(type)) ?
                byType[type]
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
            //System.Diagnostics.Debug.WriteLine("magicNumber=" + header);
            //foreach (var b in header)
                //System.Diagnostics.Debug.WriteLine(b);

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
                    return pair.Value;
                }
            }

            return MediaType.ApplicationUnknown;
        }

        public static IEnumerable<IMediaType> GetMediaTypes()
        {
            return mediaTypes;
        }

        #endregion

        #region Media Types

        public static readonly IMediaType ApplicationAtomXml = new MediaType(TypeApplication, "atom+xml", new List<string> { ".atom", ".xml" });
        public static readonly IMediaType ApplicationRssXml = new MediaType(TypeApplication, "rss+xml", new List<string> { ".rss", ".xml" });
        public static readonly IMediaType ApplicationXhtmlXml = new MediaType(TypeApplication, "xhtml+xml", new List<string> { ".xhtml", "" }, new List<string> { "text/html" });
        public static readonly IMediaType ApplicationXspfXml = new MediaType(TypeApplication, "xspf+xml", new List<string> { ".xspf" });
        public static readonly IMediaType ApplicationXml = new MediaType(TypeApplication, "xml", new List<string> { ".xml" }, new List<string> { "text/xml" });
        public static readonly IMediaType ApplicationUnknown = new MediaType(TypeApplication, "unknown");

        public static readonly IMediaType AudioMpeg = new MediaType(TypeAudio, "mpeg", new List<string> { ".mp3", ".mp2", ".mp1" });

        public static readonly IMediaType ImageBmp = new MediaType(TypeImage, "x-bmp", new List<string> { ".bmp", ".dib" }, new List<string> { "image/x-ms_bmp", "image/bmp" }, new List<byte[]> { new byte[] { 66, 77 } });
        public static readonly IMediaType ImageGif = new MediaType(TypeImage, "gif", new List<string> { ".gif" }, new List<string>(), new List<byte[]> { new byte[] { 71, 73, 70, 56, 55, 97 }, new byte[] { 71, 73, 70, 56, 57, 97 } });        
        public static readonly IMediaType ImageJpeg = new MediaType(TypeImage, "jpeg", new List<string> { ".jpg", ".jpeg", ".jpe", ".jif", ".jfif", ".jfi" }, new List<string>(), new List<byte[]> { new byte[] { 255, 216, 255, 224 } });
        public static readonly IMediaType ImagePng = new MediaType(TypeImage, "png", new List<string> { ".png" }, new List<string> { "image/x-png" }, new List<byte[]> { new byte[] { 137, 80, 78, 71, 13, 10, 26, 10} });

        public static readonly IMediaType TextCss = new MediaType(TypeText, "css", new List<string> { ".css" });
        public static readonly IMediaType TextHtml = new MediaType(TypeText, "html", new List<string> { ".html", ".htm" }, new List<string> { "text/html" });
        public static readonly IMediaType TextXsl = new MediaType(TypeText, "xsl", new List<string> { ".xsl" });

        public const string TypeApplication = "application";
        public const string TypeAudio = "audio";
        public const string TypeImage = "image";
        public const string TypeMessage = "message";
        public const string TypeModel = "model";
        public const string TypeMultipart = "multipart";
        public const string TypeText = "text";
        public const string TypeVideo = "video";

        #endregion
    }
}
