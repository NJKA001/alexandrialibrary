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
                var value = element.Name == "TimeStamp" ? timeStamp : element.GetValue(entity);
                var parameter = value.ToParameter(entityInfo.Name + "_" + element.Name);
                statement.Add(element.Name, parameter.Name);
                builder.AddParameter(parameter);
            }

            foreach (var dataType in entityInfo.DataTypes)
            {
                foreach (var element in dataType.Elements)
                {
                    var value = dataType.GetValue(element, entity);
                    var parameter = value.ToParameter(dataType.Name + "_" + element.Name);
                    statement.Add(element.Name, parameter.Name);
                    builder.AddParameter(parameter);
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
                var value = element.Name == "TimeStamp" ? timeStamp : element.GetValue(child);
                var parameter = value.ToParameter(childInfo.Name + "_" + element.Name);
                statement.Add(element.Name, parameter.Name);
                
                builder.AddParameter(parameter);
            }

            foreach (var dataType in childInfo.DataTypes)
            {
                foreach (var element in dataType.Elements)
                {
                    var value = dataType.GetValue(element, child);
                    var parameter = value.ToParameter(dataType.Name + "_" + element.Name);
                    statement.Add(element.Name, parameter.Name);
                    builder.AddParameter(parameter);
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
            var idParameter = entity.Id.ToParameter(entityInfo.Name + "_" + entityInfo.Identifier.Name);
            builder.AddParameter(idParameter);
            var whereClause = string.Format("{0}.{1} = {2}", entityInfo.Name, entityInfo.Identifier.Name, idParameter.Name);
            var statement = new UpdateStatement(entityInfo.Name, whereClause);

            const string tsName = "TimeStamp";
            var timeStampParameter = timeStamp.ToParameter(tsName);
            statement.Set(tsName, timeStampParameter.Name);
            builder.AddParameter(timeStampParameter);

            foreach (var column in entityInfo.Elements.Where(x => !x.IsReadOnly))
            {
                var value = column.GetValue(entity);
                var parameter = value.ToParameter(entityInfo.Name + "_" + column.Name);
                statement.Set(column.Name, parameter.Name);
                builder.AddParameter(parameter);
            }

            foreach (var dataType in entityInfo.DataTypes)
            {
                foreach (var element in dataType.Elements)
                {
                    var value = dataType.GetValue(element, entity);
                    var parameter = value.ToParameter(dataType.Name + "_" + element.Name);
                    statement.Set(element.Name, parameter.Name);
                    builder.AddParameter(parameter);
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

            var idParameter = child.Id.ToParameter(childInfo.Name + "_" + childInfo.Identifier.Name);
            builder.AddParameter(idParameter);
            var whereClause = string.Format("{0}.{1} = {2}", childInfo.Name, childInfo.Identifier.Name, idParameter.Name);
            var statement = new UpdateStatement(childInfo.Name, whereClause);

            var tsName = "TimeStamp";
            var timeStampParameter = timeStamp.ToParameter(tsName);
            statement.Set(tsName, timeStampParameter.Name);
            builder.AddParameter(timeStampParameter);

            foreach (var element in childInfo.Elements.Where(x => !x.IsReadOnly))
            {
                var value = element.GetValue(child);
                var parameter = value.ToParameter(childInfo.Name + "_" + element.Name);
                statement.Set(element.Name, parameter.Name);
                builder.AddParameter(parameter);
            }

            foreach (var dataType in childInfo.DataTypes)
            {
                foreach (var element in dataType.Elements)
                {
                    var value = dataType.GetValue(element, child);
                    var parameter = value.ToParameter(dataType.Name + "_" + element.Name);
                    statement.Set(element.Name, parameter.Name);
                    builder.AddParameter(parameter);
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
                var result = element.GetValue(value);
                var parameter = result.ToParameter(valueInfo.Name + "_" + element.Name);
                statement.Add(element.Name, parameter.Name);
                builder.AddParameter(parameter);
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

            var idParameter = value.Id.ToParameter(valueInfo.Name + "_" + valueInfo.Identifer.Name);
            builder.AddParameter(idParameter);
            var whereClause = string.Format("{0}.{1} = {2}", valueInfo.Name, valueInfo.Identifer.Name, idParameter.Name);
            var statement = new UpdateStatement(valueInfo.Name, whereClause);

            var seq = valueInfo.Sequence.GetValue(value);
            var sequenceParameter = seq.ToParameter(valueInfo.Sequence.Name);
            statement.Set(valueInfo.Sequence.Name, sequenceParameter.Name);
            builder.AddParameter(sequenceParameter);

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
