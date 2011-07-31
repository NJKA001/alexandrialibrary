using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class Attribute
        : Node, IAttribute
    {
        protected Attribute(IQualifiedName name, string value)
        {
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

        public static IAttribute Parse(IQualifiedName name, string value)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (value == null)
                throw new ArgumentNullException("value");

            return (name.Prefix == "xmlns" || (name.Prefix == null && name.LocalPart == "xmlns")) ?
                Namespace.ParseNamespace(name, value) as IAttribute :
                new Attribute(name, value);
        }
    }
}
