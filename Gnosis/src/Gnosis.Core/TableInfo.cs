using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TableInfo
    {
        public TableInfo(string name, string defaultSort, IEnumerable<ColumnInfo> columns, IEnumerable<IndexInfo> indices)
        {
            this.name = name;
            this.defaultSort = defaultSort;
            this.columns = columns;
            this.indices = indices;
        }

        private readonly string name;
        private readonly string defaultSort = string.Empty;
        private readonly IEnumerable<ColumnInfo> columns;
        private readonly IEnumerable<IndexInfo> indices;

        public string Name
        {
            get { return name; }
        }

        public string DefaultSort
        {
            get { return defaultSort; }
        }

        public IEnumerable<ColumnInfo> Columns
        {
            get { return columns; }
        }

        public IEnumerable<IndexInfo> Indices
        {
            get { return indices; }
        }
    }
}
