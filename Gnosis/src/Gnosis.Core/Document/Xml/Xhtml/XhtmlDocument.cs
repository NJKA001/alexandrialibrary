using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.Xhtml
{
    public class XhtmlDocument
        : IXmlDocument
    {
        public XhtmlDocument(Uri location, IContentType contentType)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (contentType == null)
                throw new ArgumentNullException("contentType");

            this.location = location;
            this.contentType = contentType;

            this.xml = location.ToXhtmlDocument();
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
