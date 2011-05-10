using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Attributes
{
    public class TableCheckConstraintAttribute : TableConstraintAttribute
    {
        public TableCheckConstraintAttribute(string name, string expression)
            : base(name)
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
