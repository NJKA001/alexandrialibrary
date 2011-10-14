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
            object value1 = null;

            if (type.Domain.BaseType == typeof(byte[]))
                value1 = record.GetBytes("Value1");
            else if (type.Domain.BaseType == typeof(uint))
                value1 = record.GetUInt32("Value1");
            else
                value1 = record["Value1"];
            
            var value2 = record["Value2"];
            var value3 = record["Value3"];
            var value4 = record["Value4"];
            var value5 = record["Value5"];
            var value6 = record["Value6"];
            var value7 = record["Value7"];
            var value = type.Domain.GetValue(new TagTuple(value1, value2, value3, value4, value5, value6, value7));

            return new Tag(target, type, value, id);
        }

        #endregion

        public ITag GetById(long id)
        {
            try
            {
                logger.Info("SQLiteTagRepository.GetById");

                var builder = new CommandBuilder("select * from Tag where Id = @Id;");
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

                var builder = new CommandBuilder("select * from Tag where Target = @Target;");
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

                var builder = new CommandBuilder("select * Tag where Target = @Target and Schema = @Schema;");
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

                var builder = new CommandBuilder("select * from Tag where Target = @Target and Type = @Type;");
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

        public IEnumerable<ITag> GetByDomain(ITagDomain domain, string name)
        {
            if (domain == null)
                throw new ArgumentNullException("domain");
            if (name == null)
                throw new ArgumentNullException("name");

            try
            {
                logger.Info("SQLiteTagRepository.GetByAlgorithm(IAlgorithm, ITagDomain, string)");

                var builder = new CommandBuilder("select * from Tag where Domain = @Domain and Value1 like @Name;");
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
                builder.AppendLine("create table if not exists Tag (Id integer primary key not null, Target text not null, Algorithm integer not null, Schema integer not null, Domain integer not null, Type integer not null, Value1 numeric not null, Value2 numeric, Value3 numeric, Value4 numeric, Value5 numeric, Value6 numeric, Value7 numeric);");
                builder.AppendLine("create index if not exists Tag_Target on Tag (Target asc);");
                builder.AppendLine("create index if not exists Tag_Target_Schema on Tag (Target asc, Schema asc);");
                builder.AppendLine("create index if not exists Tag_Target_Type on Tag (Target asc, Type asc);");
                builder.AppendLine("create index if not exists Tag_Domain_Value1 on Tag (Algorithm asc, Domain asc, Value1 asc);");
                builder.AppendLine("create index if not exists Tag_Domain_Value2 on Tag (Algorithm asc, Domain asc, Value2 asc);");
                builder.AppendLine("create index if not exists Tag_Domain_Value3 on Tag (Algorithm asc, Domain asc, Value3 asc);");
                builder.AppendLine("create index if not exists Tag_Domain_Value4 on Tag (Algorithm asc, Domain asc, Value4 asc);");
                builder.AppendLine("create index if not exists Tag_Domain_Value5 on Tag (Algorithm asc, Domain asc, Value5 asc);");
                builder.AppendLine("create index if not exists Tag_Domain_Value6 on Tag (Algorithm asc, Domain asc, Value6 asc);");
                builder.AppendLine("create index if not exists Tag_Domain_Value7 on Tag (Algorithm asc, Domain asc, Value7 asc);");

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
                    builder.AppendLine("insert into Tag (Target, Algorithm, Schema, Domain, Type, Value1, Value2, Value3, Value4, Value5, Value6, Value7) values (@Target, @Algorithm, @Schema, @Domain, @Type, @Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7);");
                    builder.AddQuotedParameter("@Target", tag.Target.ToString());
                    builder.AddUnquotedParameter("@Algorithm", tag.Type.Schema.Algorithm.Id);
                    builder.AddUnquotedParameter("@Schema", tag.Type.Schema.Id);
                    builder.AddUnquotedParameter("@Domain", tag.Type.Domain.Id);
                    builder.AddUnquotedParameter("@Type", tag.Type.Id);


                    if (tag.Type.Domain.BaseType == typeof(string) || tag.Type.Domain.BaseType == typeof(string[]))
                    {
                        builder.AddQuotedParameter("@Value1", tag.Tuple.Item1);
                        builder.AddQuotedParameter("@Value2", tag.Tuple.Item2);
                        builder.AddQuotedParameter("@Value3", tag.Tuple.Item3);
                        builder.AddQuotedParameter("@Value4", tag.Tuple.Item4);
                        builder.AddQuotedParameter("@Value5", tag.Tuple.Item5);
                        builder.AddQuotedParameter("@Value6", tag.Tuple.Item6);
                        builder.AddQuotedParameter("@Value7", tag.Tuple.Item7);
                    }
                    else
                    {
                        builder.AddUnquotedParameter("@Value1", tag.Tuple.Item1);
                        builder.AddUnquotedParameter("@Value2", tag.Tuple.Item2);
                        builder.AddUnquotedParameter("@Value3", tag.Tuple.Item3);
                        builder.AddUnquotedParameter("@Value4", tag.Tuple.Item4);
                        builder.AddUnquotedParameter("@Value5", tag.Tuple.Item5);
                        builder.AddUnquotedParameter("@Value6", tag.Tuple.Item6);
                        builder.AddUnquotedParameter("@Value7", tag.Tuple.Item7);
                    }
                    
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
