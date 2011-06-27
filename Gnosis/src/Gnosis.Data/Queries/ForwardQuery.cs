using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Data.Commands;

namespace Gnosis.Data.Queries
{
    public class ForwardQuery<T>
        : IQuery<T>
        where T : IEntity
    {
        public ForwardQuery(IFactory factory, IFilter filter)
        {
            var entityInfo = new EntityInfo(typeof(T));

            //this.logger = logger;
            this.factory = factory;
            this.setupBuilder = new CommandBuilder();

            rootName = "T_" + Guid.NewGuid().ToString().Replace("-", string.Empty);
            var selectStatement = new SelectStatement(entityInfo, filter, rootIdAlias);
            setupBuilder.AddStatement(new CreateTableStatement(rootName, selectStatement));
            foreach (var parameter in filter.Parameters)
            {
                setupBuilder.AddParameter(parameter);
            }

            this.builder = new CommandBuilder(entityInfo.Name, entityInfo.Type);
            builder.AddStatement(new SelectStatement(entityInfo, rootName, rootIdAlias));

            AddChildStatements(builder, entityInfo);
        }

        //private readonly ILogger logger;
        private readonly IFactory factory;
        private readonly ICommandBuilder setupBuilder;
        private readonly ICommandBuilder builder;
        private readonly string rootName;
        private const string rootIdAlias = "Root_Id";

        #region Private Members

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
            //logger.Info("  ForwardLookupQuery.AddChildren");
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

        public IEnumerable<T> Execute(IDbConnection connection)
        {
            //logger.Info("ForwardQuery.Execute");

            var createTempTable = setupBuilder.GetCommand(connection);
            //logger.Debug("    setup command: " + createTempTable.CommandText);
            createTempTable.ExecuteNonQuery();

            var items = new List<T>();

            var command = builder.GetCommand(connection);
            //logger.Debug("    query command: " + command.CommandText.Trim());
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

            //logger.Debug("  return items. count=" + items.Count);
            return items;
        }

        #endregion
    }
}
