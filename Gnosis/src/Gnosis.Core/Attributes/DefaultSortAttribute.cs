using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class DefaultSortAttribute : Attribute
    {
        public DefaultSortAttribute(string expression)
        {
            this.expression = expression;
        }

        private readonly string expression;

        public string Expression
        {
            get { return expression; }
        }
    }
}
