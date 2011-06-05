using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Commands;

namespace Gnosis.Core.Queries
{
    public class Query<T>
        : IQuery<T> where T : IEntity
    {
        public Query(Func<IDbConnection> getConnection, IFactory factory, IFilter filter)
        {
            this.getConnection = getConnection;
            this.factory = factory;
            this.builder = new CommandBuilder();
            this.whereClause = filter.WhereClause;
            this.orderByClause = filter.OrderByClause;
            this.parameters = filter.Parameters;

            var entityInfo = new EntityInfo(typeof(T));
            builder.AddStatement(new SelectStatement(entityInfo, filter));
            AddChildStatements(builder, entityInfo, filter);
        }

        private readonly Func<IDbConnection> getConnection;
        private readonly IFactory factory;
        private readonly ICommandBuilder builder;
        private readonly string whereClause;
        private readonly string orderByClause;
        private readonly IEnumerable<KeyValuePair<string, object>> parameters;

        private void AddChildStatements(ICommandBuilder parentBuilder, EntityInfo entityInfo, IFilter filter)
        {
            foreach (var childInfo in entityInfo.Children)
            {
                var childBuilder = new CommandBuilder(childInfo.Name, childInfo.Type);
                childBuilder.AddStatement(new SelectStatement(childInfo, filter));
                parentBuilder.AddChild(childBuilder);

                AddChildStatements(childBuilder, childInfo, filter);
            }

            foreach (var valueInfo in entityInfo.Values)
            {
                var valueBuilder = new CommandBuilder(valueInfo.Name, valueInfo.Type);
                valueBuilder.AddStatement(new SelectStatement(valueInfo, filter));
                parentBuilder.AddChild(valueBuilder);
            }
        }

        private void AddChildren(IDbConnection connection, ICommandBuilder parentBuilder, IEnumerable<IEntity> parents)
        {
            foreach (var childBuilder in parentBuilder.Children)
            {
                if (childBuilder.Type == null)
                    continue;

                var isChild = childBuilder.Type.IsChildType();

                var children = new List<IChild>();
                var values = new List<IValue>();

                using (var command = childBuilder.GetCommand(connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (isChild)
                            {
                                var child = factory.CreateChild(childBuilder.Type, reader);
                                children.Add(child);
                            }
                            else
                            {
                                var value = factory.CreateValue(childBuilder.Type, reader);
                                values.Add(value);
                            }
                        }
                    }
                }

                if (isChild)
                {
                    factory.AddChildren(parentBuilder.Type, childBuilder.Type, childBuilder.Name, parents, children);
                    AddChildren(connection, childBuilder, children.Cast<IEntity>());
                }
                else
                {
                    factory.AddValues(parentBuilder.Type, childBuilder.Type, childBuilder.Name, parents, values);
                }
            }
        }

        public IEnumerable<T> Execute()
        {
            var items = new List<T>();

            using (var connection = getConnection())
            {
                connection.Open();

                var command = builder.GetCommand(connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = factory.CreateEntity<T>(reader);
                        if (item != null)
                        {
                            items.Add(item);
                        }
                    }
                }

                AddChildren(connection, builder, items.Cast<IEntity>());
            }

            return items;
        }
    }
}
