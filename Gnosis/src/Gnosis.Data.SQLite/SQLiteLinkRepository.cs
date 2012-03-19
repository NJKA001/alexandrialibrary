using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Links;

namespace Gnosis.Data.SQLite
{
    public class SQLiteLinkRepository
        : SQLiteRepositoryBase, ILinkRepository
    {
        public SQLiteLinkRepository(ILogger logger)
            : base(logger)
        {
        }

        public SQLiteLinkRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, defaultConnection)
        {
        }

        #region Private Methods

        private IEnumerable<Gnosis.ILink> GetLinks(ICommandBuilder builder)
        {
            IDbConnection connection = null;
            var links = new List<Gnosis.ILink>();

            try
            {
                connection = GetConnection();

                var command = builder.ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var link = ReadLink(reader);
                        links.Add(link);
                    }
                }

                return links;
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        private Gnosis.ILink ReadLink(IDataRecord record)
        {
            var source = record.GetUri("Source");
            var target = record.GetUri("Target");
            var relationship = record.GetString("Relationship");
            var name = record.GetString("Name");
            var id = record.GetInt64("Id");

            return new Gnosis.Links.Link(source, target, relationship, name, id);
        }

        #endregion

        public Gnosis.ILink GetById(long id)
        {
            try
            {
                var builder = new CommandBuilder("select * from Link where Id = @Id;");
                builder.AddParameter("@Id", id);

                return GetLinks(builder).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error("  GetById", ex);
                throw;
            }
        }

        public IEnumerable<Gnosis.ILink> GetBySource(Uri source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            try
            {
                var builder = new CommandBuilder("select * from Link where Source = @Source;");
                builder.AddParameter("@Source", source.ToString());

                return GetLinks(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetBySource(Uri)", ex);
                throw;
            }
        }

        public IEnumerable<Gnosis.ILink> GetBySource(Uri source, string relationship)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (relationship == null)
                throw new ArgumentNullException("relationship");

            try
            {
                var builder = new CommandBuilder("select * from Link where Source = @Source and Relationship = @Relationship;");
                builder.AddParameter("@Source", source.ToString());
                builder.AddParameter("@Relationship", relationship);

                return GetLinks(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetBySource(Uri, string)", ex);
                throw;
            }
        }

        public IEnumerable<Gnosis.ILink> GetByTarget(Uri target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            try
            {
                var builder = new CommandBuilder("select * from Link where Target = @Target;");
                builder.AddParameter("@Target", target.ToString());

                return GetLinks(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetByTarget(Uri)", ex);
                throw;
            }
        }

        public IEnumerable<Gnosis.ILink> GetByTarget(Uri target, string relationship)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (relationship == null)
                throw new ArgumentNullException("relationship");

            try
            {
                var builder = new CommandBuilder("select * from Link where Target = @Target and Relationship = @Relationship;");
                builder.AddParameter("@Target", target.ToString());
                builder.AddParameter("@Relationship", relationship);

                return GetLinks(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetByTarget(Uri, string)", ex);
                throw;
            }
        }

        public IEnumerable<Gnosis.ILink> GetBySourceAndTarget(Uri source, Uri target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (source == null)
                throw new ArgumentNullException("source");

            try
            {
                var builder = new CommandBuilder("select * from Link where Source = @Source and Target = @Target;");
                builder.AddParameter("@Source", source.ToString());
                builder.AddParameter("@Target", target.ToString());

                return GetLinks(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetBySourceAndTarget(Uri, Uri)", ex);
                throw;
            }
        }

        public IEnumerable<Gnosis.ILink> GetBySourceAndTarget(Uri source, Uri target, string relationship)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");
            if (relationship == null)
                throw new ArgumentNullException("relationship");

            try
            {
                var builder = new CommandBuilder("select * from Link where Source = @Source and Target = @Target and Relationship = @Relationship;");
                builder.AddParameter("@Source", source.ToString());
                builder.AddParameter("@Target", target.ToString());
                builder.AddParameter("@Relationship", relationship);

                return GetLinks(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetBySourceAndTarget(Uri, Uri, string)", ex);
                throw;
            }
        }

        public void Initialize()
        {
            try
            {
                var builder = new CommandBuilder();
                builder.AppendLine("create table if not exists Link (Id integer primary key not null, Source text not null, Target text not null, Relationship text not null, Name text not null);");
                builder.AppendLine("create index if not exists Link_Source on Link (Source asc);");
                builder.AppendLine("create index if not exists Link_Source_Relationship on Link (Source asc, Relationship asc);");
                builder.AppendLine("create index if not exists Link_Target on Link (Target asc);");
                builder.AppendLine("create index if not exists Link_Target_Relationship on Link (Target asc, Relationship asc);");
                builder.AppendLine("create index if not exists Link_Source_Target on Link (Source asc, Target asc);");
                builder.AppendLine("create index if not exists Link_Source_Target_Relationship on Link (Source asc, Target asc, Relationship asc);");
                builder.AppendLine("create unique index if not exists Link_Source_Target_Relationship_Name on Link (Source asc, Target asc, Relationship asc, Name asc);");

                ExecuteNonQuery(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  Initialize", ex);
                throw;
            }
        }

        public void Delete(IEnumerable<long> ids)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");

            try
            {
                var builders = new List<ICommandBuilder>();

                foreach (var id in ids)
                {
                    var builder = new CommandBuilder();
                    builder.AppendLine("delete from Link where Id = @Id;");
                    builder.AddParameter("@Id", id);

                    builders.Add(builder);
                }

                if (builders.Count == 0)
                    return;

                ExecuteTransaction(builders);
            }
            catch (Exception ex)
            {
                logger.Error("  Delete", ex);
            }
        }

        public void Save(IEnumerable<Gnosis.ILink> links)
        {
            if (links == null)
                throw new ArgumentNullException("links");

            try
            {
                var builders = new List<ICommandBuilder>();

                foreach (var link in links)
                {
                    var builder = new CommandBuilder();
                    builder.AppendLine("replace into Link (Id, Source, Target, Relationship, Name) values (@Id, @Source, @Target, @Relationship, @Name);");
                    builder.AddParameter("@Id", link.Id > 0 ? (object)link.Id : (object)DBNull.Value);
                    builder.AddParameter("@Source", link.Source.ToString());
                    builder.AddParameter("@Target", link.Target.ToString());
                    builder.AddParameter("@Relationship", link.Relationship);
                    builder.AddParameter("@Name", link.Name);

                    builders.Add(builder);
                }

                if (builders.Count == 0)
                    return;

                ExecuteTransaction(builders);
            }
            catch (Exception ex)
            {
                logger.Error("  Save", ex);
                throw;
            }
        }
    }
}
