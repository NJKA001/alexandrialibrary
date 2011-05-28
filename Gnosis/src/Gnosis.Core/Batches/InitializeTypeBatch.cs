using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core.Commands;

namespace Gnosis.Core.Batches
{
    public class InitializeTypeBatch : Batch
    {
        public InitializeTypeBatch(Func<IDbConnection> getConnection, Type type, IEnumerable<ILookup> lookups, IEnumerable<ISearch> searches)
            : base(getConnection)
        {
            var tableInfo = type.GetTableInfo();

            AddRootCommandBuilder(tableInfo);
            AddChildrenCommandBuilders(tableInfo.Children);
            AddLookupIndices(lookups);
            AddSearchIndices(searches);
        }

        private void AddRootCommandBuilder(TableInfo tableInfo)
        {
            var builder = new CommandBuilder();
            builder.AddStatement(new CreateTableStatement(tableInfo));
            
            Add(builder);
        }

        private void AddChildrenCommandBuilders(IEnumerable<ChildInfo> children)
        {
            foreach (var childInfo in children)
            {
                var builder = new CommandBuilder();
                builder.AddStatement(new CreateTableStatement(childInfo));

                Add(builder);

                AddChildrenCommandBuilders(childInfo.BaseTable.Children);
            }
        }

        private void AddLookupIndices(IEnumerable<ILookup> lookups)
        {
            var builder = new CommandBuilder();
            foreach (var lookup in lookups)
            {
                var tableName = lookup.BaseType.GetTableName();
                var indexName = string.Format("{0}_{1}_ux", tableName, lookup.Name);
                builder.AddStatement(new CreateIndexStatement(tableName, indexName, true, lookup.Columns));
            }
            Add(builder);
        }

        private void AddSearchIndices(IEnumerable<ISearch> searches)
        {
            var builder = new CommandBuilder();
            foreach (var search in searches)
            {
                var tableName = search.BaseType.GetTableName();
                var indexName = string.Format("{0}_{1}_ux", tableName, search.Name);
                builder.AddStatement(new CreateIndexStatement(tableName, indexName, false, search.Columns));
            }
            Add(builder);
        }
    }
}
