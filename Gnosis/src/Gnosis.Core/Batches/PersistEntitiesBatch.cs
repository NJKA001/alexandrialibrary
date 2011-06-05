using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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

        private void AddDeleteStatements(IEntity entity, EntityInfo entityInfo)
        {
            foreach (var valueInfo in entityInfo.Values)
            {
                var builder = new CommandBuilder();

                foreach (var value in entity.GetValues(valueInfo))
                {
                    var idParameterName = builder.GetParameterName();
                    builder.AddParameter(idParameterName, value.Id);
                    var whereClause = string.Format("{0}.Id = {3}", valueInfo.Name, idParameterName);
                    var statement = new DeleteStatement(valueInfo.Name, whereClause);
                    builder.AddStatement(statement);
                }

                Add(builder);
            }

            foreach (var childInfo in entityInfo.Children)
            {
                var builder = new CommandBuilder();

                foreach (var child in entity.GetChildren(childInfo))
                {
                    var idParameterName = builder.GetParameterName();
                    builder.AddParameter(idParameterName, child.Id);
                    var whereClause = string.Format("{0}.Id = {3}", childInfo.Entity.Name, idParameterName);
                    var statement = new DeleteStatement(childInfo.Entity.Name, whereClause);
                    builder.AddStatement(statement);

                    AddDeleteStatements(child, childInfo.Entity);
                }

                Add(builder);
            }
        }

        #region Entity Delete

        protected void AddEntityDeleteStatement(IEntity entity, EntityInfo entityInfo)
        {
            var builder = new CommandBuilder();

            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, entity.Id);
            var whereClause = string.Format("{0}.Id = {1}", entityInfo.Name, idParameterName);
            var statement = new DeleteStatement(entityInfo.Name, whereClause);

            builder.AddStatement(statement);
            Add(builder);

            AddDeleteStatements(entity, entityInfo);
        }

        protected void AddEntityDeleteStatement(IChild child, ChildInfo childInfo)
        {
            var builder = new CommandBuilder();

            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, child.Id);
            var whereClause = string.Format("{0}.Id = {1}", childInfo.Entity.Name, idParameterName);
            var statement = new DeleteStatement(childInfo.Entity.Name, whereClause);

            builder.AddStatement(statement);
            Add(builder);

            AddDeleteStatements(child, childInfo.Entity);
        }

        #endregion

        #region Value Delete

        protected void AddValueDeleteStatement(IValue value, ValueInfo valueInfo)
        {
            var builder = new CommandBuilder();

            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, value.Id);
            var whereClause = string.Format("{0}.{1} = {2}", valueInfo.Name, valueInfo.Identifer.Name, idParameterName);
            var statement = new DeleteStatement(valueInfo.Name, whereClause);

            builder.AddStatement(statement);
            Add(builder);
        }

        #endregion
    }
}
