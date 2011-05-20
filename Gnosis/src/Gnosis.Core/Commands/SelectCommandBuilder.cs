using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Core.Commands
{
    public class SelectCommandBuilder : CommandBuilder
    {
        public SelectCommandBuilder(Type type, string whereClause)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            var table = type.GetTableInfo();
            if (table == null)
                throw new ArgumentException("type is not defined as a persistent table");

            //var defaultSort = type.GetDefaultSortAttribute();
            //var orderByClause = !string.IsNullOrEmpty(table.DefaultSort) ? tab : string.Empty;

            AddStatement(new SelectStatement(table, whereClause, table.DefaultSort));

            foreach (var oneToMany in type.GetOneToManyAttributes())
            {
                if (oneToMany.Item1.HasForeignKey)
                {
                    var oneToManyOrderByClause = (oneToMany.Item1.HasSequence) ? string.Format("{0}.{1} ASC, {0}.{2} ASC", oneToMany.Item1.TableName, oneToMany.Item1.ForeignKeyName, oneToMany.Item1.SequenceName) : string.Format("{0}.{1} ASC", oneToMany.Item1.TableName, oneToMany.Item1.ForeignKeyName);

                    AddStatement(new SelectStatement(table.Name, oneToMany.Item1.TableName, oneToMany.Item1.ForeignKeyName, whereClause, oneToManyOrderByClause));
                }
            }
        }
    }
}
