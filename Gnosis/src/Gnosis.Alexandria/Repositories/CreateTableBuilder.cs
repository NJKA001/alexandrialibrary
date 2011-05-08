using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
    public class CreateTableBuilder : IStatementBuilder
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

        private void AddBlobColumn(string name, object defaultValue)
        {
            AppendPrefix();

            builder.AppendFormat("{0} BLOB NOT NULL DEFAULT 0", name);

            hasColumns = true;
        }

        private void AddIntegerColumn(string name, object defaultValue)
        {
            AppendPrefix();

            builder.AppendFormat("{0} INTEGER NOT NULL DEFAULT {1}", name, defaultValue);

            hasColumns = true;
        }

        private void AddRealColumn(string name, object defaultValue)
        {
            AppendPrefix();

            builder.AppendFormat("{0} REAL NOT NULL DEFAULT {1}", name, defaultValue);

            hasColumns = true;
        }

        private void AddTextColumn(string name, object defaultValue)
        {
            AppendPrefix();

            var defaultString = string.Empty;
            if (defaultValue != null)
            {
                if (defaultValue is DateTime)
                    defaultString = ((DateTime)defaultValue).ToString("s");
                else
                    defaultString = defaultValue.ToString();
            }


            builder.AppendFormat("{0} TEXT NOT NULL DEFAULT '{1}'", name, defaultString);

            hasColumns = true;
        }

        public CreateTableBuilder PrimaryKeyInteger(string name)
        {
            AppendPrefix();

            builder.AppendFormat("{0} INTEGER PRIMARY KEY", name);

            hasColumns = true;

            return this;
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

        public CreateTableBuilder BlobColumn(string name, object defaultValue)
        {
            AddBlobColumn(name, defaultValue);
            return this;
        }

        public CreateTableBuilder IntegerColumn(string name, object defaultValue)
        {
            AddIntegerColumn(name, defaultValue);
            return this;
        }

        public CreateTableBuilder RealColumn(string name, object defaultValue)
        {
            AddRealColumn(name, defaultValue);
            return this;
        }

        public CreateTableBuilder TextColumn(string name, object defaultValue)
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
