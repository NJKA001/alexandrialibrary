using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public abstract class AtomTextConstruct
        : AtomCommon, IAtomTextConstruct
    {
        protected AtomTextConstruct(Uri baseId, ILanguageTag lang, IEnumerable<IXmlExtension> extensions, IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, string text, AtomTextType type)
            : base(baseId, lang, extensions, namespaces, primaryNamespace)
        {
            this.text = text;
            this.type = type;
        }

        private readonly string text;
        private readonly AtomTextType type = AtomTextType.Text;

        protected string ToString(string tag)
        {
            var xml = new StringBuilder();
            var attributes = new Dictionary<string, string>();
            attributes.Add("type", Type.ToString().ToLower());

            xml.AppendFormat("<{0}", tag);

            AppendTagAttributes(xml, attributes, new List<IXmlNamespace>());

            xml.AppendFormat(">{0}</{1}>", text ?? string.Empty, tag);

            return xml.ToString();
        }

        #region IAtomTextConstruct Members

        public string Text
        {
            get { return text; }
        }

        public AtomTextType Type
        {
            get { return type; }
        }

        #endregion
    }
}
