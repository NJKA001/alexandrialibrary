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
            if (entityInfo.IsRoot)
            {
                builder.AppendFormat("select {0}.* from {0}", entityInfo.Name);
                if (!string.IsNullOrEmpty(filter.JoinClause))
                    builder.AppendFormat(" {0}", filter.JoinClause);
                if (!string.IsNullOrEmpty(filter.WhereClause))
                    builder.AppendFormat(" where {0}", filter.WhereClause.Trim());
                if (!string.IsNullOrEmpty(filter.OrderByClause))
                    builder.AppendFormat(" order by {0}", filter.OrderByClause);
                builder.Append(";");
            }
            else
            {
                builder.AppendFormat("select {0}.* from {0}", entityInfo.Name);

                var previousName = entityInfo.Name;
                var parent = entityInfo.Parent;
                if (filter.AutoJoin)
                {
                    while (parent != null)
                    {
                        builder.AppendFormat(" inner join {0} on {0}.Id = {1}.Parent", parent.Name, previousName);
                        previousName = parent.Name;
                        parent = parent.Parent;
                    }
                }

                if (!string.IsNullOrEmpty(filter.JoinClause))
                    builder.AppendFormat(" {0}", filter.JoinClause);

                if (!string.IsNullOrEmpty(filter.WhereClause))
                    builder.AppendFormat(" where {0}", filter.WhereClause.Trim());

                if (entityInfo.Sequence != null)
                    builder.AppendFormat(" order by {0}.{1}", entityInfo.Name, entityInfo.Sequence.Name);
            }
        }

        public SelectStatement(ValueInfo valueInfo, IFilter filter)
        {
            builder.AppendFormat("select {0}.* from {0}", valueInfo.Name);

            var previousName = valueInfo.Name;
            var parent = valueInfo.Parent;
            if (filter.AutoJoin)
            {
                while (parent != null)
                {
                    builder.AppendFormat(" inner join {0} on {0}.Id = {1}.Parent", parent.Name, previousName);
                    previousName = parent.Name;
                    parent = parent.Parent;
                }
            }
            if (!string.IsNullOrEmpty(filter.JoinClause))
                builder.AppendFormat(" {0}", filter.JoinClause);

            if (!string.IsNullOrEmpty(filter.WhereClause))
                builder.AppendFormat(" where {0}", filter.WhereClause.Trim());

            if (valueInfo.Sequence != null)
                builder.AppendFormat(" order by {0}.{1}", valueInfo.Name, valueInfo.Sequence.Name);
        }

        private readonly StringBuilder builder = new StringBuilder();

        public override string ToString()
        {
            return builder.ToString();
        }
    }
}
