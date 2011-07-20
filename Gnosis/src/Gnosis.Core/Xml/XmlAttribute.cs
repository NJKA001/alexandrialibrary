using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class XmlAttribute
        : IXmlAttribute
    {
        protected XmlAttribute(string name, string value, string prefix)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.name = name;
            this.value = value;
            this.prefix = prefix;
        }

        private readonly string name;
        private readonly string value;
        private readonly string prefix;

        #region IXmlAttribute Members

        public string Prefix
        {
            get { return prefix; }
        }

        public string Name
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

            return prefix != null ? 
                string.Format("{0}:{1}=\"{2}\"", prefix, name, escapedValue) : 
                string.Format("{0}=\"{1}\"", name, escapedValue);
        }

        public static IXmlAttribute Parse(string name, string value)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            var localName = name;
            string prefix = null;

            if (name.Contains(':'))
            {
                var tokens = name.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens == null || tokens.Length != 2)
                    throw new ArgumentException("Invalid attribute name");

                prefix = tokens[0];
                localName = tokens[1];
            }

            return (prefix == "xmlns" || localName == "xmlns") ?
                XmlNamespace.Parse(localName, value, prefix) as IXmlAttribute:
                new XmlAttribute(localName, value, prefix);
        }
    }
}
