using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml
{
    public class Attribute
        : Node, IAttribute
    {
        protected Attribute(INode parent, IQualifiedName name, string value)
            : base(parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            if (name == null)
                throw new ArgumentNullException("name");

            this.name = name;
            this.value = value;
        }

        private readonly IQualifiedName name;
        private readonly string value;

        #region IXmlAttribute Members

        public IElement ParentElement
        {
            get { return Parent as IElement; }
        }

        public IQualifiedName Name
        {
            get { return name; }
        }

        public string Value
        {
            get { return value; }
        }

        #endregion

        public override string ToString()
        {
            var escapedValue = value != null ? value.ToXmlEscapedString() : string.Empty;

            return string.Format("{0}=\"{1}\"", name.ToString(), escapedValue);
        }

        public static IAttribute Parse(INode parent, IQualifiedName name, string value)
        {
            return (name.Prefix == "xmlns" || (name.Prefix == null && name.LocalPart == "xmlns")) ?
                NamespaceDeclaration.ParseNamespace(parent, name, value) as IAttribute :
                new Attribute(parent, name, value);
        }
    }
}
