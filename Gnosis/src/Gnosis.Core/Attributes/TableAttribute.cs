using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Attributes
{
    public class TableAttribute : Attribute
    {
        public TableAttribute()
        {
        }

        public TableAttribute(string name)
        {
            this.name = name;
        }

        private readonly string name;

        public string Name
        {
            get { return name; }
        }
    }
}
