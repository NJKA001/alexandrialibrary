using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
    public class CreateIndexBuilder : IStatementBuilder
    {
        public CreateIndexBuilder(string table, string name, bool isUnique)
        {
            var indexType = isUnique ? "unique index" : "index";

            builder = new StringBuilder();
            builder.AppendFormat("create {0} if not exists {1} on {2} (", indexType, name, table);
        }

        public CreateIndexBuilder(string table, string name, bool isUnique, IEnumerable<string> columns)
            : this(table, name, isUnique)
        {
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

        public CreateIndexBuilder Column(string name)
        {
            AppendPrefix();

            builder.Append(name);

            hasColumns = true;
            return this;
        }

        public CreateIndexBuilder AscendingColumn(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} ASC", name);

            hasColumns = true;
            return this;
        }

        public CreateIndexBuilder DescendingColumn(string name)
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
