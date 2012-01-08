using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application;
using Gnosis.Application.Vendor;
using Gnosis.Application.Pdf;
using Gnosis.Application.Xml;
using Gnosis.Application.Xml.Xhtml;
using Gnosis.Audio;
using Gnosis.Image;
using Gnosis.Text;
using Gnosis.Video;

namespace Gnosis
{
    public class MediaFactory
        : IMediaFactory
    {
        public MediaFactory()
        {
            AddFactoryFunction(MediaType.ApplicationAtomXml, (location, type) => new XmlDocument(location, type));
            AddFactoryFunction(MediaType.ApplicationPdf, (location, type) => new PdfDocument(location));
            AddFactoryFunction(MediaType.ApplicationRssXml, (location, type) => new XmlDocument(location, type));
            AddFactoryFunction(MediaType.ApplicationXhtmlXml, (location, type) => new XhtmlDocument(location, type));
            AddFactoryFunction(MediaType.ApplicationXml, (location, type) => new XmlDocument(location, type));
            AddFactoryFunction(MediaType.ApplicationXmlDtd, (location, type) => new XmlDtdDocument(location));
            AddFactoryFunction(MediaType.ApplicationXspfXml, (location, type) => new XmlDocument(location, type));
            AddFactoryFunction(MediaType.ApplicationMicrosoftExecutable, (location, type) => new MicrosoftExecutable(location));
            AddFactoryFunction(MediaType.ApplicationMicrosoftShortcut, (location, type) => new MicrosoftShortcut(location));

            AddFactoryFunction(MediaType.AudioMpeg, (location, type) => new MpegAudio(location));

            AddFactoryFunction(MediaType.ImageBmp, (location, type) => new BitmapImage(location));
            AddFactoryFunction(MediaType.ImageGif, (location, type) => new GifImage(location));
            AddFactoryFunction(MediaType.ImageJpeg, (location, type) => new JpegImage(location));
            AddFactoryFunction(MediaType.ImagePng, (location, type) => new PngImage(location));

            AddFactoryFunction(MediaType.TextHtml, (location, type) => new XhtmlDocument(location, type));
            AddFactoryFunction(MediaType.TextPlain, (location, type) => new PlainText(location));

            AddFactoryFunction(MediaType.VideoAvi, (location, type) => new AviVideo(location));
            AddFactoryFunction(MediaType.VideoMpeg, (location, type) => new MpegVideo(location));
            AddFactoryFunction(MediaType.VideoMpeg4, (location, type) => new Mpeg4Video(location));
            AddFactoryFunction(MediaType.VideoWmv, (location, type) => new WmvVideo(location));
        }

        private readonly IDictionary<string, Func<Uri, IMediaType, IMedia>> factoryFunctions = new Dictionary<string, Func<Uri, IMediaType, IMedia>>();

        public void AddFactoryFunction(IMediaType mediaType, Func<Uri, IMediaType, IMedia> function)
        {
            if (mediaType == null)
                throw new ArgumentNullException("mediaType");
            if (function == null)
                throw new ArgumentNullException("function");

            factoryFunctions[mediaType.ToString()] = function;
        }

        #region IMediaFactory Members

        public IMedia Create(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            if (location.IsFile && System.IO.Directory.Exists(location.LocalPath))
                return new GnosisFilesystemDirectory(location);

            var contentType = ContentType.GetContentType(location);

            if (contentType == null)
            {
                System.Diagnostics.Debug.WriteLine("contentType is null");
                return new UnknownApplication(location);
            }

            if (contentType == ContentType.Empty || contentType.Type == null)
            {
                System.Diagnostics.Debug.WriteLine("contentType=" + contentType.ToString());
                return new UnknownApplication(location);
            }

            return Create(location, contentType.Type);
        }

        public IMedia Create(Uri location, IMediaType type)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (type == null)
                throw new ArgumentNullException("type");

            if (type == MediaType.ApplicationGnosisFilesystemDirectory)
                return new GnosisFilesystemDirectory(location);

            var mediaType = type.ToString();

            if (!factoryFunctions.ContainsKey(mediaType))
                System.Diagnostics.Debug.WriteLine("MediaFactory does not contain a factory function for MediaType: " + mediaType);

            return factoryFunctions.ContainsKey(mediaType) ?
                factoryFunctions[mediaType](location, type)
                : new UnknownApplication(location);
        }

        #endregion
    }
}
