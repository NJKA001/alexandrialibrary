using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Links;

namespace Gnosis.Application.Xml.Xhtml
{
    public class XhtmlDocument
        : IXmlDocument
    {
        public XhtmlDocument(Uri location, IContentType type)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (type == null)
                throw new ArgumentNullException("type");

            this.location = location;
            this.type = type;
        }

        private Uri location;
        private IContentType type;

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
            //var links = new List<ILink>();

            if (!isLoaded)
                Load();

            foreach (var elem in Xml.Where<IHtmlAnchor>(x => x != null && x.Target != null))
            {
                //TODO: use the rel attribute to get the actual link type
                Uri target = new Uri("http://example.com/index.html");
                if (Uri.TryCreate(elem.Target, UriKind.RelativeOrAbsolute, out target))
                    yield return new Link(location, target, elem.Rel, elem.Content);
            }

            //return links;
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
                this.xml = XhtmlElement.Parse(location);
            }
        }
    }
}
