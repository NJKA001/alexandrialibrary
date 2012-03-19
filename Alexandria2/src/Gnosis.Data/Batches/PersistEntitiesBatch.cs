using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Data.Commands;

namespace Gnosis.Data.Batches
{
    public abstract class PersistEntitiesBatch
        : Batch
    {
        protected PersistEntitiesBatch(IDbConnection connection)
            : base(connection)
        {
        }

        private void AddDeleteStatements(IEntity entity, EntityInfo entityInfo)
        {
            foreach (var valueInfo in entityInfo.Values)
            {
                foreach (var value in entity.GetValues(valueInfo))
                {
                    var builder = new ComplexCommandBuilder();
                    var idParameter = value.Id.ToParameter();
                    builder.AddParameter(idParameter);
                    var whereClause = string.Format("{0}.{1} = {2}", valueInfo.Name, valueInfo.Identifer.Name, idParameter.Name);
                    var statement = new DeleteStatement(valueInfo.Name, whereClause);
                    builder.AddStatement(statement);
                    Add(builder);
                }
            }

            foreach (var childInfo in entityInfo.Children)
            {
                foreach (var child in entity.GetChildren(childInfo))
                {
                    var builder = new ComplexCommandBuilder();
                    var idParameter = child.Id.ToParameter();
                    builder.AddParameter(idParameter);
                    var whereClause = string.Format("{0}.{1} = {2}", childInfo.Name, childInfo.Identifier.Name, idParameter.Name);
                    var statement = new DeleteStatement(childInfo.Name, whereClause);
                    builder.AddStatement(statement);

                    AddDeleteStatements(child, childInfo);
                    Add(builder);
                }
            }
        }

        #region Entity Delete

        protected void AddEntityDeleteStatement(IEntity entity, EntityInfo entityInfo)
        {
            var builder = new ComplexCommandBuilder();

            var idParameter = entity.Id.ToParameter();
            builder.AddParameter(idParameter);
            var whereClause = string.Format("{0}.{1} = {2}", entityInfo.Name, entityInfo.Identifier.Name, idParameter.Name);
            var statement = new DeleteStatement(entityInfo.Name, whereClause);

            builder.AddStatement(statement);
            Add(builder);

            AddDeleteStatements(entity, entityInfo);
        }

        protected void AddEntityDeleteStatement(IChild child, EntityInfo childInfo)
        {
            var builder = new ComplexCommandBuilder();

            var idParameter = child.Id.ToParameter();
            builder.AddParameter(idParameter);
            var whereClause = string.Format("{0}.{1} = {2}", childInfo.Name, childInfo.Identifier.Name, idParameter.Name);
            var statement = new DeleteStatement(childInfo.Name, whereClause);

            builder.AddStatement(statement);
            Add(builder);

            AddDeleteStatements(child, childInfo);
        }

        #endregion

        #region Value Delete

        protected void AddValueDeleteStatement(IValue value, ValueInfo valueInfo)
        {
            var builder = new ComplexCommandBuilder();

            var idParameter = value.Id.ToParameter();
            builder.AddParameter(idParameter);
            var whereClause = string.Format("{0}.{1} = {2}", valueInfo.Name, valueInfo.Identifer.Name, idParameter.Name);
            var statement = new DeleteStatement(valueInfo.Name, whereClause);

            builder.AddStatement(statement);
            Add(builder);
        }

        #endregion
    }
}
