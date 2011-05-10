using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
    public class CreateIndexBuilder : IStatementBuilder
    {
        public CreateIndexBuilder(string name, bool isUnique, string table)
        {
            var indexType = isUnique ? "unique index" : "index";

            builder = new StringBuilder();
            builder.AppendFormat("create {0} if not exists {1} on {2} (", indexType, name, table);
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
