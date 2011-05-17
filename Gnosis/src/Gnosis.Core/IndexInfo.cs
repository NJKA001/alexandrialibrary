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
