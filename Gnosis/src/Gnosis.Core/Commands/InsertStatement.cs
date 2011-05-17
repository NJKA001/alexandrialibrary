using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Commands
{
    public class InsertStatement
        : IStatement
    {
        public InsertStatement(string tableName)
        {
            this.tableName = tableName;
        }

        private readonly string tableName;
        private readonly StringBuilder columnNames = new StringBuilder();
        private readonly StringBuilder parameterNames = new StringBuilder();

        private void AppendColumnPrefix()
        {
            if (columnNames.Length > 0)
                columnNames.Append(", ");
        }

        private void AppendParameterNamePrefix()
        {
            if (parameterNames.Length > 0)
                parameterNames.Append(", ");
        }

        public void Add(string columnName, string parameterName)
        {
            AppendColumnPrefix();
            columnNames.Append(columnName);

            AppendParameterNamePrefix();
            parameterNames.Append(parameterName);
        }

        public override string ToString()
        {
            return string.Format("insert into {0} ({1}) values ({2});", tableName, columnNames, parameterNames);
        }
    }
}
