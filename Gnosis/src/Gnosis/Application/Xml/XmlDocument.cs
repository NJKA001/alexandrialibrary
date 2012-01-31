using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application.Xml.Xhtml;
using Gnosis.Links;

namespace Gnosis.Application.Xml
{
    public class XmlDocument
        : IXmlDocument
    {
        public XmlDocument(Uri location, IContentType type, IMediaTypeFactory mediaTypeFactory, ICharacterSetFactory characterSetFactory)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (type == null)
                throw new ArgumentNullException("type");
            if (mediaTypeFactory == null)
                throw new ArgumentNullException("mediaTypeFactory");
            if (characterSetFactory == null)
                throw new ArgumentNullException("characterSetFactory");

            this.location = location;
            this.type = type;
            this.mediaTypeFactory = mediaTypeFactory;
            this.characterSetFactory = characterSetFactory;
        }

        private readonly Uri location;
        private readonly IContentType type;
        private readonly IMediaTypeFactory mediaTypeFactory;
        private readonly ICharacterSetFactory characterSetFactory;

        private IXmlElement xml;
        private bool isLoaded;

        public Uri Location
        {
            get { return location; }
        }

        public IContentType Type
        {
            get { return type; }
        }

        public IXmlElement Xml
        {
            get { return xml; }
        }

        public IEnumerable<ILink> GetLinks()
        {
            if (!isLoaded)
                Load();

            foreach (var elem in Xml.Where<IHtmlAnchor>(x => x != null))
            {
                //TODO: use the rel attribute to get the actual link type
                Uri target = null;
                if (Uri.TryCreate(elem.Target, UriKind.RelativeOrAbsolute, out target))
                    yield return new Link(location, target, elem.Rel, elem.Content);
            }
        }

        public IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }

        public void Load()
        {
            if (!isLoaded)
            {
                isLoaded = true;
                this.xml = XmlElement.Parse(location, mediaTypeFactory, characterSetFactory);
            }
        }
    }
}
