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
        public SQLiteTagRepository(ILogger logger, ITagTypeFactory typeFactory)
            : base(logger, connectionString)
        {
            if (typeFactory == null)
                throw new ArgumentNullException("typeFactory");

            this.typeFactory = typeFactory;
        }

        public SQLiteTagRepository(ILogger logger, ITagTypeFactory typeFactory, IDbConnection defaultConnection)
            : base(logger, connectionString, defaultConnection)
        {
            if (typeFactory == null)
                throw new ArgumentNullException("typeFactory");

            this.typeFactory = typeFactory;
        }

        private const string connectionString = "Data Source=Tag.db;Version=3;";
        private readonly ITagTypeFactory typeFactory;

        #region Private Methods

        private ITag ReadTag(IDataRecord record)
        {
            var target = record.GetUri("Target");
            var type = record.GetInt32Lookup<ITagType>("Type", typeId => typeFactory.Create(typeId));
            var id = record.GetInt64("Id");
            object value = null;

            if (type.Domain.BaseType == typeof(byte[]))
            {
                value = record.GetBytes("Data");
            }
            else
            {
                var name = record.GetString("Name");
                value = type.Domain.GetValue(name);
            }

            return new Tag(target, type, value, id);
        }

        #endregion

        public ITag GetById(long id)
        {
            try
            {
                logger.Info("SQLiteTagRepository.GetById");

                var builder = new CommandBuilder("select Id, Target, Type, Name, Data from Tag where Id = @Id;");
                builder.AddUnquotedParameter("@Id", id);

                return GetRecord(builder, record => ReadTag(record));
            }
            catch (Exception ex)
            {
                logger.Error("  GetById", ex);
                throw;
            }
        }

        public IEnumerable<ITag> GetByTarget(Uri target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            try
            {
                logger.Info("SQLiteTagRepository.GetByTarget(Uri)");

                var builder = new CommandBuilder("select Id, Target, Type, Name, Data from Tag where Target = @Target;");
                builder.AddQuotedParameter("@Target", target.ToString());

                return GetRecords(builder, record => ReadTag(record));
            }
            catch (Exception ex)
            {
                logger.Error("  GetByTarget(Uri)", ex);
                throw;
            }
        }

        public IEnumerable<ITag> GetByTarget(Uri target, ITagSchema schema)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (schema == null)
                throw new ArgumentNullException("schema");

            try
            {
                logger.Info("SQLiteTagRepository.GetByTarget(Uri, ITagSchema)");

                var builder = new CommandBuilder("select Id, Target, Type, Name, Data from Tag where Target = @Target and Schema = @Schema;");
                builder.AddQuotedParameter("@Target", target.ToString());
                builder.AddUnquotedParameter("@Schema", schema.Id);

                return GetRecords(builder, record => ReadTag(record));
            }
            catch (Exception ex)
            {
                logger.Error("  GetByTarget(Uri, ITagSchema)", ex);
                throw;
            }
        }

        public IEnumerable<ITag> GetByTarget(Uri target, ITagType type)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (type == null)
                throw new ArgumentNullException("type");

            try
            {
                logger.Info("SQLiteTagRepository.GetByTarget(Uri, ITagType)");

                var builder = new CommandBuilder("select Id, Target, Type, Name, Data from Tag where Target = @Target and Type = @Type;");
                builder.AddQuotedParameter("@Target", target.ToString());
                builder.AddUnquotedParameter("@Type", type.Id);

                return GetRecords(builder, record => ReadTag(record));
            }
            catch (Exception ex)
            {
                logger.Error("  GetByTarget(Uri, ITagType)", ex);
                throw;
            }
        }

        public IEnumerable<ITag> GetByAlgorithm(IAlgorithm algorithm, ITagDomain domain, string name)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (domain == null)
                throw new ArgumentNullException("domain");
            if (name == null)
                throw new ArgumentNullException("name");

            try
            {
                logger.Info("SQLiteTagRepository.GetByAlgorithm(IAlgorithm, ITagDomain, string)");

                var builder = new CommandBuilder("select Id, Target, Type, Name, Data from Tag where Algorithm = @Algorithm and Domain = @Domain and Name like @Name;");
                builder.AddUnquotedParameter("@Algorithm", algorithm.Id);
                builder.AddUnquotedParameter("@Domain", domain.Id);
                builder.AddQuotedParameter("@Name", name);

                return GetRecords(builder, record => ReadTag(record));
            }
            catch (Exception ex)
            {
                logger.Error("  GetByAlgorithm(IAlgorithm, ITagDomain, string)", ex);
                throw;
            }
        }

        public void Initialize()
        {
            try
            {
                logger.Info("SQLiteTagRepository.Initialize");

                var builder = new CommandBuilder();
                builder.AppendLine("create table if not exists Tag (Id integer primary key not null, Target text not null, Algorithm integer not null, Schema integer not null, Domain integer not null, Type integer not null, Name text not null, Data blob);");
                builder.AppendLine("create index if not exists Tag_Target on Tag (Target asc);");
                builder.AppendLine("create index if not exists Tag_Target_Schema on Tag (Target asc, Schema asc);");
                builder.AppendLine("create index if not exists Tag_Target_Type on Tag (Target asc, Type asc);");
                builder.AppendLine("create index if not exists Tag_Algorithm_Domain_Name on Tag (Algorithm asc, Domain asc, Name asc);");
                

                //builder.AppendLine("create index if not exists Tag_Target_Algorithm on Tag (Target asc, Algorithm asc);");
                
                //builder.AppendLine("create index if not exists Tag_Algorithm on Tag (Algorithm asc);");
                //builder.AppendLine("create index if not exists Tag_Algorithm_Schema on Tag (Algorithm asc, Schema asc);");
                
                //builder.AppendLine("create index if not exists Tag_Algorithm_Type on Tag (Algorithm asc, Type asc);");
                //builder.AppendLine("create index if not exists Tag_Algorithm_Schema_Name on Tag (Algorithm asc, Schema asc, Name asc);");
                //builder.AppendLine("create index if not exists Tag_Algorithm_Schema_Domain_Name on Tag (Algorithm asc, Schema asc, Domain asc, Name asc);");
                //builder.AppendLine("create index if not exists Tag_Algorithm_Name on Tag (Algorithm asc, Name asc);");
                //builder.AppendLine("create index if not exists Tag_Name on Tag (Name asc);");

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

            Save(new List<ITag> { tag });
        }

        public void Save(IEnumerable<ITag> tags)
        {
            if (tags == null)
                throw new ArgumentNullException("tags");

            try
            {
                logger.Info("SQLiteTagRepository.Save(IEnumerable<ITag>)");

                var builders = new List<ICommandBuilder>();

                foreach (var tag in tags)
                {
                    var builder = new CommandBuilder();
                    builder.AppendLine("insert into Tag (Target, Algorithm, Schema, Domain, Type, Name, Data) values (@Target, @Algorithm, @Schema, @Domain, @Type, @Name, @Data);");
                    builder.AddQuotedParameter("@Target", tag.Target.ToString());
                    builder.AddUnquotedParameter("@Algorithm", tag.Type.Algorithm.Id);
                    builder.AddUnquotedParameter("@Schema", tag.Type.Schema.Id);
                    builder.AddUnquotedParameter("@Domain", tag.Type.Domain.Id);
                    builder.AddUnquotedParameter("@Type", tag.Type.Id);
                    builder.AddQuotedParameter("@Name", tag.Name);
                    
                    var data = tag.Type.Domain.BaseType == typeof(byte[]) ?
                        tag.Value
                        : new byte[0];

                    builder.AddUnquotedParameter("@Data", data);
                    builders.Add(builder);
                }

                if (builders.Count == 0)
                    return;

                ExecuteTransaction(builders);
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

            Delete(new List<ITag> { tag });
        }

        public void Delete(IEnumerable<ITag> tags)
        {
            if (tags == null)
                throw new ArgumentNullException("tags");

            try
            {
                logger.Info("SQLiteTagRepository.Delete(IEnumerable<ITag>)");

                var builders = new List<ICommandBuilder>();

                foreach (var tag in tags)
                {
                    var builder = new CommandBuilder();
                    builder.AppendLine("delete from Tag where Id = @Id;");
                    builder.AddUnquotedParameter("@Id", tag.Id);
                    builders.Add(builder);
                }

                if (builders.Count == 0)
                    return;

                ExecuteTransaction(builders);
            }
            catch (Exception ex)
            {
                logger.Error("  Delete(IEnumerable<ITag>)", ex);
                throw;
            }
        }
    }
}
