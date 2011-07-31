using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Xml
{
    public class LangAttribute
        : Attribute, ILangAttribute
    {
        public LangAttribute(IQualifiedName name, string value)
            : base(name, value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this.value = LanguageTag.Parse(value);
        }

        private readonly ILanguageTag value;

        #region IXmlLangAttribute Members

        ILanguageTag ILangAttribute.Value
        {
            get { return value; }
        }

        #endregion
    }
}
