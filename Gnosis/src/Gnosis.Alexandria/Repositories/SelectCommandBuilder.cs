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

            foreach (var tuple in type.GetOneToManyAttributes())
            {
                var property = tuple.Item1;
                var oneToMany = tuple.Item2;

                if (oneToMany.HasForeignKey)
                {
                    var oneToManyOrderByClause = (oneToMany.HasSequence) ? string.Format("{0}.{1} ASC, {0}.{2} ASC", oneToMany.TableName, oneToMany.ForeignKeyName, oneToMany.SequenceName) : string.Format("{0}.{1} ASC", oneToMany.TableName, oneToMany.ForeignKeyName);

                    AddStatement(new SelectStatementBuilder(table.Name, oneToMany.TableName, oneToMany.ForeignKeyName, whereClause, oneToManyOrderByClause));
                }
                else
                {
                    var args = property.PropertyType.GetGenericArguments();
                    if (args != null && args.Length > 0)
                    {
                        var itemType = args[0];

                    }
                    //Handle entity cases here
                    //oneToMany.
                }
            }
        }
    }
}
