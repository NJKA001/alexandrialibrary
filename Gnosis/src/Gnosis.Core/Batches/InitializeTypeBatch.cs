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
            var entityInfo = new EntityInfo(type);

            AddRootCommandBuilder(entityInfo);
            AddCommandBuilders(entityInfo);
            AddLookupIndices(lookups);
            AddSearchIndices(searches);
        }

        private void AddRootCommandBuilder(EntityInfo entityInfo)
        {
            var builder = new CommandBuilder();
            builder.AddStatement(new CreateTableStatement(entityInfo));
            
            Add(builder);
        }

        private void AddCommandBuilders(EntityInfo entityInfo)
        {
            foreach (var valueInfo in entityInfo.Values)
            {
                var builder = new CommandBuilder();
                builder.AddStatement(new CreateTableStatement(valueInfo));

                Add(builder);
            }

            foreach (var childInfo in entityInfo.Children)
            {
                var builder = new CommandBuilder();
                builder.AddStatement(new CreateTableStatement(childInfo));

                Add(builder);

                AddCommandBuilders(childInfo);
            }
        }

        private void AddLookupIndices(IEnumerable<ILookup> lookups)
        {
            var builder = new CommandBuilder();
            foreach (var lookup in lookups)
            {
                var tableName = new EntityInfo(lookup.BaseType).Name;
                var indexName = string.Format("{0}_{1}_index", tableName, lookup.Name);
                builder.AddStatement(new CreateIndexStatement(tableName, indexName, true, lookup.Columns));
            }
            Add(builder);
        }

        private void AddSearchIndices(IEnumerable<ISearch> searches)
        {
            var builder = new CommandBuilder();
            foreach (var search in searches)
            {
                var tableName = new EntityInfo(search.BaseType).Name;
                var indexName = string.Format("{0}_{1}_index", tableName, search.Name);
                builder.AddStatement(new CreateIndexStatement(tableName, indexName, false, search.Columns));
            }
            Add(builder);
        }
    }
}
