using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data.Commands
{
    public class SelectStatement : IStatement
    {
        public SelectStatement(EntityInfo entityInfo, string rootName, string rootIdAlias)
            : this(entityInfo, rootName, rootIdAlias, null)
        {
        }

        public SelectStatement(EntityInfo entityInfo, string rootName, string rootIdAlias, string orderByClause)
        {
            if (entityInfo.IsRoot)
            {
                builder.AppendFormat("select distinct {0}.* from {0}", entityInfo.Name);
                if (!string.IsNullOrEmpty(rootName) && !string.IsNullOrEmpty(rootIdAlias))
                    builder.AppendFormat(" inner join {0} on {0}.{1} = {2}.{3}", rootName, rootIdAlias, entityInfo.Name, entityInfo.Identifier.Name);
                if (!string.IsNullOrEmpty(orderByClause))
                    builder.AppendFormat(" order by {0}", orderByClause);
                builder.Append(";");
            }
            else
            {
                builder.AppendFormat("select distinct {0}.* from {0}", entityInfo.Name);

                var previousName = entityInfo.Name;
                var previousIdName = entityInfo.Identifier.Name;
                var parent = entityInfo.Parent;
                while (parent != null)
                {
                    builder.AppendFormat(" inner join {0} on {0}.Id = {1}.Parent", parent.Name, previousName);
                    previousName = parent.Name;
                    previousIdName = parent.Identifier.Name;
                    parent = parent.Parent;
                }

                if (!string.IsNullOrEmpty(rootName) && !string.IsNullOrEmpty(rootIdAlias))
                    builder.AppendFormat(" inner join {0} on {0}.{1} = {2}.{3}", rootName, rootIdAlias, previousName, previousIdName);

                if (entityInfo.Sequence != null)
                    builder.AppendFormat(" order by {0}.{1}", entityInfo.Name, entityInfo.Sequence.Name);
            }
        }

        public SelectStatement(EntityInfo entityInfo, IFilter filter, string rootIdAlias)
        {
            if (entityInfo.IsRoot)
            {
                builder.AppendFormat("select distinct {0}.{1} {2} from {0}", entityInfo.Name, entityInfo.Identifier.Name, rootIdAlias);
                if (!string.IsNullOrEmpty(filter.JoinClause))
                    builder.AppendFormat(" {0}", filter.JoinClause);
                if (!string.IsNullOrEmpty(filter.WhereClause))
                    builder.AppendFormat(" where {0}", filter.WhereClause.Trim());
                if (!string.IsNullOrEmpty(filter.OrderByClause))
                    builder.AppendFormat(" order by {0}", filter.OrderByClause);
                builder.Append(";");
            }
        }

        public SelectStatement(EntityInfo entityInfo, IFilter filter)
        {
            if (entityInfo.IsRoot)
            {
                builder.AppendFormat("select distinct {0}.* from {0}", entityInfo.Name);
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
                builder.AppendFormat("select distinct {0}.* from {0}", entityInfo.Name);

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
            : this(valueInfo, filter, string.Format("select distinct {0}.*", valueInfo.Name))
        {
        }

        public SelectStatement(ValueInfo valueInfo, IFilter filter, EntityInfo rootInfo, string rootIdAlias)
        {
            builder.AppendFormat("select distinct {0}.{1} {2} from {3}", rootInfo.Name, rootInfo.Identifier.Name, rootIdAlias, valueInfo.Name);

            var previousName = valueInfo.Name;
            var parent = valueInfo.Parent;
            while (parent != null)
            {
                builder.AppendFormat(" inner join {0} on {0}.Id = {1}.Parent", parent.Name, previousName);
                previousName = parent.Name;
                parent = parent.Parent;
            }
            
            if (!string.IsNullOrEmpty(filter.WhereClause))
                builder.AppendFormat(" where {0}", filter.WhereClause.Trim());

            if (valueInfo.Sequence != null)
                builder.AppendFormat(" order by {0}.{1}", valueInfo.Name, valueInfo.Sequence.Name);

        }

        public SelectStatement(ValueInfo valueInfo, IFilter filter, string selectClause)
        {
            builder.Append(selectClause);

            builder.AppendFormat(" from {0}", valueInfo.Name);

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

        public SelectStatement(ValueInfo valueInfo, string rootName, string rootIdAlias)
        {
            builder.AppendFormat("select distinct {0}.* from {0}", valueInfo.Name);

            var previousName = valueInfo.Name;
            var previousIdName = valueInfo.Identifer.Name;
            var parent = valueInfo.Parent;
            while (parent != null)
            {
                builder.AppendFormat(" inner join {0} on {0}.Id = {1}.Parent", parent.Name, previousName);
                previousName = parent.Name;
                previousIdName = parent.Identifier.Name;
                parent = parent.Parent;
            }

            if (!string.IsNullOrEmpty(rootName) && !string.IsNullOrEmpty(rootIdAlias))
                builder.AppendFormat(" inner join {0} on {0}.{1} = {2}.{3}", rootName, rootIdAlias, previousName, previousIdName);

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
