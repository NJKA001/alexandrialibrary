using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Commands
{
    public class CreateIndexStatement : IStatement
    {
        public CreateIndexStatement(TableInfo tableInfo, IndexInfo indexInfo)
        {
            var indexType = indexInfo.IsUnique ? "unique index" : "index";

            builder = new StringBuilder();
            builder.AppendFormat("create {0} if not exists {1} on {2} (", indexType, indexInfo.Name, tableInfo.Name);

            foreach (var column in indexInfo.Columns)
                Column(column);
        }

        private readonly StringBuilder builder;
        private bool hasColumns;

        private void AppendPrefix()
        {
            if (hasColumns)
                builder.Append(", ");
        }

        public CreateIndexStatement Column(string name)
        {
            AppendPrefix();

            builder.Append(name);

            hasColumns = true;
            return this;
        }

        public CreateIndexStatement AscendingColumn(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} ASC", name);

            hasColumns = true;
            return this;
        }

        public CreateIndexStatement DescendingColumn(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} DESC", name);

            hasColumns = true;
            return this;
        }

        public override string ToString()
        {
            return builder.ToString() + ");";
        }
    }
}
