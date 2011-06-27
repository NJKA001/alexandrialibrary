using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Data.Commands;

namespace Gnosis.Data.Queries
{
    public class OutlineQuery<TEntity, TOutline>
        : IOutlineQuery<TEntity, TOutline>
        where TEntity : IEntity
        where TOutline : IOutline<TEntity>
    {
        public OutlineQuery(Func<TOutline> factory, IFilter filter)
        {
            var entityInfo = new EntityInfo(typeof(TEntity));

            //this.logger = logger;
            this.factory = factory;
            this.builder = new CommandBuilder(entityInfo.Name, entityInfo.Type);

            builder.AddStatement(new SelectStatement(entityInfo, filter));
            foreach (var parameter in filter.Parameters)
            {
                builder.AddParameter(parameter);
            }
        }

        //private readonly ILogger logger;
        private readonly Func<TOutline> factory;

        private readonly ICommandBuilder builder;

        #region IQuery Members

        public IEnumerable<TOutline> Execute(IDbConnection connection)
        {
            //logger.Info("OutlineQuery.Execute");

            var outlines = new List<TOutline>();

            var command = builder.GetCommand(connection);
            //logger.Debug("    " + command.CommandText.Trim());
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var outline = factory();
                    outline.Initialize(reader);
                    outlines.Add(outline);
                }
            }

            //logger.Debug("  return outlines. count=" + outlines.Count);
            return outlines;
        }

        #endregion
    }
}
