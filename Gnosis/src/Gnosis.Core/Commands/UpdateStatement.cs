using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Commands
{
    public class UpdateStatement : IStatement
    {
        public UpdateStatement(string tableName, string whereClause)
        {
            this.tableName = tableName;
            this.whereClause = whereClause;
        }

        private readonly string tableName;
        private readonly StringBuilder builder = new StringBuilder();
        private readonly string whereClause;

        private void AppendPrefix()
        {
            if (builder.Length > 0)
                builder.Append(", ");
        }

        public void Set(string columnName, string parameterName)
        {
            AppendPrefix();

            builder.AppendFormat("{0} = {1}", columnName, parameterName);
        }

        public override string ToString()
        {
            return string.Format("update {0} set {1} where {2};", tableName, builder, whereClause);
        }
    }
}
