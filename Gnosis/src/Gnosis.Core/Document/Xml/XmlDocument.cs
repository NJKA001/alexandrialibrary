using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml
{
    public class XmlDocument
        : IXmlDocument
    {
        public XmlDocument(Uri location, IMediaType type)
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
            this.xml = location.ToXmlDocument();
        }
    }
}
