using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core.Commands;

namespace Gnosis.Core.Batches
{
    public class SaveEntitiesBatch<T>
        : PersistEntitiesBatch 
        where T : IEntity
    {
        public SaveEntitiesBatch(IDbConnection connection, ILogger logger, IEnumerable<T> entities)
            : base(connection, logger)
        {
            var timeStamp = DateTime.Now.ToUniversalTime();
            var entityInfo = new EntityInfo(typeof(T));

            foreach (var entity in entities)
            {
                if (entity.IsNew())
                {
                    AddEntityInsertStatement(entity, entityInfo, timeStamp);
                }
                else if (entity.IsChanged())
                {
                    AddEntityUpdateStatement(entity, entityInfo, timeStamp);
                }
                else
                {
                    AddSaveStatements(entity, entityInfo, timeStamp);
                }
            }
        }

        #region Entity Insert

        private void AddEntityInsertStatement(IEntity entity, EntityInfo entityInfo, DateTime timeStamp)
        {
            var builder = new CommandBuilder();
            var statement = new InsertStatement(entityInfo.Name);

            foreach (var element in entityInfo.Elements)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(element.Name, parameterName);
                var value = element.Name == "TimeStamp" ? timeStamp : element.GetValue(entity);
                builder.AddParameter(parameterName, value);
            }

            foreach (var dataType in entityInfo.DataTypes)
            {
                foreach (var element in dataType.Elements)
                {
                    var parameterName = builder.GetParameterName();
                    statement.Add(element.Name, parameterName);
                    builder.AddParameter(parameterName, dataType.GetValue(element, entity));
                }
            }

            builder.AddStatement(statement);
            Add(builder);

            AddSaveStatements(entity, entityInfo, timeStamp);

            entity.Save(timeStamp);
        }

        private void AddEntityInsertStatement(IChild child, EntityInfo childInfo, DateTime timeStamp)
        {
            var builder = new CommandBuilder();
            var statement = new InsertStatement(childInfo.Name);

            foreach (var element in childInfo.Elements)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(element.Name, parameterName);
                var value = element.Name == "TimeStamp" ? timeStamp : element.GetValue(child);
                builder.AddParameter(parameterName, value);
            }

            foreach (var dataType in childInfo.DataTypes)
            {
                foreach (var element in dataType.Elements)
                {
                    var parameterName = builder.GetParameterName();
                    statement.Add(element.Name, parameterName);
                    builder.AddParameter(parameterName, dataType.GetValue(element, child));
                }
            }

            builder.AddStatement(statement);
            Add(builder);

            AddSaveStatements(child, childInfo, timeStamp);

            child.Save(timeStamp);
        }

        #endregion

        #region Entity Update

        private void AddEntityUpdateStatement(IEntity entity, EntityInfo entityInfo, DateTime timeStamp)
        {
            var builder = new CommandBuilder();
            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, entity.Id);
            var whereClause = string.Format("{0}.{1} = {2}", entityInfo.Name, entityInfo.Identifier.Name, idParameterName);
            var statement = new UpdateStatement(entityInfo.Name, whereClause);
            
            var timeStampParameterName = builder.GetParameterName();
            statement.Set("TimeStamp", timeStampParameterName);
            builder.AddParameter(timeStampParameterName, timeStamp);

            foreach (var column in entityInfo.Elements.Where(x => !x.IsReadOnly))
            {
                var parameterName = builder.GetParameterName();
                statement.Set(column.Name, parameterName);
                builder.AddParameter(parameterName, column.GetValue(entity));
            }

            foreach (var dataType in entityInfo.DataTypes)
            {
                foreach (var element in dataType.Elements)
                {
                    var parameterName = builder.GetParameterName();
                    statement.Set(element.Name, parameterName);
                    builder.AddParameter(parameterName, dataType.GetValue(element, entity));
                }
            }

            builder.AddStatement(statement);
            Add(builder);

            AddSaveStatements(entity, entityInfo, timeStamp);

            entity.Save(timeStamp);
        }

        private void AddEntityUpdateStatement(IChild child, EntityInfo childInfo, DateTime timeStamp)
        {
            var builder = new CommandBuilder();

            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, child.Id);
            var whereClause = string.Format("{0}.{1} = {2}", childInfo.Name, childInfo.Identifier.Name, idParameterName);
            var statement = new UpdateStatement(childInfo.Name, whereClause);

            var timeStampParameterName = builder.GetParameterName();
            statement.Set("TimeStamp", timeStampParameterName);
            builder.AddParameter(timeStampParameterName, timeStamp);

            foreach (var element in childInfo.Elements.Where(x => !x.IsReadOnly))
            {
                var parameterName = builder.GetParameterName();
                statement.Set(element.Name, parameterName);
                builder.AddParameter(parameterName, element.GetValue(child));
            }

            foreach (var dataType in childInfo.DataTypes)
            {
                foreach (var element in dataType.Elements)
                {
                    var parameterName = builder.GetParameterName();
                    statement.Set(element.Name, parameterName);
                    builder.AddParameter(parameterName, dataType.GetValue(element, child));
                }
            }

            builder.AddStatement(statement);
            Add(builder);

            AddSaveStatements(child, childInfo, timeStamp);

            child.Save(timeStamp);
        }

        #endregion

        #region Value Insert

        private void AddValueInsertStatement(IValue value, ValueInfo valueInfo)
        {
            var builder = new CommandBuilder();
            var statement = new InsertStatement(valueInfo.Name);

            foreach (var element in valueInfo.Elements)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(element.Name, parameterName);
                builder.AddParameter(parameterName, element.GetValue(value));
            }

            builder.AddStatement(statement);
            Add(builder);

            value.Save();
        }

        #endregion

        #region Value Move

        private void AddValueMoveStatement(IValue value, ValueInfo valueInfo)
        {
            var builder = new CommandBuilder();

            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, value.Id);
            var whereClause = string.Format("{0}.{1} = {2}", valueInfo.Name, valueInfo.Identifer.Name, value.Id);
            var statement = new UpdateStatement(valueInfo.Name, whereClause);

            var sequenceParameterName = builder.GetParameterName();
            statement.Set(valueInfo.Sequence.Name, sequenceParameterName);
            builder.AddParameter(sequenceParameterName, valueInfo.Sequence.GetValue(value));

            builder.AddStatement(statement);
            Add(builder);

            value.Save();
        }

        #endregion

        #region Child Entity

        private void AddSaveStatements(IEntity entity, EntityInfo entityInfo, DateTime timeStamp)
        {
            foreach (var valueInfo in entityInfo.Values)
            {
                foreach (var value in entity.GetValues(valueInfo))
                {
                    if (value.IsNew())
                    {
                        AddValueInsertStatement(value, valueInfo);
                    }
                    else if (value.IsMoved())
                    {
                        AddValueMoveStatement(value, valueInfo);
                    }
                    else if (value.IsRemoved())
                    {
                        AddValueDeleteStatement(value, valueInfo);
                    }
                }
            }

            foreach (var childInfo in entityInfo.Children)
            {
                foreach (var child in entity.GetChildren(childInfo))
                {
                    if (child.IsNew())
                    {
                        AddEntityInsertStatement(child, childInfo, timeStamp);
                    }
                    else if (child.IsChanged() || child.IsMoved())
                    {
                        AddEntityUpdateStatement(child, childInfo, timeStamp);
                    }
                    else if (child.IsRemoved())
                    {
                        AddEntityDeleteStatement(child, childInfo);
                    }
                    else
                    {
                        AddSaveStatements(child, childInfo, timeStamp);
                    }
                }
            }
        }

        #endregion
    }
}
