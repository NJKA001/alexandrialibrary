using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Document.Xml;
using Gnosis.Core.Document.Xml.Xhtml;
using Gnosis.Core.Image;

namespace Gnosis.Core
{
    public class RepresentationFactory
        : IRepresentationFactory
    {
        public RepresentationFactory()
        {
            AddFactoryFunction(MediaType.ApplicationAtomXml, (location, contentType) => new XmlDocument(location, contentType));
            AddFactoryFunction(MediaType.ApplicationRssXml, (location, contentType) => new XmlDocument(location, contentType));
            AddFactoryFunction(MediaType.ApplicationXhtmlXml, (location, contentType) => new XhtmlDocument(location, contentType));
            AddFactoryFunction(MediaType.ApplicationXml, (location, contentType) => new XmlDocument(location, contentType));

            AddFactoryFunction(MediaType.ImageBmp, (location, contentType) => new BitmapImage(location, contentType));
            AddFactoryFunction(MediaType.ImageGif, (location, contentType) => new GifImage(location, contentType));
            AddFactoryFunction(MediaType.ImageJpeg, (location, contentType) => new JpegImage(location, contentType));
            AddFactoryFunction(MediaType.ImagePng, (location, contentType) => new PngImage(location, contentType));

            AddFactoryFunction(MediaType.TextHtml, (location, contentType) => new XhtmlDocument(location, contentType));
        }

        private readonly IDictionary<string, Func<Uri, IContentType, IRepresentation>> factoryFunctions = new Dictionary<string, Func<Uri, IContentType, IRepresentation>>();

        public void AddFactoryFunction(IMediaType mediaType, Func<Uri, IContentType, IRepresentation> function)
        {
            if (mediaType == null)
                throw new ArgumentNullException("mediaType");
            if (function == null)
                throw new ArgumentNullException("function");

            factoryFunctions[mediaType.ToString()] = function;
        }

        #region IRepresentationFactory Members

        public IRepresentation Create(Uri location)
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
                System.Diagnostics.Debug.WriteLine("contentTye=" + contentType.ToString());
                return null;
            }

            var mediaType = contentType.Type.ToString();
            System.Diagnostics.Debug.WriteLine("mediaType=" + mediaType);

            return factoryFunctions.ContainsKey(mediaType) ?
                factoryFunctions[mediaType](location, contentType)
                : null;
        }

        #endregion
    }
}
