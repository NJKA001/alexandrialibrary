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
        public Query(Func<IDbConnection> getConnection, IFilter filter)
        {
            this.getConnection = getConnection;
            this.whereClause = filter.WhereClause;
            this.orderByClause = filter.OrderByClause;
            this.parameters = filter.Parameters;
        }

        private readonly Func<IDbConnection> getConnection;
        private readonly string whereClause;
        private readonly string orderByClause;
        private readonly IEnumerable<KeyValuePair<string, object>> parameters;

        private readonly IList<ICommandBuilder> builders = new List<ICommandBuilder>();

        public void Add(ICommandBuilder builder)
        {
            builders.Add(builder);
        }

        public IEnumerable<T> Execute()
        {
            var items = new List<T>();

            using (var connection = getConnection())
            {
                connection.Open();
            }

            return items;
        }
    }
}
