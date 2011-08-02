using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HtmlAgilityPack;

namespace Gnosis.Core.Xml.Xhtml
{
    public class XhtmlDocument
    {
        public XhtmlDocument(HtmlDocument document)
        {
            this.document = document;
        }

        private readonly HtmlDocument document;

        public HtmlDocument Document
        {
            get { return document; }
        }
    }
}
