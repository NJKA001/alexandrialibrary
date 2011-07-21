using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class XmlElement
        : IXmlElement
    {
        public XmlElement(IXmlQualifiedName name, IXmlElement parent, IEnumerable<IXmlComment> comments, IEnumerable<IXmlAttribute> attributes, IXmlCharacterData characterData)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (comments == null)
                throw new ArgumentNullException("comments");
            if (attributes == null)
                throw new ArgumentNullException("attributes");

            this.name = name;
            this.parent = parent;
            this.comments = comments;
            this.characterData = characterData;

            foreach (var attribute in attributes)
                this.attributes.Add(attribute.Name.ToString(), attribute);
        }

        private readonly IXmlQualifiedName name;
        private readonly IXmlElement parent;
        private readonly IEnumerable<IXmlComment> comments;
        private readonly IDictionary<string, IXmlAttribute> attributes = new Dictionary<string, IXmlAttribute>();
        private readonly IList<IXmlElement> children = new List<IXmlElement>();
        private readonly IXmlCharacterData characterData;

        private int GetDepth()
        {
            var depth = 0;

            var currentParent = parent;
            while (currentParent != null)
            {
                depth++;
                currentParent = currentParent.Parent;
            }

            return depth;
        }

        #region IXmlElement Members

        public IXmlQualifiedName Name
        {
            get { return name; }
        }

        public IXmlElement Parent
        {
            get { return parent; }
        }

        public IEnumerable<IXmlComment> Comments
        {
            get { return comments; }
        }

        public IEnumerable<IXmlAttribute> Attributes
        {
            get { return attributes.Values; }
        }

        public IEnumerable<IXmlElement> Children
        {
            get { return children; }
        }

        public IXmlCharacterData CharacterData
        {
            get { return characterData; }
        }

        public IXmlAttribute GetAttribute(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            var qName = XmlQualifiedName.Parse(name);
            if (qName == null)
                return null;

            var key = qName.ToString();
            return attributes.ContainsKey(key) ? attributes[key] : null;
        }

        public IEnumerable<IXmlNamespace> GetNamespaces()
        {
            return attributes.OfType<IXmlNamespace>();
        }

        public void AddChild(IXmlElement child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            children.Add(child);
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            const int multiplier = 2;
            var depth = GetDepth();
            var indent = (depth > 0) ? string.Empty.PadLeft(depth * multiplier) : string.Empty;
            var childIndent = string.Empty.PadLeft((depth + 1) * multiplier);

            xml.AppendFormat("{0}<{1}", indent, name);

            foreach (var attribute in attributes.Values)
                xml.AppendFormat(" {0}", attribute.ToString());

            if (children.Count() > 0)
            {
                xml.AppendLine(">");
                foreach (var child in children)
                {
                    xml.AppendFormat("{0}{1}", childIndent, child);
                    xml.AppendLine();
                }

                xml.AppendFormat("{0}</{1}>", indent, name);
            }
            else if (characterData != null)
            {
                var characterContent = characterData.ToString();
                if (characterContent.Length > 120 || characterContent.Contains("\r") || characterContent.Contains("\n") || characterContent.Contains("\t"))
                {
                    xml.Append(">");
                    xml.Append(characterContent);
                    xml.AppendFormat("{0}</{1}>", indent, name);
                }
                else
                {
                    xml.AppendFormat(">{0}</{1}>", characterContent, name);
                }
            }
            else
            {
                xml.Append("/>");
            }

            return xml.ToString();
        }
    }
}
