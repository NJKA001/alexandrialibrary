using System;
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
        }

        private readonly Func<IDbConnection> getConnection;
        private readonly IFactory factory;
        private readonly ICommandBuilder builder;
        private readonly string whereClause;
        private readonly string orderByClause;
        private readonly IEnumerable<KeyValuePair<string, object>> parameters;

        private void AddChildren(IDbConnection connection, IEnumerable<IEntity> parents)
        {
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
                        var item = factory.Create<T>(reader);
                        if (item != null)
                        {
                            items.Add(item);
                        }
                    }
                }

            }

            return items;
        }
    }
}
