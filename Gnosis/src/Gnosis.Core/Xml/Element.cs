using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class Element
        : Node, IElement
    {
        public Element(INode parent, IQualifiedName name)
            : base(parent)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.name = name;
        }

        private readonly IQualifiedName name;
        private readonly IList<IAttribute> attributes = new List<IAttribute>();

        protected T GetAttributeEnum<T>(string name, T defaultValue)
            where T : struct
        {
            var s = GetAttributeString(name).Replace("-", "_");

            var result = defaultValue;
            Enum.TryParse<T>(s, out result);
            return result;
        }

        protected int GetAttributeInt32(string name)
        {
            var s = GetAttributeString(name);

            var number = 0;
            int.TryParse(s, out number);
            return number;
        }

        protected long GetAttributeInt64(string name)
        {
            var s = GetAttributeString(name);

            long number = 0;
            long.TryParse(s, out number);
            return number;
        }

        protected string GetAttributeString(string name)
        {
            var attrib = attributes.Where(x => x.Name.ToString() == name).FirstOrDefault();

            return attrib != null ?
                attrib.Value
                : null;
        }

        protected Uri GetAttributeUri(string name)
        {
            var s = GetAttributeString(name);

            return s != null ?
                s.ToUri()
                : null;
        }

        protected bool GetAttributeBoolean(string name, bool defaultValue)
        {
            var s = GetAttributeString(name);

            var result = defaultValue;
            bool.TryParse(s, out result);
            return result;
        }

        protected DateTime GetAttributeDateTime(string name, DateTime defaultValue)
        {
            var s = GetAttributeString(name);

            var result = defaultValue;
            DateTime.TryParse(s, out result);
            return result;
        }

        protected ILanguageTag GetAttributeLanguageTag(string name)
        {
            var s = GetAttributeString(name);

            return s != null ?
                LanguageTag.Parse(s)
                : null;
        }

        protected IMediaType GetAttributeMediaType(string name)
        {
            var s = GetAttributeString(name);

            return s != null ?
                MediaType.Parse(s)
                : null;
        }

        protected bool GetContentBoolean(bool defaultValue)
        {
            var s = GetContentString();

            var result = defaultValue;
            bool.TryParse(s, out result);
            return result;
        }

        protected DateTime GetContentDateTime(DateTime defaultValue)
        {
            var s = GetContentString();

            var result = defaultValue;
            DateTime.TryParse(s, out result);
            return result;
        }

        protected T GetContentEnum<T>(T defaultValue)
            where T : struct
        {
            var s = GetContentString();

            var result = defaultValue;
            Enum.TryParse<T>(s, out result);
            return result;
        }

        protected int GetContentInt32(int defaultValue)
        {
            var s = GetContentString();

            var result = defaultValue;
            int.TryParse(s, out result);
            return result;
        }

        protected string GetContentString()
        {
            var child = Children.FirstOrDefault() as ICharacterData;

            return child != null ?
                child.Content
                : null;
        }

        protected Uri GetContentUri()
        {
            var s = GetContentString();

            return s != null ?
                s.ToUri()
                : null;
        }

        protected string GetChildString(string name)
        {
            var child = ChildElements.Where(elem => elem.Name.ToString() == name).FirstOrDefault();
            if (child == null)
                return null;

            var charData = child.Children.FirstOrDefault() as ICharacterData;

            return charData != null ?
                charData.Content
                : null;
        }

        protected DateTime GetChildDateTime(string name)
        {
            var s = GetChildString(name);

            var result = DateTime.MinValue;
            DateTime.TryParse(s, out result);
            return result;
        }

        protected int GetChildInt32(string name, int defaultValue)
        {
            var s = GetChildString(name);

            var result = defaultValue;
            int.TryParse(s, out result);
            return result;
        }

        protected Uri GetChildUri(string name)
        {
            var s = GetChildString(name);

            return s != null ?
                s.ToUri()
                : null;
        }

        #region IXmlElement Members

        public IQualifiedName Name
        {
            get { return name; }
        }

        public IElement ParentElement
        {
            get { return Parent as IElement; }
        }

        public IEnumerable<IAttribute> Attributes
        {
            get { return attributes; }
        }

        public IEnumerable<IComment> Comments
        {
            get { return Children.OfType<IComment>(); }
        }

        public IEnumerable<IElement> ChildElements
        {
            get { return Children.OfType<IElement>(); }
        }

        public IEnumerable<INamespaceDeclaration> Namespaces
        {
            get { return attributes.OfType<INamespaceDeclaration>(); }
        }

        public IEnumerable<ICharacterData> CharacterDataSections
        {
            get { return Children.OfType<ICharacterData>(); }
        }

        public INamespaceDeclaration CurrentNamespace
        {
            get { return FindNamespace(this.Name.Prefix); }
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

        public void AddAttribute(IAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");

            attributes.Add(attribute);
        }

        public INamespaceDeclaration FindNamespace(string alias)
        {
            var found = Namespaces.Where(ns => ns.Alias == alias).FirstOrDefault();
            if (found != null)
                return found;
            else if (ParentElement != null)
                return ParentElement.FindNamespace(alias);
            else
                return null;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            var xml = new StringBuilder();

            var indent = GetIndent();

            if (Parent != null && Parent is IElement)
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

                if (Children.Any(x => x is IElement))
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
