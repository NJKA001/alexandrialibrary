using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Data.Firebird
{
    public class FirebirdTagRepository
        : FirebirdRepositoryBase
    {
        public FirebirdTagRepository(ILogger logger)
            : base(logger)
        {
        }

        public void Initialize()
        {
            try
            {
                CreateDatabase();

                var tableSql = new StringBuilder();
                tableSql.AppendLine("EXECUTE BLOCK AS BEGIN");
                tableSql.AppendLine("if (not exists(select 1 from rdb$relations where rdb$relation_name = 'TAG')) then");
                tableSql.AppendLine("execute statement 'create table Tag (Id INT64 NOT NULL, Target VARCHAR(4000) CHARACTER SET UNICODE_FSS NOT NULL, Type INT64 NOT NULL, Name VARCHAR(4000) CHARACTER SET UNICODE_FSS NOT NULL, PRIMARY KEY (Id));';");
                tableSql.AppendLine("END");

                Execute(tableSql.ToString(), Enumerable.Empty<KeyValuePair<string, object>>());

                var generatorSql = new StringBuilder();
                generatorSql.AppendLine("EXECUTE BLOCK AS BEGIN");
                generatorSql.AppendLine("if (not exists(select 1 from rdb$relations where rdb$relation_name = 'TAG_SEQUENCE')) then");
                generatorSql.AppendLine("execute statement 'create sequence Tag_Sequence;';");
                generatorSql.AppendLine("END");
            }
            catch (Exception ex)
            {
                logger.Error("  Initialize", ex);
                throw;
            }
        }

        public void Save(IEnumerable<ITag> tags)
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error("  Save", ex);
                throw;
            }
        }
    }
}
