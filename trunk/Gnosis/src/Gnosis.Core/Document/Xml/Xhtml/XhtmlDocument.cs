﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.Xhtml
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

            this.xml = location.ToXhtmlDocument();
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
    }
}