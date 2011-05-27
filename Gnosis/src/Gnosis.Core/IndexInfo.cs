using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Attributes;

namespace Gnosis.Core
{
    public class IndexInfo
    {
        public IndexInfo(string name, bool isUnique, IEnumerable<string> columns)
        {
            this.name = name;
            this.isUnique = isUnique;
            this.columns = columns;
        }

        public IndexInfo(IndexAttribute indexAttribute)
        {
            this.name = indexAttribute.Name;
            this.isUnique = indexAttribute.IsUnique;
            this.columns = indexAttribute.Columns;
        }

        public IndexInfo(ForeignIndexAttribute indexAttribute)
        {
            this.name = indexAttribute.Name;
            this.isUnique = indexAttribute.IsUnique;
            this.columns = indexAttribute.Columns;
        }

        private readonly string name;
        private readonly bool isUnique;
        private readonly IEnumerable<string> columns;

        private string columnList
        {
            get { return columns.Aggregate((result, column) => result + ", " + column); }
        }

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

        public override string ToString()
        {
            if (isUnique)
                return string.Format("UNIQUE INDEX {0} ({1})", name, columnList);
            else
                return string.Format("INDEX {0} ({1})", name, columnList);
        }
    }
}
