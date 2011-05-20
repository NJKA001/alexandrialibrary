using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TableInfo
    {
        public TableInfo(string name, string defaultSort, IEnumerable<ColumnInfo> columns, IEnumerable<IndexInfo> indices, IEnumerable<OneToManyInfo> children, IEnumerable<CustomDataTypeInfo> customDataTypes)
        {
            this.name = name;
            this.defaultSort = defaultSort;
            this.columns = columns;
            this.indices = indices;
            this.children = children;
            this.customDataTypes = customDataTypes;
        }

        private readonly string name;
        private readonly string defaultSort = string.Empty;
        private readonly IEnumerable<ColumnInfo> columns;
        private readonly IEnumerable<IndexInfo> indices;
        private readonly IEnumerable<OneToManyInfo> children;
        private readonly IEnumerable<CustomDataTypeInfo> customDataTypes;

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

        public IEnumerable<OneToManyInfo> Children
        {
            get { return children; }
        }

        public IEnumerable<CustomDataTypeInfo> CustomDataTypes
        {
            get { return customDataTypes; }
        }
    }
}
