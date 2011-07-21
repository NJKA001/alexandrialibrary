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
            this.attributes = attributes;
            this.characterData = characterData;
        }

        private readonly IXmlQualifiedName name;
        private readonly IXmlElement parent;
        private readonly IEnumerable<IXmlComment> comments;
        private readonly IEnumerable<IXmlAttribute> attributes;
        private readonly IXmlCharacterData characterData;
        private readonly IList<IXmlElement> children = new List<IXmlElement>();

        #region Private Methods

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

        #endregion

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
            get { return attributes; }
        }

        public IEnumerable<IXmlElement> Children
        {
            get { return children; }
        }

        public IXmlCharacterData CharacterData
        {
            get { return characterData; }
        }

        public IEnumerable<IXmlNamespace> Namespaces
        {
            get { return attributes.OfType<IXmlNamespace>(); }
        }

        public IEnumerable<T> Where<T>(Func<T, bool> predicate)
            where T : class, IXmlNode
        {
            var results = new List<T>();

            var test = this as T;
            if (test != null && predicate(test))
                results.Add(test);

            if (typeof(T) == typeof(IXmlQualifiedName))
            {
                var item = name as T;
                if (item != null && predicate(item))
                    results.Add(item);

                foreach (var attribName in attributes.Select(attrib => attrib.Name))
                {
                    var attribItem = attribName as T;
                    if (attribItem != null && predicate(attribItem))
                        results.Add(attribItem);
                }
            }
            if (typeof(T) == typeof(IXmlAttribute))
            {
                foreach (var attribute in attributes)
                {
                    var item = attribute as T;
                    if (item != null && predicate(item))
                        results.Add(item);
                }
            }
            if (typeof(T) == typeof(IXmlComment))
            {
                foreach (var comment in comments)
                {
                    var item = comment as T;
                    if (item != null && predicate(item))
                        results.Add(item);
                }
            }
            if (typeof(T) == typeof(IXmlNamespace))
            {
                foreach (var ns in Namespaces)
                {
                    var item = ns as T;
                    if (item != null && predicate(item))
                        results.Add(item);
                }
            }
            if (typeof(T) == typeof(IXmlEscapedSection) || typeof(T) == typeof(IXmlCDataSection))
            {
                var item = characterData as T;
                if (item != null && predicate(item))
                    results.Add(item);
            }

            foreach (var child in children)
                results.AddRange(child.Where(predicate));

            return results;
        }

        public void AddChild(IXmlElement child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            children.Add(child);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            var xml = new StringBuilder();

            const int multiplier = 2;
            var depth = GetDepth();
            var indent = (depth > 0) ? string.Empty.PadLeft(depth * multiplier) : string.Empty;
            var childIndent = string.Empty.PadLeft((depth + 1) * multiplier);
            var closeIndent = string.Empty.PadLeft((depth + 2) * multiplier);

            xml.AppendFormat("{0}<{1}", indent, name);

            foreach (var attribute in attributes)
                xml.AppendFormat(" {0}", attribute.ToString());

            var hasContent = false;
            var inline = true;

            if (children.Count() > 0)
            {
                hasContent = true;
                inline = false;
                xml.AppendLine(">");
                foreach (var child in children)
                {
                    xml.AppendFormat("{0}{1}", childIndent, child);
                    xml.AppendLine();
                }
            }
            else if (characterData != null)
            {
                hasContent = true;
                var characterContent = characterData.ToString();
                if (characterContent.Length > 120 || characterContent.Contains("\r") || characterContent.Contains("\n") || characterContent.Contains("\t"))
                {
                    inline = false;
                    xml.Append(">");
                    xml.Append(characterContent);
                }
                else
                {
                    xml.AppendFormat(">{0}", characterContent, name);
                }
            }

            if (comments.Count() > 0)
            {
                if (!hasContent)
                    xml.AppendLine(">");

                hasContent = true;

                foreach (var comment in comments)
                {
                    xml.AppendFormat("{0}{1}", childIndent, comment);
                    xml.AppendLine();
                }
            }

            if (hasContent)
            {
                if (inline)
                    xml.AppendFormat("</{0}>", name);
                else
                    xml.AppendFormat("{0}</{1}>", closeIndent, name);
                    
            }
            else
                xml.Append("/>");

            return xml.ToString();
        }

        #endregion
    }
}
