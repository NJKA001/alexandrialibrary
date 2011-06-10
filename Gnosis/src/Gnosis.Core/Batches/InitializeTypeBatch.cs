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
        public InitializeTypeBatch(IDbConnection connection, ILogger logger, Type type, IEnumerable<ILookup> lookups, IEnumerable<ISearch> searches)
            : base(connection, logger)
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
            foreach (var lookup in lookups)
            {
                var builder = new CommandBuilder();
                //var tableName = new EntityInfo(lookup.BaseType).Name;
                //var indexName = string.Format("{0}_{1}", tableName, lookup.Name);
                //builder.AddStatement(new CreateIndexStatement(tableName, indexName, true, lookup.Columns));
                builder.AddStatement(new CreateIndexStatement(lookup));
                Add(builder);
            }
        }

        private void AddSearchIndices(IEnumerable<ISearch> searches)
        {
            foreach (var search in searches)
            {
                var builder = new CommandBuilder();
                //var tableName = new EntityInfo(search.BaseType).Name;
                //var indexName = string.Format("{0}_{1}", tableName, search.Name);
                //builder.AddStatement(new CreateIndexStatement(search.SourceName, search.Name, false, search.Columns));
                builder.AddStatement(new CreateIndexStatement(search));
                Add(builder);
            }
        }
    }
}
