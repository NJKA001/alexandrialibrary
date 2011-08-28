using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml
{
    public class NamespaceDeclaration
        : Attribute, INamespaceDeclaration
    {
        protected NamespaceDeclaration(INode parent, IQualifiedName name, Uri identifier)
            : base(parent, name, identifier.ToString())
        {
            this.identifier = identifier;
        }

        private readonly Uri identifier;

        #region IXmlNamespace Members

        public string Alias
        {
            get { return Name.Prefix == null ? null : Name.LocalPart; }
        }

        public Uri Identifier
        {
            get { return identifier; }
        }

        #endregion

        public static INamespaceDeclaration ParseNamespace(INode parent, IQualifiedName name, string value)
        {
            if (name.Prefix != "xmlns" && name.LocalPart != "xmlns")
                throw new ArgumentException("Either the prefix or local part of a namespace must be 'xmlns'");

            var identifier = new Uri(value, UriKind.Absolute);

            return new NamespaceDeclaration(parent, name, identifier);
        }
    }
}
