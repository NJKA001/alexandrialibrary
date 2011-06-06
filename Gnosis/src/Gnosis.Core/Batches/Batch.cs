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
        public Batch(Func<IDbConnection> getConnection, ILogger logger)
        {
            this.getConnection = getConnection;
            this.logger = logger;
        }

        private readonly Func<IDbConnection> getConnection;
        private readonly ILogger logger;
        private readonly IList<ICommandBuilder> builders = new List<ICommandBuilder>();

        public void Add(ICommandBuilder builder)
        {
            builders.Add(builder);
        }

        public void Execute()
        {
            logger.Info("Batch.Execute()");
            IDbTransaction transaction = null;
            var isCommited = false;
            try
            {
                using (var connection = getConnection())
                {
                    logger.Debug("  opening connection");
                    connection.Open();
                    using (transaction = connection.BeginTransaction())
                    {
                        foreach (var builder in builders)
                        {
                            using (var command = builder.GetCommand(connection))
                            {
                                logger.Debug("    " + command.CommandText.Trim());
                                command.ExecuteNonQuery();
                            }
                        }
                        logger.Debug("  committing transaction");
                        transaction.Commit();
                        isCommited = true;
                    }
                }
                logger.Debug("  closing connection");
            }
            catch (Exception ex)
            {
                logger.Error("Batch.Execute failed, rolling back transaction", ex);

                if (!isCommited && transaction != null && transaction.Connection != null)
                    transaction.Rollback();

                throw;
            }
        }
    }
}
