using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Core.Commands
{
    public class SelectStatement : IStatement
    {
        public SelectStatement(EntityInfo entityInfo, IFilter filter)
        {
            builder.AppendFormat("select {0}.* from {0}", entityInfo.Name);
            if (!string.IsNullOrEmpty(filter.WhereClause))
                builder.AppendFormat(" where {0}", filter.WhereClause);
            if (!string.IsNullOrEmpty(filter.OrderByClause))
                builder.AppendFormat(" order by {0}", filter.OrderByClause);
            builder.Append(";");
        }

        public SelectStatement(ChildInfo childInfo, IFilter filter)
        {
            builder.AppendFormat("select {0}.* from {0}", childInfo.Name);

            var previousName = childInfo.Name;
            var parent = childInfo.Parent;
            while (parent != null)
            {
                builder.AppendFormat(" inner join {0} on {0}.Id = {1}.Parent", parent.Name, previousName);
                previousName = parent.Name;
                parent = parent.Parent;
            }

            if (!string.IsNullOrEmpty(filter.WhereClause))
                builder.AppendFormat(" where {0}", filter.WhereClause);

            if (childInfo.Sequence != null)
                builder.AppendFormat(" order by {0}.{1}", childInfo.Name, childInfo.Sequence.Name);
        }

        public SelectStatement(ValueInfo valueInfo, IFilter filter)
        {
            builder.AppendFormat("select {0}.* from {0}", valueInfo.Name);

            var previousName = valueInfo.Name;
            var parent = valueInfo.Parent;
            while (parent != null)
            {
                builder.AppendFormat(" inner join {0} on {0}.Id = {1}.Parent", parent.Name, previousName);
                previousName = parent.Name;
                parent = parent.Parent;
            }

            if (!string.IsNullOrEmpty(filter.WhereClause))
                builder.AppendFormat(" where {0}", filter.WhereClause);

            if (valueInfo.Sequence != null)
                builder.AppendFormat(" order by {0}.{1}", valueInfo.Name, valueInfo.Sequence.Name);
        }

        //public SelectStatement(string parentTableName, string tableName, string foreignKeyColumnName, string whereClause, string orderByClause)
        //{
        //    builder.AppendFormat("select {0}.* from {0}", tableName);
        //    builder.AppendFormat(" inner join {0} on {0}.Id = {1}.{2}", parentTableName, tableName, foreignKeyColumnName);
        //    if (!string.IsNullOrEmpty(whereClause))
        //        builder.AppendFormat(" where {0}", whereClause);
        //    if (!string.IsNullOrEmpty(orderByClause))
        //        builder.AppendFormat(" order by {0}", orderByClause);
        //    builder.Append(";");
        //}

        private readonly StringBuilder builder = new StringBuilder();

        public override string ToString()
        {
            return builder.ToString();
        }
    }
}
