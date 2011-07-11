using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public abstract class AtomDateConstruct
        : AtomCommon, IAtomDateConstruct
    {
        protected AtomDateConstruct(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, DateTime date)
            : base(baseId, lang, extensions)
        {
            this.date = date;
        }

        private readonly DateTime date;

        protected string ToString(string tag)
        {
            var xml = new StringBuilder();
            AppendStartTag(xml, tag);
            xml.AppendFormat(date.ToRfc3339String());
            AppendEndTag(xml, tag);
            return xml.ToString();
        }

        #region IAtomDateConstruct Members

        public DateTime Date
        {
            get { return date; }
        }

        #endregion
    }
}
