using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class XmlElement
        : XmlNode, IXmlElement
    {
        public XmlElement(IXmlNode parent, IEnumerable<IXmlNode> children, IXmlQualifiedName name, IEnumerable<IXmlAttribute> attributes)
            : base(parent, children)
        {
            if (children == null)
                throw new ArgumentNullException("children");
            if (name == null)
                throw new ArgumentNullException("name");
            if (attributes == null)
                throw new ArgumentNullException("attributes");

            this.name = name;
            this.attributes = attributes;

            foreach (var attribute in attributes)
            {
                if (attribute.Parent == null)
                    attribute.Parent = this;
            }
        }

        private readonly IXmlQualifiedName name;
        private readonly IEnumerable<IXmlAttribute> attributes;

        #region IXmlElement Members

        public IXmlQualifiedName Name
        {
            get { return name; }
        }

        public IXmlElement ParentElement
        {
            get { return Parent as IXmlElement; }
        }

        public IEnumerable<IXmlAttribute> Attributes
        {
            get { return attributes; }
        }

        public IEnumerable<IXmlComment> Comments
        {
            get { return Children.OfType<IXmlComment>(); }
        }

        public IEnumerable<IXmlElement> ChildElements
        {
            get { return Children.OfType<IXmlElement>(); }
        }

        public IEnumerable<IXmlNamespace> Namespaces
        {
            get { return attributes.OfType<IXmlNamespace>(); }
        }

        public IEnumerable<IXmlCharacterData> CharacterDataSections
        {
            get { return Children.OfType<IXmlCharacterData>(); }
        }

        public override IEnumerable<T> Where<T>(Func<T, bool> predicate)
        {
            var results = new List<T>();

            var self = this as T;
            if (self != null && predicate(self))
                results.Add(self);

            foreach (var attribute in attributes)
                results.AddRange(attribute.Where(predicate));

            foreach (var child in Children)
                results.AddRange(child.Where(predicate));

            return results;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            var xml = new StringBuilder();

            var indent = GetIndent();

            if (Parent != null && Parent is IXmlElement)
                xml.AppendLine();

            xml.AppendFormat("{0}<{1}", indent, name);

            foreach (var attribute in attributes)
                xml.AppendFormat(" {0}", attribute.ToString());

            var count = Children.Count();
            if (count > 0)
            {
                xml.Append(">");
                foreach (var child in Children)
                    xml.Append(child.ToString());

                if (Children.Any(x => x is IXmlElement))
                {
                    xml.AppendLine();
                    xml.AppendFormat("{0}</{1}>", indent, name);
                }
                else
                    xml.AppendFormat("</{0}>", name);
            }
            else
            {
                xml.Append("/>");
            }

            return xml.ToString();
        }

        #endregion
    }
}
