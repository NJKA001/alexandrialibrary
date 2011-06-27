using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data.Commands
{
    public class CreateIndexStatement : IStatement
    {
        public CreateIndexStatement(ILookup lookup)
            : this(lookup.SourceName, lookup.Name, true, lookup.Columns)
        {
        }

        public CreateIndexStatement(ISearch search)
            : this(search.SourceName, search.Name, false, search.Columns)
        {
        }

        private CreateIndexStatement(string tableName, string name, bool isUnique, IEnumerable<string> columns)
        {
            var indexType = isUnique ? "unique index" : "index";

            builder = new StringBuilder();
            builder.AppendFormat("create {0} if not exists {1} on {2} (", indexType, name, tableName);

            foreach (var column in columns)
                Column(column);
        }

        private readonly StringBuilder builder;
        private bool hasColumns;

        private void AppendPrefix()
        {
            if (hasColumns)
                builder.Append(", ");
        }

        private CreateIndexStatement Column(string name)
        {
            AppendPrefix();

            builder.Append(name);

            hasColumns = true;
            return this;
        }

        private CreateIndexStatement AscendingColumn(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} ASC", name);

            hasColumns = true;
            return this;
        }

        private CreateIndexStatement DescendingColumn(string name)
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
