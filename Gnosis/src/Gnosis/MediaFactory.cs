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
using Gnosis.Video;

namespace Gnosis
{
    public class MediaFactory
        : IMediaFactory
    {
        public MediaFactory(ICharacterSetFactory characterSetFactory)
        {
            if (characterSetFactory == null)
                throw new ArgumentNullException("characterSetFactory");

            this.characterSetFactory = characterSetFactory;

            InitializeDefaultMappings();
        }

        private readonly ICharacterSetFactory characterSetFactory;
        private readonly IDictionary<string, Func<Uri, IContentType, IMedia>> createFunctions = new Dictionary<string, Func<Uri, IContentType, IMedia>>();

        private void InitializeDefaultMappings()
        {
            MapMediaType(ContentTypeFactory.mediaType_ApplicationAtomXml, (uri, type) => new XmlDocument(uri, type, characterSetFactory));
            MapMediaType(ContentTypeFactory.mediaType_ApplicationDosExe, (uri, type) => new MicrosoftExecutable(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_ApplicationXMsDownload, (uri, type) => new MicrosoftExecutable(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_ApplicationXMsShortcut, (uri, type) => new MicrosoftShortcut(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_ApplicationXExe, (uri, type) => new MicrosoftExecutable(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_ApplicationXWinExe, (uri, type) => new MicrosoftExecutable(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_ApplicationRssXml, (uri, type) => new XmlDocument(uri, type, characterSetFactory));
            MapMediaType(ContentTypeFactory.mediaType_ApplicationXml, (uri, type) => new XmlDocument(uri, type, characterSetFactory));
            MapMediaType(ContentTypeFactory.mediaType_ApplicationXspfXml, (uri, type) => new XmlDocument(uri, type, characterSetFactory));
            MapMediaType(ContentTypeFactory.mediaType_AudioMp3, (uri, type) => new MpegAudio(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_AudioMpeg, (uri, type) => new MpegAudio(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_ImageXBmp, (uri, type) => new BitmapImage(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_ImageXMsBmp, (uri, type) => new BitmapImage(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_ImageGif, (uri, type) => new GifImage(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_ImageJpeg, (uri, type) => new JpegImage(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_ImagePng, (uri, type) => new PngImage(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_TextHtml, (uri, type) => new XhtmlDocument(uri, type, characterSetFactory));
            MapMediaType(ContentTypeFactory.mediaType_TextXml, (uri, type) => new XmlDocument(uri, type, characterSetFactory));
            MapMediaType(ContentTypeFactory.mediaType_VideoAvi, (uri, type) => new AviVideo(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_VideoMsVideo, (uri, type) => new AviVideo(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_VideoXMsVideo, (uri, type) => new AviVideo(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_VideoMpeg, (uri, type) => new MpegVideo(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_VideoMpeg4, (uri, type) => new Mpeg4Video(uri, type));
            MapMediaType(ContentTypeFactory.mediaType_VideoWmv, (uri, type) => new WmvVideo(uri, type));
        }

        public IMedia Create(Uri location, IContentType type)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (type == null)
                throw new ArgumentNullException("type");

            var key = type.Name.ToLower();
            return createFunctions.ContainsKey(key) ?
                createFunctions[key](location, type)
                : null;
        }

        public IEnumerable<string> GetMediaTypes()
        {
            return createFunctions.Keys.ToList();
        }

        public void MapMediaType(string mediaType, Func<Uri, IContentType, IMedia> createFunction)
        {
            if (mediaType == null)
                throw new ArgumentNullException("mediaType");
            if (createFunction == null)
                throw new ArgumentNullException("createFunction");

            var key = mediaType.ToLower();
            createFunctions[key] = createFunction;
        }
    }
}
