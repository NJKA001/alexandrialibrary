using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Gnosis.Data.Commands;

namespace Gnosis.Data.Queries
{
    public class ReverseQuery<TParent, TValue>
        : IQuery<TParent>
        where TParent : IEntity
        where TValue : IValue
    {
        public ReverseQuery(IFactory factory, IFilter filter, Expression<Func<TParent, object>> property)
        {
            var parent = new EntityInfo(typeof(TParent));
            var valueType = typeof(TValue);
            var valueInfo = new ValueInfo(parent, property.ToPropertyInfo(), valueType);

            //this.logger = logger;
            this.factory = factory;
            this.valueBuilder = new CommandBuilder(valueInfo.Name, valueInfo.Type);

            rootName = "T_" + Guid.NewGuid().ToString().Replace("-", string.Empty);
            var selectStatement = new SelectStatement(valueInfo, filter, parent, rootIdAlias);
            var createTableStatement = new CreateTableStatement(rootName, selectStatement);
            valueBuilder.AddStatement(createTableStatement);
            foreach (var parameter in filter.Parameters)
            {
                valueBuilder.AddParameter(parameter);
            }

            builder = new CommandBuilder(parent.Name, parent.Type);
            builder.AddStatement(new SelectStatement(parent, rootName, rootIdAlias));

            AddChildStatements(builder, parent);
        }

        private readonly IFactory factory;
        private readonly ICommandBuilder valueBuilder;
        private readonly ICommandBuilder builder;
        private readonly string rootName;
        const string rootIdAlias = "Root_Id";

        #region Private Methods

        private void AddChildStatements(ICommandBuilder parentBuilder, EntityInfo entityInfo)
        {
            foreach (var childInfo in entityInfo.Children)
            {
                var childBuilder = new CommandBuilder(childInfo.Name, childInfo.Type);
                childBuilder.AddStatement(new SelectStatement(childInfo, rootName, rootIdAlias));

                parentBuilder.AddChild(childBuilder);

                AddChildStatements(childBuilder, childInfo);
            }

            foreach (var valueInfo in entityInfo.Values)
            {
                var valueBuilder = new CommandBuilder(valueInfo.Name, valueInfo.Type);
                valueBuilder.AddStatement(new SelectStatement(valueInfo, rootName, rootIdAlias));

                parentBuilder.AddChild(valueBuilder);
            }
        }

        private void AddChildren(IDbConnection connection, ICommandBuilder parentBuilder, IEnumerable<IEntity> parents)
        {
            //logger.Info("  ReverseLookupQuery.AddChildren");
            foreach (var childBuilder in parentBuilder.Children)
            {
                if (childBuilder.Type == null)
                    continue;

                var isChildType = childBuilder.Type.IsChildType();

                var children = new Dictionary<Guid, IList<IChild>>();
                var values = new Dictionary<Guid, IList<IValue>>();
                var allChildren = new List<IEntity>();

                using (var command = childBuilder.GetCommand(connection))
                {
                    //logger.Debug("    " + command.CommandText.Trim());
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (isChildType)
                            {
                                var child = factory.CreateChild(childBuilder.Type, reader);

                                if (!children.ContainsKey(child.Parent))
                                    children.Add(child.Parent, new List<IChild>());

                                children[child.Parent].Add(child);
                                allChildren.Add(child);
                            }
                            else
                            {
                                var value = factory.CreateValue(childBuilder.Type, reader);

                                if (!values.ContainsKey(value.Parent))
                                    values.Add(value.Parent, new List<IValue>());

                                values[value.Parent].Add(value);
                            }
                        }
                    }
                }

                if (isChildType)
                {
                    foreach (var parent in parents)
                    {
                        if (children.ContainsKey(parent.Id))
                        {
                            var theseChildren = children[parent.Id];
                            parent.InitializeChildren(childBuilder.Name, theseChildren);
                        }
                    }

                    AddChildren(connection, childBuilder, allChildren);
                }
                else
                {
                    foreach (var parent in parents)
                    {
                        if (values.ContainsKey(parent.Id))
                        {
                            var theseValues = values[parent.Id];
                            parent.InitializeValues(childBuilder.Name, theseValues);
                        }
                    }
                }
            }
        }

        #endregion

        #region IQuery Members

        public IEnumerable<TParent> Execute(IDbConnection connection)
        {
            //logger.Info("ReverseLookupQuery.Execute");

            var createTempTable = valueBuilder.GetCommand(connection);
            createTempTable.ExecuteNonQuery();

            var items = new List<TParent>();

            var command = builder.GetCommand(connection);
            //logger.Debug("    " + command.CommandText.Trim());
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = factory.CreateEntity<TParent>(reader);
                    if (item != null)
                    {
                        items.Add(item);
                    }
                }
            }

            AddChildren(connection, builder, items.Cast<IEntity>());

            //logger.Debug("  return items. count=" + items.Count);
            return items;
        }

        #endregion
    }
}
