using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public abstract class KeyAttribute : Attribute
    {
        protected KeyAttribute(string name, params string[] columns)
        {
            this.name = name;
            this.columns = columns.AsEnumerable<string>();
        }

        private readonly string name;
        private readonly IEnumerable<string> columns;

        public string Name
        {
            get { return name; }
        }

        public IEnumerable<string> Columns
        {
            get { return columns; }
        }
    }
}
