using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public abstract class KeyAttribute : Attribute
    {
        protected KeyAttribute(string name, bool isUnique, params string[] columns)
        {
            this.name = name;
            this.columns = columns.AsEnumerable<string>();
        }

        private readonly string name;
        private readonly bool isUnique;
        private readonly IEnumerable<string> columns;

        public string Name
        {
            get { return name; }
        }

        public bool IsUnique
        {
            get { return isUnique; }
        }

        public IEnumerable<string> Columns
        {
            get { return columns; }
        }
    }
}
