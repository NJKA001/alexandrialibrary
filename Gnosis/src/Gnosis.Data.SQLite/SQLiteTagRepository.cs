using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;
using Gnosis.Data.Queries;

namespace Gnosis.Data.SQLite
{
    public class SQLiteTagRepository
        : SQLiteRepositoryBase, ITagRepository
    {
        public SQLiteTagRepository(ILogger logger, ISchemaRepository schemaRepository)
            : base(logger, connectionString)
        {
            if (schemaRepository == null)
                throw new ArgumentNullException("schemaRepository");

            this.schemaRepository = schemaRepository;
        }

        public SQLiteTagRepository(ILogger logger, ISchemaRepository schemaRepository, IDbConnection defaultConnection)
            : base(logger, connectionString, defaultConnection)
        {
            if (schemaRepository == null)
                throw new ArgumentNullException("schemaRepository");

            this.schemaRepository = schemaRepository;
        }

        private const string connectionString = "Data Source=Tag.db;Version=3;";
        private readonly ISchemaRepository schemaRepository;

        #region Private Methods

        private ITag ReadTag(IDataRecord record)
        {
            var target = record.GetUri("Target");
            var algorithm = record.GetInt32Lookup<IAlgorithm>("Algorithm", algorithmId => Algorithm.Parse(algorithmId));
            var schema = schemaRepository.Get(record.GetUri("Schema"));
            var name = record.GetString("Name");
            var id = record.GetInt64("Id");
            return new Tag(target, algorithm, schema, name, id);
        }

        #endregion

        public ITag Lookup(long id)
        {
            try
            {
                logger.Info("SQLiteTagRepository.Lookup");

                var builder = new SimpleCommandBuilder("select * from Tag where Id = @Id;");
                builder.AddUnquotedParameter("@Id", id);

                return GetRecord(builder, record => ReadTag(record));
            }
            catch (Exception ex)
            {
                logger.Error("  Lookup", ex);
                throw;
            }
        }

        public IEnumerable<ITag> All()
        {
            try
            {
                logger.Info("SQLiteTagRepository.All()");

                var builder = new SimpleCommandBuilder("select * from Tag;");

                return GetRecords(builder, record => ReadTag(record));
            }
            catch (Exception ex)
            {
                logger.Error("  All", ex);
                throw;
            }
        }

        public IEnumerable<ITag> Search(IAlgorithm algorithm, Uri schema)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (schema == null)
                throw new ArgumentNullException("schema");

            try
            {
                logger.Info("SQLiteTagRepository.Search(IAlgorithm, Uri)");

                var schemaPattern = string.Format("{0}%", schema);
                var builder = new SimpleCommandBuilder("select * from Tag where Algorithm = @Algorithm and Schema like @Schema;");
                builder.AddUnquotedParameter("@Algorithm", algorithm.Id);
                builder.AddQuotedParameter("@Schema", schemaPattern);

                return GetRecords(builder, record => ReadTag(record));
            }
            catch (Exception ex)
            {
                logger.Error("  Search(IAlgorithm, Uri)", ex);
                throw;
            }
        }

        public IEnumerable<ITag> Search(IAlgorithm algorithm, Uri schema, string name)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (schema == null)
                throw new ArgumentNullException("schema");
            if (name == null)
                throw new ArgumentNullException("name");

            try
            {
                logger.Info("SQLiteTagRepository.Search(IAlgorithm, Uri, string)");

                var builder = new SimpleCommandBuilder("select * from Tag where Algorithm = @Algorithm and Schema like @Schema and Name like @Name;");
                builder.AddUnquotedParameter("@Algorithm", algorithm.Id);
                builder.AddQuotedParameter("@Schema", schema.ToString() + "%");
                builder.AddQuotedParameter("@Name", name);

                return GetRecords(builder, record => ReadTag(record));
            }
            catch (Exception ex)
            {
                logger.Error("  Search(IAlgorithm, Uri, string)", ex);
                throw;
            }
        }

        public IEnumerable<ITag> Search(IAlgorithm algorithm, string name)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (name == null)
                throw new ArgumentNullException("name");

            try
            {
                logger.Info("SQLiteTagRepository.Search(IAlgorithm, string)");

                var builder = new SimpleCommandBuilder("select * from Tag where Algorithm = @Algorithm and Name like @Name;");
                builder.AddUnquotedParameter("@Algorithm", algorithm.Id);
                builder.AddQuotedParameter("@Name", name);

                return GetRecords(builder, record => ReadTag(record));
            }
            catch (Exception ex)
            {
                logger.Error("  Search(IAlgorithm, string)", ex);
                throw;
            }
        }

        public void Initialize()
        {
            try
            {
                logger.Info("SQLiteTagRepository.Initialize");

                var builder = new SimpleCommandBuilder();
                builder.AppendLine("create table if not exists Tag (Id integer primary key not null, Target text not null, Algorithm integer not null, Schema text not null, Name text not null);");
                builder.AppendLine("create index if not exists Tag_Target on Tag (Target asc);");
                builder.AppendLine("create index if not exists Tag_Target_Algorithm on Tag (Target asc, Algorithm asc);");
                builder.AppendLine("create index if not exists Tag_Target_Schema on Tag (Target asc, Schema asc);");
                builder.AppendLine("create index if not exists Tag_Algorithm on Tag (Algorithm asc);");
                builder.AppendLine("create index if not exists Tag_Algorithm_Schema on Tag (Algorithm asc, Schema asc);");
                builder.AppendLine("create index if not exists Tag_Algorithm_Name on Tag (Algorithm asc, Name asc);");
                builder.AppendLine("create index if not exists Tag_Schema on Tag (Schema asc);");
                builder.AppendLine("create index if not exists Tag_Schema_Name on Tag (Schema asc, Name asc);");
                builder.AppendLine("create index if not exists Tag_Name on Tag (Name asc);");

                ExecuteNonQuery(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  Initialize", ex);
                throw;
            }
        }

        public void Save(ITag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            try
            {
                logger.Info("SQLiteTagRepository.Save(ITag)");

                var builder = new SimpleCommandBuilder();
                builder.Append("insert into Tag (Target, Algorithm, Schema, Name) values (@Target, @Algorithm, @Schema, @Name);");
                builder.AddQuotedParameter("@Target", tag.Target.ToString());
                builder.AddUnquotedParameter("@Algorithm", tag.Algorithm.Id);
                builder.AddQuotedParameter("@Schema", tag.Schema.Identifier.ToString());
                builder.AddQuotedParameter("@Name", tag.Name);

                ExecuteNonQuery(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  Save(ITag)", ex);
                throw;
            }
        }

        public void Save(IEnumerable<ITag> tags)
        {
            if (tags == null)
                throw new ArgumentNullException("tags");

            try
            {
                logger.Info("SQLiteTagRepository.Save(IEnumerable<ITag>)");

                var builder = new SimpleCommandBuilder();

                var count = 0;
                foreach (var tag in tags)
                {
                    count++;
                    builder.AppendFormatLine("insert into Tag (Target, Algorithm, Schema, Name) values (@Target{0}, @Algorithm{0}, @Schema{0}, @Name{0});", count);
                    builder.AddQuotedParameter(string.Format("@Target{0}", count), tag.Target.ToString());
                    builder.AddUnquotedParameter(string.Format("@Algorithm{0}", count), tag.Algorithm.Id);
                    builder.AddQuotedParameter(string.Format("@Schema{0}", count), tag.Schema.Identifier.ToString());
                    builder.AddQuotedParameter(string.Format("@Name{0}", count), tag.Name);
                }

                if (count == 0)
                    return;

                ExecuteTransaction(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  Save(IEnumerable<ITag>)", ex);
                throw;
            }
        }

        public void Delete(ITag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            try
            {
                logger.Info("SQLiteTagRepository.Delete(ITag)");

                var builder = new SimpleCommandBuilder();
                builder.Append("delete from Tag where Id = @Id;");
                builder.AddUnquotedParameter("@Id", tag.Id);

                ExecuteNonQuery(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  Delete(ITag)", ex);
                throw;
            }
        }

        public void Delete(IEnumerable<ITag> tags)
        {
            if (tags == null)
                throw new ArgumentNullException("tags");

            try
            {
                logger.Info("SQLiteTagRepository.Delete(IEnumerable<ITag>)");

                var builder = new SimpleCommandBuilder();

                var count = 0;
                foreach (var tag in tags)
                {
                    count++;
                    builder.AppendFormatLine("delete from Tag where Id = @Id{0};", count);
                    builder.AddUnquotedParameter(string.Format("@Id{0}", count), tag.Id);
                }

                if (count == 0)
                    return;

                ExecuteTransaction(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  Delete(IEnumerable<ITag>)", ex);
                throw;
            }
        }
    }
}
