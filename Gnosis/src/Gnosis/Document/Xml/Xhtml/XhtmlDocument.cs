using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Xhtml
{
    public class XhtmlDocument
        : IXmlDocument
    {
        public XhtmlDocument(Uri location, IMediaType type)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (type == null)
                throw new ArgumentNullException("type");

            this.location = location;
            this.type = type;
        }

        private Uri location;
        private IMediaType type;
        private IXmlElement xml;

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return type; }
        }

        public IXmlElement Xml
        {
            get { return xml; }
        }

        public void Load()
        {
            this.xml = location.ToXhtmlDocument();
        }
    }
}
