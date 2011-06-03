using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Core.Commands
{
    public class SelectStatement : IStatement
    {
        public SelectStatement(EntityInfo entityInfo, string whereClause, string orderByClause)
        {
            builder.AppendFormat("select '{0}', {0}.* from {0}", entityInfo.Name);
            if (!string.IsNullOrEmpty(whereClause))
                builder.AppendFormat(" where {0}", whereClause);
            if (!string.IsNullOrEmpty(orderByClause))
                builder.AppendFormat(" order by {0}", orderByClause);
            builder.Append(";");
        }

        public SelectStatement(string parentTableName, string tableName, string foreignKeyColumnName, string whereClause, string orderByClause)
        {
            builder.AppendFormat("select '{0}', {0}.* from {0}", tableName);
            builder.AppendFormat(" inner join {0} on {0}.Id = {1}.{2}", parentTableName, tableName, foreignKeyColumnName);
            if (!string.IsNullOrEmpty(whereClause))
                builder.AppendFormat(" where {0}", whereClause);
            if (!string.IsNullOrEmpty(orderByClause))
                builder.AppendFormat(" order by {0}", orderByClause);
            builder.Append(";");
        }

        private readonly StringBuilder builder = new StringBuilder();

        public override string ToString()
        {
            return builder.ToString();
        }
    }
}
