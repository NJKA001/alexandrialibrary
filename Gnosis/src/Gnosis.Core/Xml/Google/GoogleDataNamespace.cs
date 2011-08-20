using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Google
{
    public class GoogleDataNamespace
        : NamespaceDeclaration
    {
        public GoogleDataNamespace(INode parent)
            : base(parent, name, identifier)
        {
        }

        private static readonly IQualifiedName name = QualifiedName.Parse("xmlns:gd");
        private static readonly Uri identifier = new Uri("http://schemas.google.com/g/2005");
    }
}
