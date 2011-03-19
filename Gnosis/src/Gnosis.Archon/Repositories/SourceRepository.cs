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
            : base("Alexandria.db", "Source")
        {
        }

        protected override string GetInitializeText()
        {
            var sql = new StringBuilder();

            /*
             *         Guid Id { get; }
        string Path { get; set; }
        string ImagePath { get; set; }
        ICollection<byte> ImageData { get; set; }
        object ImageSource { get; }
        ISource Parent { get; set; }
        string Name { get; set; }
        string NameHash { get; }
        string NameMetaphone { get; }
            */

            return sql.ToString();
        }

        protected override ISource Get(IDataReader reader)
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
