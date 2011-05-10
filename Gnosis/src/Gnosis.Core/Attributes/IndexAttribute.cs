using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
    public class IndexAttribute : Attribute
    {
        protected IndexAttribute(string name, bool isUnique, params string[] columns)
        {
            this.name = name;
            this.isUnique = isUnique;
            this.columns = columns.AsEnumerable<string>();
        }

        public IndexAttribute(string name, params string[] columns)
        {
            this.name = name;
            this.columns = columns.AsEnumerable<string>();
        }

        private readonly string name;
        private readonly bool isUnique = false;
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
