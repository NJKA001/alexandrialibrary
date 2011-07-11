using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public abstract class AtomCommon
        : IAtomCommon
    {
        protected AtomCommon(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions)
        {
            this.baseId = baseId;
            this.lang = lang;
            this.extensions = extensions;
        }

        private readonly Uri baseId;
        private readonly ILanguageTag lang;
        private readonly IEnumerable<IAtomExtension> extensions;

        #region XML Helper methods

        protected void AppendTagAttributes(StringBuilder xml, IEnumerable<KeyValuePair<string, string>> attributes, IEnumerable<IXmlNamespace> namespaces)
        {
            foreach (var attrib in attributes)
                xml.AppendFormat(" {0}='{1}'", attrib.Key, attrib.Value);

            foreach (var ns in namespaces)
                xml.AppendFormat(" {0}", ns.ToString());

            if (baseId != null)
                xml.AppendFormat(" xml:base='{0}'", baseId.ToString());

            if (lang != null)
                xml.AppendFormat(" xml:lang='{0}'", lang.ToString());
        }

        protected void AppendStartTag(StringBuilder xml, string name)
        {
            AppendStartTag(xml, name, new List<KeyValuePair<string, string>>(), new List<IXmlNamespace>());
        }

        protected void AppendStartTag(StringBuilder xml, string name, IEnumerable<KeyValuePair<string, string>> attributes)
        {
            AppendStartTag(xml, name, attributes, new List<IXmlNamespace>());
        }

        protected void AppendStartTag(StringBuilder xml, string name, IEnumerable<IXmlNamespace> namespaces)
        {
            AppendStartTag(xml, name, new List<KeyValuePair<string, string>>(), namespaces);
        }

        protected void AppendStartTag(StringBuilder xml, string name, IEnumerable<KeyValuePair<string, string>> attributes, IEnumerable<IXmlNamespace> namespaces)
        {
            xml.AppendFormat("<{0}", name);

            AppendTagAttributes(xml, attributes, namespaces);

            xml.Append(">");

            if (extensions.Count() > 0)
            {
                xml.AppendLine();

                foreach (var extension in extensions)
                    xml.AppendLine(extension.Content);
            }
        }

        protected void AppendEndTag(StringBuilder xml, string name)
        {
            xml.AppendFormat("</{0}>", name);
            xml.AppendLine();
        }

        protected void AppendTag(StringBuilder xml, string name, IEnumerable<KeyValuePair<string, string>> attributes)
        {
            AppendTag(xml, name, attributes, new List<IXmlNamespace>());
        }

        protected void AppendTag(StringBuilder xml, string name, IEnumerable<KeyValuePair<string, string>> attributes, IEnumerable<IXmlNamespace> namespaces)
        {
            xml.AppendFormat("<{0}", name);

            AppendTagAttributes(xml, attributes, namespaces);

            if (extensions.Count() == 0)
                xml.Append("/>");
            else
            {
                xml.Append(">");
                xml.AppendLine();

                foreach (var extension in extensions)
                {
                    xml.AppendLine(extension.ToString());
                }

                xml.AppendFormat("</{0}>", name);
                xml.AppendLine();
            }
        }

        #endregion

        #region IAtomCommon Members

        public Uri  BaseId
        {
	        get { return baseId; }
        }

        public ILanguageTag  Lang
        {
	        get { return lang; }
        }

        public IEnumerable<IAtomExtension> Extensions
        {
            get { return extensions; }
        }

        #endregion
    }
}
