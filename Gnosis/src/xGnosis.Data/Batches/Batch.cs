using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Data.Commands;

namespace Gnosis.Data.Batches
{
    public class Batch
        : IBatch
    {
        public Batch(IDbConnection connection)
        {
            this.connection = connection;
        }

        private readonly IDbConnection connection;
        private readonly IList<ICommandBuilder> builders = new List<ICommandBuilder>();

        public void Add(ICommandBuilder builder)
        {
            builders.Add(builder);
        }

        public void Execute()
        {
            //logger.Info("Batch.Execute()");
            IDbTransaction transaction = null;
            var isCommited = false;
            try
            {
                using (transaction = connection.BeginTransaction())
                {
                    foreach (var builder in builders)
                    {
                        using (var command = builder.GetCommand(connection))
                        {
                            //logger.Debug("    " + command.CommandText.Trim());
                            System.Diagnostics.Debug.WriteLine("    " + command.CommandText);
                            foreach (var parameter in command.Parameters)
                            {
                                var dbParameter = parameter as System.Data.IDbDataParameter;
                                if (dbParameter != null)
                                    System.Diagnostics.Debug.WriteLine("      parameter name=" + dbParameter.ParameterName + " value=" + dbParameter.Value);
                                    //logger.Debug("      parameter name=" + dbParameter.ParameterName + " value=" + dbParameter.Value);
                            }
                            command.ExecuteNonQuery();
                        }
                    }
                    //logger.Debug("  committing transaction");
                    transaction.Commit();
                    isCommited = true;
                }
            }
            catch (Exception ex)
            {
                //logger.Error("Batch.Execute failed, rolling back transaction", ex);
                System.Diagnostics.Debug.WriteLine("Batch.Execute failed: " + ex.Message + Environment.NewLine + ex.StackTrace);

                if (!isCommited && transaction != null && transaction.Connection != null)
                    transaction.Rollback();

                throw;
            }
        }
    }
}
