using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
    public class CreateIndexBuilder
    {
        public CreateIndexBuilder(string name, string table)
        {
            builder = new StringBuilder();
            builder.AppendFormat("create index if not exists {0} on {1} ({2}", name, table, Environment.NewLine);
        }

        private readonly StringBuilder builder;
        private bool hasColumns;

        private void AppendPrefix()
        {
            if (hasColumns)
                builder.Append("," + Environment.NewLine);
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
