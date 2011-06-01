using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core.Commands;
using Gnosis.Core.Collections;

namespace Gnosis.Core.Batches
{
    public class SaveEntitiesBatch<T>
        : PersistEntitiesBatch 
        where T : IEntity
    {
        public SaveEntitiesBatch(Func<IDbConnection> getConnection, IEnumerable<T> entities)
            : base(getConnection)
        {
            var table = typeof(T).GetTableInfo();

            foreach (var entity in entities)
            {
                if (entity.IsNew())
                {
                    AddEntityInsertStatement(entity, table);
                }
                else if (entity.IsChanged())
                {
                    AddEntityUpdateStatement(entity, table);
                }

                foreach (var childInfo in table.Children)
                {
                    AddChildSaveStatements(childInfo, entity);
                }
            }
        }

        #region Entity Insert

        private void AddEntityInsertStatement(IEntity entity, TableInfo table)
        {
            var builder = new CommandBuilder();
            var statement = new InsertStatement(table.Name);

            foreach (var column in table.Columns)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(column.Name, parameterName);
                builder.AddParameter(parameterName, column.GetValue(entity));
            }

            foreach (var customDataType in table.CustomDataTypes)
            {
                var dataTypeValue = customDataType.GetValue(entity);

                foreach (var column in customDataType.Columns)
                {
                    var parameterName = builder.GetParameterName();
                    statement.Add(column.Name, parameterName);
                    builder.AddParameter(parameterName, column.GetValue(dataTypeValue));
                }
            }

            builder.AddStatement(statement);
            Add(builder);
        }

        private void AddEntityInsertStatement(IEntity entity, ChildInfo childInfo, CollectionItemInfo itemInfo, IEntity parent)
        {
            var builder = new CommandBuilder();
            var statement = new InsertStatement(childInfo.TableName);

            if (childInfo.PrimaryKey != null)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(childInfo.PrimaryKey.Name, parameterName);
                builder.AddParameter(parameterName, itemInfo.Id);
            }

            if (childInfo.ForeignKey != null)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(childInfo.ForeignKey.Name, parameterName);
                builder.AddParameter(parameterName, parent.Id);
            }

            if (childInfo.Sequence != null)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(childInfo.Sequence.Name, parameterName);
                builder.AddParameter(parameterName, itemInfo.Sequence);
            }

            foreach (var column in childInfo.BaseTable.Columns)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(column.Name, parameterName);
                builder.AddParameter(parameterName, column.GetValue(itemInfo.Item));
            }

            foreach (var customDataType in childInfo.BaseTable.CustomDataTypes)
            {
                foreach (var column in customDataType.Columns)
                {
                    var parameterName = builder.GetParameterName();
                    statement.Add(column.Name, parameterName);
                    builder.AddParameter(parameterName, column.GetValue(itemInfo.Item));
                }
            }

            builder.AddStatement(statement);
            Add(builder);
        }

        #endregion

        #region Entity Update

        private void AddEntityUpdateStatement(IEntity entity, TableInfo table)
        {
            var builder = new CommandBuilder();
            var idParameterName = builder.GetParameterName();
            var whereClause = string.Format("{0}.Id = {1}", table.Name, idParameterName);
            var statement = new UpdateStatement(table.Name, whereClause);
            builder.AddParameter(idParameterName, entity.Id);

            foreach (var column in table.Columns.Where(x => x.IsReadOnly == false))
            {
                var parameterName = builder.GetParameterName();
                statement.Set(column.Name, parameterName);
                builder.AddParameter(parameterName, column.GetValue(entity));
            }

            foreach (var customDataType in table.CustomDataTypes)
            {
                var dataTypeValue = customDataType.GetValue(entity);

                foreach (var column in customDataType.Columns)
                {
                    var parameterName = builder.GetParameterName();
                    statement.Set(column.Name, parameterName);
                    builder.AddParameter(parameterName, column.GetValue(dataTypeValue));
                }
            }

            builder.AddStatement(statement);
            Add(builder);
        }

        private void AddEntityUpdateStatement(IEntity entity, ChildInfo childInfo, CollectionItemInfo itemInfo, IEntity parent)
        {
            var builder = new CommandBuilder();

            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, entity.Id);
            var whereClause = string.Format("{0}.Id = {1}", childInfo.TableName, idParameterName);
            var statement = new UpdateStatement(childInfo.TableName, whereClause);

            if (childInfo.Sequence != null)
            {
                var parameterName = builder.GetParameterName();
                statement.Set(childInfo.Sequence.Name, parameterName);
                builder.AddParameter(parameterName, itemInfo.Sequence);
            }

            foreach (var column in childInfo.BaseTable.Columns)
            {
                var parameterName = builder.GetParameterName();
                statement.Set(column.Name, parameterName);
                builder.AddParameter(parameterName, column.GetValue(itemInfo.Item));
            }

            foreach (var customDataType in childInfo.BaseTable.CustomDataTypes)
            {
                foreach (var column in customDataType.Columns)
                {
                    var parameterName = builder.GetParameterName();
                    statement.Set(column.Name, parameterName);
                    builder.AddParameter(parameterName, column.GetValue(itemInfo.Item));
                }
            }

            builder.AddStatement(statement);
            Add(builder);
        }

        #endregion

        #region Value Insert

        private void AddValueInsertStatement(IValue value, ChildInfo childInfo, CollectionItemInfo itemInfo, IEntity parent)
        {
            var builder = new CommandBuilder();
            var statement = new InsertStatement(childInfo.TableName);

            if (childInfo.PrimaryKey != null)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(childInfo.PrimaryKey.Name, parameterName);
                builder.AddParameter(parameterName, itemInfo.Id);
            }

            if (childInfo.ForeignKey != null)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(childInfo.ForeignKey.Name, parameterName);
                builder.AddParameter(parameterName, parent.Id);
            }

            if (childInfo.Sequence != null)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(childInfo.Sequence.Name, parameterName);
                builder.AddParameter(parameterName, itemInfo.Sequence);
            }

            builder.AddStatement(statement);
            Add(builder);
        }

        #endregion

        #region Value Move

        private void AddValueMoveStatement(IValue value, ChildInfo childInfo, CollectionItemInfo itemInfo, IEntity parent)
        {
            var builder = new CommandBuilder();

            var parentParameterName = builder.GetParameterName();
            builder.AddParameter(parentParameterName, parent.Id);
            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, itemInfo.Id);
            var whereClause = string.Format("{0}.{1} = {2} and {0}.{3} = {4}", childInfo.TableName, childInfo.ForeignKey.Name, parent.Id, childInfo.PrimaryKey.Name, itemInfo.Id);
            var statement = new UpdateStatement(childInfo.TableName, whereClause);

            var sequenceParameterName = builder.GetParameterName();
            statement.Set(childInfo.Sequence.Name, sequenceParameterName);
            builder.AddParameter(sequenceParameterName, itemInfo.Sequence);

            builder.AddStatement(statement);
            Add(builder);
        }

        #endregion

        #region Child Entity

        private void AddChildSaveStatements(ChildInfo childInfo, IEntity parent)
        {
            var itemInfos = childInfo.GetItemInfo(parent);
            if (childInfo.BaseType.IsEntityType())
            {
                IEntity entity = null;
                foreach (var itemInfo in itemInfos)
                {
                    entity = itemInfo.Item as IEntity;

                    switch (itemInfo.State)
                    {
                        case Collections.CollectionItemState.Added:
                            AddEntityInsertStatement(entity, childInfo, itemInfo, parent);
                            break;
                        case Collections.CollectionItemState.Removed:
                            AddEntityDeleteStatement(entity, childInfo, itemInfo, parent);
                            break;
                        case Collections.CollectionItemState.Existing:
                            if (entity.IsChanged())
                            {
                                AddEntityUpdateStatement(entity, childInfo, itemInfo, parent);
                            }
                            break;
                        case Collections.CollectionItemState.Moved:
                            AddEntityUpdateStatement(entity, childInfo, itemInfo, parent);
                            break;
                        default:
                            break;
                    }

                    foreach (var grandchildInfo in childInfo.BaseTable.Children)
                    {
                        AddChildSaveStatements(grandchildInfo, entity);
                    }
                }
            }
            else
            {
                IValue value = null;

                foreach (var itemInfo in itemInfos)
                {
                    value = itemInfo.Item as IValue;

                    switch (itemInfo.State)
                    {
                        case Collections.CollectionItemState.Added:
                            AddValueInsertStatement(value, childInfo, itemInfo, parent);
                            break;
                        case Collections.CollectionItemState.Removed:
                            AddValueDeleteStatement(value, childInfo, itemInfo, parent);
                            break;
                        case Collections.CollectionItemState.Existing:
                            break;
                        case Collections.CollectionItemState.Moved:
                            AddValueMoveStatement(value, childInfo, itemInfo, parent);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        #endregion
    }
}
