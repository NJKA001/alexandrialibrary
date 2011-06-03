using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core.Collections;
using Gnosis.Core.Commands;

namespace Gnosis.Core.Batches
{
    public abstract class PersistEntitiesBatch
        : Batch
    {
        protected PersistEntitiesBatch(Func<IDbConnection> getConnection)
            : base(getConnection)
        {
        }

        #region Entity Delete

        protected void AddEntityDeleteStatement(IEntity entity, TableInfo table)
        {
            var builder = new CommandBuilder(table.Name);

            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, entity.Id);
            var whereClause = string.Format("{0}.Id = {1}", table.Name, idParameterName);
            var statement = new DeleteStatement(table.Name, whereClause);

            builder.AddStatement(statement);
            Add(builder);
        }

        protected void AddEntityDeleteStatement(IEntity entity, ChildInfo childInfo, CollectionItemInfo itemInfo, IEntity parent)
        {
            var builder = new CommandBuilder(childInfo.TableName);

            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, entity.Id);
            var whereClause = string.Format("{0}.Id = {1}", childInfo.TableName, idParameterName);
            var statement = new DeleteStatement(childInfo.TableName, whereClause);

            builder.AddStatement(statement);
            Add(builder);

            foreach (var grandchildInfo in childInfo.BaseTable.Children)
            {
                AddChildDeleteStatements(grandchildInfo, entity);
            }
        }

        #endregion

        #region Value Delete

        protected void AddValueDeleteStatement(IValue value, ChildInfo childInfo, CollectionItemInfo itemInfo, IEntity parent)
        {
            var builder = new CommandBuilder();

            var parentParameterName = builder.GetParameterName();
            builder.AddParameter(parentParameterName, parent.Id);
            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, value.Id);
            var whereClause = string.Format("{0}.{1} = {2} and {0}.Id = {3}", childInfo.TableName, childInfo.ForeignKey.Name, parentParameterName, idParameterName);
            var statement = new DeleteStatement(childInfo.TableName, whereClause);

            builder.AddStatement(statement);
            Add(builder);
        }

        #endregion

        #region Child Entity

        protected void AddChildDeleteStatements(ChildInfo childInfo, IEntity parent)
        {
            var itemInfos = childInfo.GetItemInfo(parent);
            if (childInfo.BaseType.IsEntityType())
            {
                IEntity entity = null;
                foreach (var itemInfo in itemInfos)
                {
                    entity = itemInfo.Item as IEntity;
                    AddEntityDeleteStatement(entity, childInfo, itemInfo, parent);

                    foreach (var grandchildInfo in childInfo.BaseTable.Children)
                    {
                        AddChildDeleteStatements(grandchildInfo, entity);
                    }
                }
            }
            else
            {
                IValue value = null;
                foreach (var itemInfo in itemInfos)
                {
                    value = itemInfo.Item as IValue;
                    AddValueDeleteStatement(value, childInfo, itemInfo, parent);
                }
            }
        }

        #endregion
    }
}
