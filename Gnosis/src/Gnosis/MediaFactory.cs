using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application;
using Gnosis.Audio;
using Gnosis.Document;
using Gnosis.Document.Xml;
using Gnosis.Document.Xml.Xhtml;
using Gnosis.Image;

namespace Gnosis
{
    public class MediaFactory
        : IMediaFactory
    {
        public MediaFactory()
        {
            AddFactoryFunction(MediaType.ApplicationAtomXml, (location, type) => new XmlDocument(location, type));
            AddFactoryFunction(MediaType.ApplicationRssXml, (location, type) => new XmlDocument(location, type));
            AddFactoryFunction(MediaType.ApplicationXhtmlXml, (location, type) => new XhtmlDocument(location, type));
            AddFactoryFunction(MediaType.ApplicationXml, (location, type) => new XmlDocument(location, type));
            AddFactoryFunction(MediaType.ApplicationXspfXml, (location, type) => new XmlDocument(location, type));

            AddFactoryFunction(MediaType.AudioMpeg, (location, type) => new MpegAudio(location));

            AddFactoryFunction(MediaType.ImageBmp, (location, type) => new BitmapImage(location));
            AddFactoryFunction(MediaType.ImageGif, (location, type) => new GifImage(location));
            AddFactoryFunction(MediaType.ImageJpeg, (location, type) => new JpegImage(location));
            AddFactoryFunction(MediaType.ImagePng, (location, type) => new PngImage(location));

            AddFactoryFunction(MediaType.TextHtml, (location, type) => new XhtmlDocument(location, type));
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
                return new FilesystemDirectory(location);

            var contentType = ContentType.GetContentType(location);

            if (contentType == null)
            {
                System.Diagnostics.Debug.WriteLine("contentType is null");
                return null;
            }

            if (contentType == ContentType.Empty || contentType.Type == null)
            {
                System.Diagnostics.Debug.WriteLine("contentType=" + contentType.ToString());
                return null;
            }

            return Create(location, contentType.Type);
        }

        public IMedia Create(Uri location, IMediaType type)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (type == null)
                throw new ArgumentNullException("type");

            var mediaType = type.ToString();

            if (!factoryFunctions.ContainsKey(mediaType))
                System.Diagnostics.Debug.WriteLine("MediaFactory does not contain a factory function for MediaType: " + mediaType);

            return factoryFunctions.ContainsKey(mediaType) ?
                factoryFunctions[mediaType](location, type)
                : null;
        }

        #endregion
    }
}
