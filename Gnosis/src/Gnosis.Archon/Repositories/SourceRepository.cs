using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Archon;

namespace Gnosis.Archon.Repositories
{
    public class SourceRepository : RepositoryBase<ISource>
    {
        public SourceRepository()
            : base("Alexandria.db", "Source", "Number, NameHash")
        {
        }

        protected override string GetInitializeText()
        {
            var sql = new StringBuilder();

            sql.AppendLine("create table if not exists Source (");
            sql.AppendLine("  Id TEXT PRIMARY KEY NOT NULL,");
            sql.AppendLine("  Path TEXT,");
            sql.AppendLine("  ImagePath TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("  Parent TEXT,");
            sql.AppendLine("  Name TEXT NOT NULL DEFAULT 'Unknown Source',");
            sql.AppendLine("  NameHash TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("  NameMetaphone TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("  Creator TEXT NOT NULL DEFAULT 'Unknown Creator',");
            sql.AppendLine("  Number INTEGER NOT NULL DEFAULT 0");
            sql.AppendLine(");");
            sql.AppendLine("create index if not exists Source_Path on Source (Path ASC);");
            sql.AppendLine("create index if not exists Source_Parent on Source (Parent ASC);");
            sql.AppendLine("create index if not exists Source_Name on Source (Name ASC);");
            sql.AppendLine("create index if not exists Source_NameHash on Source (NameHash ASC);");
            sql.AppendLine("create index if not exists Source_NameMetaphone on Source (NameMetaphone ASC);");
            sql.AppendLine("create index if not exists Source_Creator on Source (Creator ASC);");
            sql.AppendLine("create index if not exists Source_Nuber on Source (Number ASC);");
            sql.AppendLine("create index if not exists Source_DefaultSortOrder on Source (Number ASC, NameHash ASC);");

            sql.AppendLine("create table if not exists SourceProperty (");
            sql.AppendLine("  Id TEXT PRIMARY KEY NOT NULL,");

            return sql.ToString();
        }

        protected override ISource GetRecord(IDataReader reader)
        {
            ISource source = null;

            //TODO: Get source by type

            return source;
        }

        protected override IDbCommand GetDeleteCommand(IDbConnection connection, Guid id)
        {
            throw new NotImplementedException();
        }

        protected override IDbCommand GetSaveCommand(IDbConnection connection, ISource record)
        {
            throw new NotImplementedException();
        }

        protected override IDbCommand GetSelectCommand(IDbConnection connection, Guid id)
        {
            return base.GetSelectCommand(connection, id);
        }

        protected override IDbCommand GetSelectCommand(IDbConnection connection, IEnumerable<KeyValuePair<string, object>> criteria)
        {
            throw new NotImplementedException();
        }
    }
}
