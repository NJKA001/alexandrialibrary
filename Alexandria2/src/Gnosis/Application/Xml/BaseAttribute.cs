using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml
{
    public class BaseAttribute
        : Attribute, IBaseAttribute
    {
        public BaseAttribute(INode parent, IQualifiedName name, string value)
            : base(parent, name, value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this.value = new Uri(value, UriKind.RelativeOrAbsolute);
        }

        private readonly Uri value;

        #region IXmlBaseAttribute Members

        Uri IBaseAttribute.Value
        {
            get { return value; }
        }

        #endregion
    }
}
