using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml
{
    public class XmlDocument
        : IXmlDocument
    {
        public XmlDocument(Uri location, IContentType contentType)
        {
            this.location = location;
            this.contentType = contentType;

            this.xml = location.ToXmlDocument();
        }

        private Uri location;
        private IContentType contentType;
        private IXmlElement xml;

        public Uri Location
        {
            get { return location; }
        }

        public IContentType ContentType
        {
            get { return contentType; }
        }

        public IXmlElement Xml
        {
            get { return xml; }
        }
    }
}
