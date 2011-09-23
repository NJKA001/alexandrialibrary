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
        : SQLiteRepositoryBase
    {
        public SQLiteTagRepository(ILogger logger)
            : base(logger, connectionString)
        {
        }

        public SQLiteTagRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, connectionString, defaultConnection)
        {
        }

        private const string connectionString = "Data Source=Tag.db;Version=3;";

        #region Private Methods

        private ITag ReadTag(IDataRecord record)
        {
            var target = record.GetUri("Target");
            var type = record.GetInt64Lookup<ITagType>("Type", typeId => TagType.Parse(typeId));
            var name = record.GetString("Name");
            var id = record.GetInt64("Id");
            return new Tag(target, type, name, id);
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

        public void Initialize()
        {
            try
            {
                logger.Info("SQLiteTagRepository.Initialize");

                var builder = new SimpleCommandBuilder();
                builder.AppendLine("create table if not exists Tag (Id integer primary key not null, Name text not null, Type integer not null, Target text not null);");
                builder.AppendLine("create index if not exists Tag_Name on Tag (Name asc);");
                builder.AppendLine("create index if not exists Tag_Type on Tag (Type asc);");
                builder.AppendLine("create index if not exists Tag_Type_Name on Tag (Type asc, Name asc);");
                builder.AppendLine("create index if not exists Tag_Target on Tag (Target asc);");
                builder.AppendLine("create index if not exists Tag_Target_Type on Tag (Target asc, Type asc);");

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
                builder.Append("insert into Tag (Target, Type, Name) values (@Target, @Type, @Name);");
                builder.AddQuotedParameter("@Target", tag.Target.ToString());
                builder.AddUnquotedParameter("@Type", tag.Type.Id);
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
                    builder.AppendFormatLine("insert into Tag (Target, Type, Name) values (@Target{0}, @Type{0}, @Name{0});", count);
                    builder.AddQuotedParameter(string.Format("@Target{0}", count), tag.Target.ToString());
                    builder.AddUnquotedParameter(string.Format("@Type{0}", count), tag.Type.Id);
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
