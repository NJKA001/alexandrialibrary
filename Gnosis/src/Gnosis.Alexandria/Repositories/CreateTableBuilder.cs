using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
    public class CreateTableBuilder
    {
        public CreateTableBuilder(string name)
        {
            builder = new StringBuilder();
            builder.AppendFormat("create table if not exists {0} ({1}", name, Environment.NewLine);
        }

        private readonly StringBuilder builder;
        private bool hasColumns;

        private void AppendPrefix()
        {
            if (hasColumns)
                builder.Append("," + Environment.NewLine);
        }

        private void AddBlobColumn(string name, byte defaultValue)
        {
            AppendPrefix();

            builder.AppendFormat("{0} BLOB NOT NULL DEFAULT {1}", name, defaultValue);

            hasColumns = true;
        }

        private void AddIntegerColumn(string name, int defaultValue)
        {
            AppendPrefix();

            builder.AppendFormat("{0} INTEGER NOT NULL DEFAULT {1}", name, defaultValue);

            hasColumns = true;
        }

        private void AddRealColumn(string name, decimal defaultValue)
        {
            AppendPrefix();

            builder.AppendFormat("{0} REAL NOT NULL DEFAULT {1}", name, defaultValue);

            hasColumns = true;
        }

        private void AddTextColumn(string name, string defaultValue)
        {
            AppendPrefix();

            builder.AppendFormat("{0} TEXT NOT NULL DEFAULT '{1}'", name, defaultValue);

            hasColumns = true;
        }

        private void AddTextColumn(string name, DateTime defaultValue)
        {
            AppendPrefix();

            builder.AppendFormat("{0} TEXT NOT NULL DEFAULT '{1}'", name, defaultValue.ToString("s"));

            hasColumns = true;
        }

        public CreateTableBuilder PrimaryKeyIntegerAutoIncrement(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} INTEGER PRIMARY KEY AUTOINCREMENT", name);

            hasColumns = true;

            return this;
        }

        public CreateTableBuilder PrimaryKeyText(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} TEXT PRIMARY KEY NOT NULL", name);

            hasColumns = true;

            return this;
        }

        public CreateTableBuilder BlobColumn(string name, byte defaultValue)
        {
            AddBlobColumn(name, defaultValue);
            return this;
        }

        public CreateTableBuilder IntegerColumn(string name, int defaultValue)
        {
            AddIntegerColumn(name, defaultValue);
            return this;
        }

        public CreateTableBuilder RealColumn(string name, decimal defaultValue)
        {
            AddRealColumn(name, defaultValue);
            return this;
        }

        public CreateTableBuilder TextColumn(string name, string defaultValue)
        {
            AddTextColumn(name, defaultValue);
            return this;
        }

        public CreateTableBuilder TextColumn(string name, DateTime defaultValue)
        {
            AddTextColumn(name, defaultValue);
            return this;
        }

        public override string ToString()
        {
            return builder.ToString() + ");";
        }
    }
}
