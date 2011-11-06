using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gnosis.Tags;
using Gnosis.Tasks;
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

        private IEnumerable<ITag> GetTags(ICommandBuilder builder)
        {
            IDbConnection connection = null;
            var tags = new List<ITag>();

            try
            {
                connection = GetConnection();
                
                var command = builder.ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tag = ReadTag(reader);
                        tags.Add(tag);
                    }
                }
                
                return tags;
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        private ITag ReadTag(IDataRecord record)
        {
            var target = record.GetUri("Target");
            var type = record.GetInt32Lookup<ITagType>("Type", typeId => typeFactory.Create(typeId));
            var id = record.GetInt64("Id");
            var length = type.Domain.BaseTypes.Length;
            object[] values = new object[length];

            for (var i = 0; i < length; i++)
            {
                var columnName = string.Format("Value{0}", i + 1);
                if (type.Domain.BaseTypes[i] == null)
                    values[i] = null;
                else if (type.Domain.BaseTypes[i] == typeof(byte[]))
                    values[i] = record.GetBytes(columnName);
                else if (type.Domain.BaseTypes[i] == typeof(uint))
                    values[i] = record.GetUInt32(columnName);
                else if (type.Domain.BaseTypes[i] == typeof(string))
                    values[i] = record.GetString(columnName);
                else
                    values[i] = record[columnName];

            }

            var value = type.Domain.GetValue(TagTuple.FromArray(values));

            return new Tag(target, type, value, id);
        }

        private IEnumerable<ITag> GetId3v1SimpleGenreTags(IAlgorithm algorithm, string pattern)
        {
            var builder = new CommandBuilder("select * from Tag where Algorithm = @Algorithm and Domain = @Domain and Value1 like @Pattern;");
            builder.AddParameter("@Algorithm", algorithm.Id);
            builder.AddParameter("@Domain", TagDomain.Id3v1SimpleGenre.Id);
            builder.AddParameter("@Pattern", pattern);
            return GetTags(builder);
        }

        private IEnumerable<ITag> GetStringTags(IAlgorithm algorithm, string pattern)
        {
            var builder = new CommandBuilder("select * from Tag where Algorithm = @Algorithm and Domain = @Domain and Value1 like @Pattern;");
            builder.AddParameter("@Algorithm", algorithm.Id);
            builder.AddParameter("@Domain", TagDomain.String.Id);
            builder.AddParameter("@Pattern", pattern);
            return GetTags(builder);
        }

        private IEnumerable<ITag> GetStringArrayTags1(IAlgorithm algorithm, string pattern)
        {
            var builder = new CommandBuilder("select * from Tag where Algorithm = @Algorithm and Domain = @Domain and Value1 like @Pattern;");
            builder.AddParameter("@Algorithm", algorithm.Id);
            builder.AddParameter("@Domain", TagDomain.StringArray.Id);
            builder.AddParameter("@Pattern", pattern);
            return GetTags(builder);
        }

        private IEnumerable<ITag> GetStringArrayTags2(IAlgorithm algorithm, string pattern)
        {
            var builder = new CommandBuilder("select * from Tag where Algorithm = @Algorithm and Domain = @Domain and Value2 like @Pattern;");
            builder.AddParameter("@Algorithm", algorithm.Id);
            builder.AddParameter("@Domain", TagDomain.StringArray.Id);
            builder.AddParameter("@Pattern", pattern);
            return GetTags(builder);
        }

        private IEnumerable<ITag> GetStringArrayTags3(IAlgorithm algorithm, string pattern)
        {
            var builder = new CommandBuilder("select * from Tag where Algorithm = @Algorithm and Domain = @Domain and Value3 like @Pattern;");
            builder.AddParameter("@Algorithm", algorithm.Id);
            builder.AddParameter("@Domain", TagDomain.StringArray.Id);
            builder.AddParameter("@Pattern", pattern);
            return GetTags(builder);
        }

        private IEnumerable<ITag> GetStringArrayTags4(IAlgorithm algorithm, string pattern)
        {
            var builder = new CommandBuilder("select * from Tag where Algorithm = @Algorithm and Domain = @Domain and Value4 like @Pattern;");
            builder.AddParameter("@Algorithm", algorithm.Id);
            builder.AddParameter("@Domain", TagDomain.StringArray.Id);
            builder.AddParameter("@Pattern", pattern);
            return GetTags(builder);
        }

        private IEnumerable<ITag> GetStringArrayTags5(IAlgorithm algorithm, string pattern)
        {
            var builder = new CommandBuilder("select * from Tag where Algorithm = @Algorithm and Domain = @Domain and Value5 like @Pattern;");
            builder.AddParameter("@Algorithm", algorithm.Id);
            builder.AddParameter("@Domain", TagDomain.StringArray.Id);
            builder.AddParameter("@Pattern", pattern);
            return GetTags(builder);
        }

        private IEnumerable<ITag> GetStringArrayTags6(IAlgorithm algorithm, string pattern)
        {
            var builder = new CommandBuilder("select * from Tag where Algorithm = @Algorithm and Domain = @Domain and Value6 like @Pattern;");
            builder.AddParameter("@Algorithm", algorithm.Id);
            builder.AddParameter("@Domain", TagDomain.StringArray.Id);
            builder.AddParameter("@Pattern", pattern);
            return GetTags(builder);
        }

        private IEnumerable<ITag> GetStringArrayTags7(IAlgorithm algorithm, string pattern)
        {
            var builder = new CommandBuilder("select * from Tag where Algorithm = @Algorithm and Domain = @Domain and Value7 like @Pattern;");
            builder.AddParameter("@Algorithm", algorithm.Id);
            builder.AddParameter("@Domain", TagDomain.StringArray.Id);
            builder.AddParameter("@Pattern", pattern);
            return GetTags(builder);
        }

        #endregion

        public ITag GetById(long id)
        {
            try
            {
                logger.Info("SQLiteTagRepository.GetById");

                var builder = new CommandBuilder("select * from Tag where Id = @Id;");
                builder.AddParameter("@Id", id);

                return GetTags(builder).FirstOrDefault();
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
                builder.AddParameter("@Target", target.ToString());

                return GetTags(builder);
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
                builder.AddParameter("@Target", target.ToString());
                builder.AddParameter("@Schema", schema.Id);

                return GetTags(builder);
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
                builder.AddParameter("@Target", target.ToString());
                builder.AddParameter("@Type", type.Id);

                return GetTags(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetByTarget(Uri, ITagType)", ex);
                throw;
            }
        }

        public IEnumerable<ITag> GetByAlgorithm(IAlgorithm algorithm, ITagDomain domain, string pattern)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (domain == null)
                throw new ArgumentNullException("domain");
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            try
            {
                logger.Info("SQLiteTagRepository.GetByAlgorithm(IAlgorithm, ITagDomain, string)");

                if (domain == TagDomain.StringArray)
                {
                    var tags = new List<ITag>();

                    for (var i = 1; i < 8; i++)
                    {
                        var builder = new CommandBuilder(string.Format("select * from Tag where Algorithm = @Algorithm and Domain = @Domain and Value{0} like @Pattern;", i));
                        builder.AddParameter("@Algorithm", algorithm.Id);
                        builder.AddParameter("@Domain", domain.Id);
                        builder.AddParameter("@Pattern", pattern);

                        tags.AddRange(GetTags(builder));
                    }

                    return tags;
                }
                else
                {
                    var builder = new CommandBuilder("select * from Tag where Algorithm = @Algorithm and Domain = @Domain and Value1 like @Pattern;");
                    builder.AddParameter("@Algorithm", algorithm.Id);
                    builder.AddParameter("@Domain", domain.Id);
                    builder.AddParameter("@Pattern", pattern);

                    return GetTags(builder);
                }
            }
            catch (Exception ex)
            {
                logger.Error("  GetByAlgorithm(IAlgorithm, ITagDomain, string)", ex);
                throw;
            }
        }

        private void StartSearch(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var options = e.Argument as SearchOptions;
            if (options == null)
                return;

            options.TagCallback(GetStringTags(options.Algorithm, options.Pattern));

            if (e.Cancel)
                return;

            options.TagCallback(GetStringArrayTags1(options.Algorithm, options.Pattern));

            if (e.Cancel)
                return;

            options.TagCallback(GetStringArrayTags2(options.Algorithm, options.Pattern));

            if (e.Cancel)
                return;

            options.TagCallback(GetStringArrayTags3(options.Algorithm, options.Pattern));

            if (e.Cancel)
                return;

            options.TagCallback(GetStringArrayTags4(options.Algorithm, options.Pattern));

            if (e.Cancel)
                return;

            options.TagCallback(GetStringArrayTags5(options.Algorithm, options.Pattern));

            if (e.Cancel)
                return;

            options.TagCallback(GetStringArrayTags6(options.Algorithm, options.Pattern));

            if (e.Cancel)
                return;

            options.TagCallback(GetStringArrayTags7(options.Algorithm, options.Pattern));

            if (e.Cancel)
                return;

            options.TagCallback(GetId3v1SimpleGenreTags(options.Algorithm, options.Pattern));

            if (e.Cancel)
                return;

            if (options.CompletedCallback != null)
                options.CompletedCallback();
        }

        private class SearchOptions
        {
            public SearchOptions(IAlgorithm algorithm, string pattern, Action<IEnumerable<ITag>> tagCallback, Action completedCallback)
            {
                Algorithm = algorithm;
                Pattern = pattern;
                TagCallback = tagCallback;
                CompletedCallback = completedCallback;
            }

            public IAlgorithm Algorithm { get; set; }
            public string Pattern { get; set; }
            public Action<IEnumerable<ITag>> TagCallback { get; set; }
            public Action CompletedCallback { get; set; }
        }

        public ITask<IEnumerable<ITag>> Search(IAlgorithm algorithm, string pattern)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            try
            {
                return new TagSearchTask(logger, this, algorithm, pattern);
            }
            catch (Exception ex)
            {
                logger.Error("  Search", ex);
                throw;
            }
        }

        public void Initialize()
        {
            try
            {
                logger.Info("SQLiteTagRepository.Initialize");

                var builder = new CommandBuilder();
                builder.AppendLine("create table if not exists Tag (Id integer primary key not null, Target text not null, Algorithm integer not null, Schema integer not null, Domain integer not null, Type integer not null, Value1 text not null, Value2 text, Value3 text, Value4 text, Value5 text, Value6 text, Value7 text);");
                builder.AppendLine("create index if not exists Tag_Target on Tag (Target asc);");
                builder.AppendLine("create index if not exists Tag_Target_Schema on Tag (Target asc, Schema asc);");
                builder.AppendLine("create unique index if not exists Tag_Target_Type on Tag (Target asc, Type asc);");
                builder.AppendLine("create index if not exists Tag_Algorithm_Domain_Value1 on Tag (Algorithm asc, Domain asc, Value1 asc);");
                builder.AppendLine("create index if not exists Tag_Algorithm_Domain_Value2 on Tag (Algorithm asc, Domain asc, Value2 asc);");
                builder.AppendLine("create index if not exists Tag_Algorithm_Domain_Value3 on Tag (Algorithm asc, Domain asc, Value3 asc);");
                builder.AppendLine("create index if not exists Tag_Algorithm_Domain_Value4 on Tag (Algorithm asc, Domain asc, Value4 asc);");
                builder.AppendLine("create index if not exists Tag_Algorithm_Domain_Value5 on Tag (Algorithm asc, Domain asc, Value5 asc);");
                builder.AppendLine("create index if not exists Tag_Algorithm_Domain_Value6 on Tag (Algorithm asc, Domain asc, Value6 asc);");
                builder.AppendLine("create index if not exists Tag_Algorithm_Domain_Value7 on Tag (Algorithm asc, Domain asc, Value7 asc);");

                ExecuteNonQuery(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  Initialize", ex);
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

                var builders = new List<ICommandBuilder>();

                foreach (var tag in tags)
                {
                    var builder = new CommandBuilder();
                    builder.AppendLine("replace into Tag (Id, Target, Algorithm, Schema, Domain, Type, Value1, Value2, Value3, Value4, Value5, Value6, Value7) values (@Id, @Target, @Algorithm, @Schema, @Domain, @Type, @Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7);");
                    builder.AddParameter("@Id", tag.Id > 0 ? (object)tag.Id : (object)DBNull.Value);
                    builder.AddParameter("@Target", tag.Target.ToString());
                    builder.AddParameter("@Algorithm", tag.Type.Schema.Algorithm.Id);
                    builder.AddParameter("@Schema", tag.Type.Schema.Id);
                    builder.AddParameter("@Domain", tag.Type.Domain.Id);
                    builder.AddParameter("@Type", tag.Type.Id);

                    var values = tag.Tuple.ToArray();
                    for(var i=0; i < values.Length; i++)
                        builder.AddParameter(string.Format("@Value{0}", i + 1), values[i]);

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

        public void Delete(IEnumerable<long> ids)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");

            try
            {
                logger.Info("SQLiteTagRepository.Delete(IEnumerable<long>)");

                var builders = new List<ICommandBuilder>();

                foreach (var id in ids)
                {
                    var builder = new CommandBuilder();
                    builder.AppendLine("delete from Tag where Id = @Id;");
                    builder.AddParameter("@Id", id);
                    builders.Add(builder);
                }

                if (builders.Count == 0)
                    return;

                ExecuteTransaction(builders);
            }
            catch (Exception ex)
            {
                logger.Error("  Delete(IEnumerable<long>)", ex);
                throw;
            }
        }
    }
}
