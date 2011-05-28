using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core.Commands;

namespace Gnosis.Core.Batches
{
    public class Batch
        : IBatch
    {
        public Batch(Func<IDbConnection> getConnection)
        {
            this.getConnection = getConnection;
        }

        private readonly Func<IDbConnection> getConnection;
        private readonly IList<ICommandBuilder> builders = new List<ICommandBuilder>();

        public void Add(ICommandBuilder builder)
        {
            builders.Add(builder);
        }

        public void Execute()
        {
            IDbTransaction transaction = null;
            var isCommited = false;
            try
            {
                using (var connection = getConnection())
                {
                    connection.Open();
                    using (transaction = connection.BeginTransaction())
                    {
                        foreach (var builder in builders)
                        {
                            using (var command = builder.GetCommand(connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        isCommited = true;
                    }
                }
            }
            catch (Exception)
            {
                if (!isCommited && transaction != null && transaction.Connection != null)
                    transaction.Rollback();

                throw;
            }
        }
    }
}
