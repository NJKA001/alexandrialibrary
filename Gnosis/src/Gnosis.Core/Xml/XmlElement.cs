using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class XmlElement
        : IXmlElement
    {
        public XmlElement(string prefix, string name, IXmlElement parent, IEnumerable<IXmlComment> comments, IEnumerable<IXmlAttribute> attributes, IEnumerable<IXmlElement> children, IXmlCharacterData characterData)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (comments == null)
                throw new ArgumentNullException("comments");
            if (attributes == null)
                throw new ArgumentNullException("attributes");
            if (children == null)
                throw new ArgumentNullException("children");

            this.prefix = prefix;
            this.name = name;
            this.parent = parent;
            this.comments = comments;
            this.children = children;
            this.characterData = characterData;

            foreach (var attribute in attributes)
            {
                var key = attribute.Prefix != null ? string.Format("{0}:{1}", attribute.Prefix, attribute.Name) : attribute.Name;
                this.attributes.Add(key, attribute);
            }
        }

        private readonly string prefix;
        private readonly string name;
        private readonly IXmlElement parent;
        private readonly IEnumerable<IXmlComment> comments;
        private readonly IDictionary<string, IXmlAttribute> attributes = new Dictionary<string, IXmlAttribute>();
        private readonly IEnumerable<IXmlElement> children;
        private readonly IXmlCharacterData characterData;

        #region IXmlElement Members

        public string Prefix
        {
            get { return prefix; }
        }

        public string Name
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

            return attributes.ContainsKey(name) ? attributes[name] : null;
        }

        public IXmlAttribute GetAttribute(string name, string prefix)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (prefix == null)
                throw new ArgumentNullException("prefix");

            var key = string.Format("{0}:{1}", prefix, name);
            return GetAttribute(key);
        }

        public IEnumerable<IXmlNamespace> GetNamespaces()
        {
            return attributes.OfType<IXmlNamespace>();
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            var tag = prefix != null ? string.Format("{0}:{1}", prefix, name) : name;
            xml.AppendFormat("<{0}", tag);

            foreach (var attribute in attributes.Values)
                xml.AppendFormat(" {0}", attribute.ToString());

            if (children.Count() > 0)
            {
                xml.AppendLine(">");
                foreach (var child in children)
                    xml.AppendLine(child.ToString());
                xml.AppendFormat("</{0}>", tag);
                xml.AppendLine();
            }
            else if (characterData != null)
            {
                xml.AppendFormat(">{0}</{1}>", characterData.ToString(), tag);
                xml.AppendLine();
            }
            else
            {
                xml.AppendLine("/>");
            }

            return xml.ToString();
        }
    }
}
