using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Document.Xml;

namespace Gnosis.Document.Xml.MediaRss
{
    public class MediaRssNamespace
        : NamespaceDeclaration
    {
        public MediaRssNamespace(INode parent)
            : base(parent, name, identifier)
        {
        }

        private static readonly IQualifiedName name = QualifiedName.Parse("xmlns:media");
        private static readonly Uri identifier = new Uri("http://search.yahoo.com/mrss/");
    }
}
