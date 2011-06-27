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
    public class ValueQuery<TParent, TValue>
        : IValueQuery<TValue> 
        where TParent : IEntity
        where TValue : IValue
    {
        public ValueQuery(IFactory factory, IFilter filter, Expression<Func<TParent, object>> property)
        {
            var parent = new EntityInfo(typeof(TParent));
            var valueType = typeof(TValue);
            var valueInfo = new ValueInfo(parent, property.ToPropertyInfo(), valueType);

            this.factory = factory;
            this.builder = new CommandBuilder(valueInfo.Name, valueType);
            this.whereClause = filter.WhereClause;
            this.orderByClause = filter.OrderByClause;
            this.parameters = filter.Parameters;

            builder.AddStatement(new SelectStatement(valueInfo, filter));
            foreach (var parameter in filter.Parameters)
            {
                builder.AddParameter(parameter);
            }
        }

        private readonly IFactory factory;
        private readonly ICommandBuilder builder;
        private readonly string whereClause;
        private readonly string orderByClause;
        private readonly IEnumerable<IParameter> parameters;

        #region IValueQuery Members

        public IEnumerable<TValue> Execute(IDbConnection connection)
        {
            //logger.Info("ValueQuery.Execute");

            var values = new List<TValue>();

            var command = builder.GetCommand(connection);
            //logger.Debug("    " + command.CommandText.Trim());
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var value = factory.CreateValue<TValue>(reader);
                    if (value != null)
                    {
                        values.Add(value);
                    }
                }
            }

            //logger.Debug("  return values. count=" + values.Count);
            return values;
        }

        #endregion
    }
}
