using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Data.SQLite
{
    public class SQLiteLinkRepository
        : SQLiteRepositoryBase, ILinkRepository
    {
        public SQLiteLinkRepository(ILogger logger, ILinkTypeFactory typeFactory)
            : base(logger, connectionString)
        {
            if (typeFactory == null)
                throw new ArgumentNullException("typeFactory");

            this.typeFactory = typeFactory;
        }

        public SQLiteLinkRepository(ILogger logger, ILinkTypeFactory typeFactory, IDbConnection defaultConnection)
            : base(logger, connectionString, defaultConnection)
        {
            if (typeFactory == null)
                throw new ArgumentNullException("typeFactory");

            this.typeFactory = typeFactory;
        }

        private const string connectionString = "Data Source=Link.db;Version=3;";
        private readonly ILinkTypeFactory typeFactory;

        #region Private Methods

        private IEnumerable<ILink> GetLinks(ICommandBuilder builder)
        {
            IDbConnection connection = null;
            var links = new List<ILink>();

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

        private ILink ReadLink(IDataRecord record)
        {
            var source = record.GetUri("Source");
            var target = record.GetUri("Target");
            var type = record.GetInt32Lookup<ILinkType>("Type", typeId => typeFactory.Create(typeId));
            var name = record.GetString("Name");
            var id = record.GetInt64("Id");

            return new Link(source, target, type, name, id);
        }

        #endregion

        public ILink GetById(long id)
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

        public IEnumerable<ILink> GetBySource(Uri source)
        {
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

        public IEnumerable<ILink> GetBySource(Uri source, ILinkType type)
        {
            try
            {
                var builder = new CommandBuilder("select * from Link where Source = @Source and Type = @Link;");
                builder.AddParameter("@Source", source.ToString());
                builder.AddParameter("@Link", type.Id);

                return GetLinks(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetBySource(Uri, ILinkType)", ex);
                throw;
            }
        }

        public IEnumerable<ILink> GetByTarget(Uri target)
        {
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

        public IEnumerable<ILink> GetByTarget(Uri target, ILinkType type)
        {
            try
            {
                var builder = new CommandBuilder("select * from Link where Target = @Target and Type = @Type;");
                builder.AddParameter("@Target", target.ToString());
                builder.AddParameter("@Type", type.Id);

                return GetLinks(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetByTarget(Uri, ILinkType)", ex);
                throw;
            }
        }

        public IEnumerable<ILink> GetBySourceAndTarget(Uri source, Uri target)
        {
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

        public IEnumerable<ILink> GetBySourceAndTarget(Uri source, Uri target, ILinkType type)
        {
            try
            {
                var builder = new CommandBuilder("select * from Link where Source = @Source and Target = @Target and Type = @Type;");
                builder.AddParameter("@Source", source.ToString());
                builder.AddParameter("@Target", target.ToString());
                builder.AddParameter("@Type", type.Id);

                return GetLinks(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetBySourceAndTarget(Uri, Uri, ILinkType)", ex);
                throw;
            }
        }

        public void Initialize()
        {
            try
            {
                var builder = new CommandBuilder();
                builder.AppendLine("create table if not exists Link (Id integer primary key not null, Source text not null, Target text not null, Type integer not null, Name text not null);");
                builder.AppendLine("create index if not exists Link_Source on Link (Source asc);");
                builder.AppendLine("create index if not exists Link_Source_Type on Link (Source asc, Type asc);");
                builder.AppendLine("create index if not exists Link_Target on Link (Target asc);");
                builder.AppendLine("create index if not exists Link_Target_Type on Link (Target asc, Type asc);");
                builder.AppendLine("create index if not exists Link_Source_Target on Link (Source asc, Target asc);");
                builder.AppendLine("create index if not exists Link_Source_Target_Type on Link (Source asc, Target asc, Type asc);");

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

        public void Save(IEnumerable<ILink> links)
        {
            if (links == null)
                throw new ArgumentNullException("links");

            try
            {
                var builders = new List<ICommandBuilder>();

                foreach (var link in links)
                {
                    var builder = new CommandBuilder();
                    builder.AppendLine("replace into Link (Id, Source, Target, Type, Name) values (@Id, @Source, @Target, @Type, @Name);");
                    builder.AddParameter("@Id", link.Id > 0 ? (object)link.Id : (object)DBNull.Value);
                    builder.AddParameter("@Source", link.Source.ToString());
                    builder.AddParameter("@Target", link.Target.ToString());
                    builder.AddParameter("@Type", link.Type.Id);
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
