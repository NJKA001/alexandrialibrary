using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Repositories
{
    public class SelectCommandBuilder : CommandBuilder
    {
        public SelectCommandBuilder(Type type, string whereClause)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            var table = type.GetTableAttribute();
            if (table == null)
                throw new ArgumentException("type is not defined as a persistent table");

            var defaultSort = type.GetDefaultSortAttribute();
            var orderByClause = defaultSort != null ? defaultSort.Expression : string.Empty;

            AddStatement(new SelectStatementBuilder(table, whereClause, orderByClause));

            foreach (var oneToMany in type.GetOneToManyAttributes())
            {
                if (oneToMany.HasForeignKey)
                {
                    var oneToManyOrderByClause = (oneToMany.HasSequence) ? string.Format("{0}.{1} ASC, {0}.{2} ASC", oneToMany.TableName, oneToMany.ForeignKeyName, oneToMany.SequenceName) : string.Format("{0}.{1} ASC", oneToMany.TableName, oneToMany.ForeignKeyName);

                    AddStatement(new SelectStatementBuilder(table.Name, oneToMany.TableName, oneToMany.ForeignKeyName, whereClause, oneToManyOrderByClause));
                }
            }
        }
    }
}
