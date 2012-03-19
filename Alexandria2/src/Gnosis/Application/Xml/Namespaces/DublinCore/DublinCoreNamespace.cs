using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.DublinCore
{
    public class DublinCoreNamespace
        : NamespaceDeclaration
    {
        public DublinCoreNamespace(INode parent)
            : base(parent, name, identifier)
        {
        }

        private static readonly IQualifiedName name = QualifiedName.Parse("xmlns:dc");
        private static readonly Uri identifier = new Uri("http://purl.org/dc/elements/1.1/");
    }
}
