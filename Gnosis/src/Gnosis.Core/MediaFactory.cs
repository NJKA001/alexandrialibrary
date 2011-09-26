using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Document.Xml;
using Gnosis.Core.Document.Xml.Xhtml;
using Gnosis.Core.Image;

namespace Gnosis.Core
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

            AddFactoryFunction(MediaType.ImageBmp, (location, type) => new BitmapImage(location));
            AddFactoryFunction(MediaType.ImageGif, (location, type) => new GifImage(location));
            AddFactoryFunction(MediaType.ImageJpeg, (location, type) => new JpegImage(location));
            AddFactoryFunction(MediaType.ImagePng, (location, type) => new PngImage(location));

            AddFactoryFunction(MediaType.TextHtml, (location, contentType) => new XhtmlDocument(location, contentType));
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

            var mediaType = contentType.Type.ToString();
            System.Diagnostics.Debug.WriteLine("mediaType=" + mediaType);

            return factoryFunctions.ContainsKey(mediaType) ?
                factoryFunctions[mediaType](location, contentType.Type)
                : null;
        }

        #endregion
    }
}
