using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public class XmlNamespace
        : IXmlNamespace
    {
        public XmlNamespace(Uri identifier)
            : this(identifier, null)
        {   
        }

        public XmlNamespace(Uri identifier, string prefix)
        {
            this.identifier = identifier;
            this.prefix = prefix;
        }

        private readonly Uri identifier;
        private readonly string prefix;

        #region INamespace Members

        public Uri Identifier
        {
            get { return identifier; }
        }

        public string Prefix
        {
            get { return prefix; }
        }

        #endregion

        public override string ToString()
        {
            return !string.IsNullOrEmpty(prefix) ? 
                string.Format("xmlns:{0}='{1}'", prefix, identifier.ToString())
                : string.Format("xmlns='{0}'", identifier.ToString());
        }

        public static IXmlNamespace Parse(string value)
        {
            if (string.IsNullOrEmpty(value) || !value.StartsWith("xmlns") || !value.Contains('='))
                return null;

            var tokens = value.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens == null || tokens.Length != 2)
                return null;

            var identifier = new Uri(tokens[1], UriKind.RelativeOrAbsolute);

            if (!tokens[0].Contains(':'))
                return new XmlNamespace(identifier);

            var pieces = tokens[0].Split(':');
            if (pieces == null || pieces.Length != 2)
                return new XmlNamespace(identifier);

            return new XmlNamespace(identifier, pieces[1]);
        }
    }
}
