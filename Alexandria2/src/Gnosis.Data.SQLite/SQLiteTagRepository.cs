using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gnosis.Tags;
using Gnosis.Tasks;
using Gnosis.Data;

namespace Gnosis.Data.SQLite
{
    public class SQLiteTagRepository
        : SQLiteRepositoryBase, ITagRepository
    {
        public SQLiteTagRepository(ILogger logger, ITagTypeFactory typeFactory)
            : base(logger)
        {
            if (typeFactory == null)
                throw new ArgumentNullException("typeFactory");

            this.typeFactory = typeFactory;
        }

        public SQLiteTagRepository(ILogger logger, ITagTypeFactory typeFactory, IDbConnection defaultConnection)
            : base(logger, defaultConnection)
        {
            if (typeFactory == null)
                throw new ArgumentNullException("typeFactory");

            this.typeFactory = typeFactory;
        }

        
        private readonly ITagTypeFactory typeFactory;

        #region Private Methods

        private IEnumerable<Gnosis.ITag> GetTags(ICommandBuilder builder)
        {
            IDbConnection connection = null;
            var tags = new List<Gnosis.ITag>();

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

        private Gnosis.ITag ReadTag(IDataRecord record)
        {
            var id = record.GetInt64("Id");
            var target = record.GetUri("Target");
            var algorithm = record.GetInt32Lookup<IAlgorithm>("Algorithm", algorithmId => Algorithms.Algorithm.Parse(algorithmId));
            var type = record.GetInt32Lookup<ITagType>("Type", typeId => typeFactory.Create(typeId));
            var value = record.GetString("Value");
            var data = record.GetBytes("Data");

            return new Gnosis.Tags.Tag(target, type, value, algorithm, data, id);
        }

        //private IEnumerable<ITag> GetId3v1SimpleGenreTags(IAlgorithm algorithm, string pattern)
        //{
        //    var builder = new CommandBuilder("select * from Tag where Algorithm = @Algorithm and Domain = @Domain and Value1 like @Pattern;");
        //    builder.AddParameter("@Algorithm", algorithm.Id);
        //    builder.AddParameter("@Domain", TagDomain.Id3v1SimpleGenre.Id);
        //    builder.AddParameter("@Pattern", pattern);
        //    return GetTags(builder);
        //}

        //private IEnumerable<ITag> GetStringTags(IAlgorithm algorithm, string pattern)
        //{
        //    var builder = new CommandBuilder("select * from Tag where Algorithm = @Algorithm and Domain = @Domain and Value1 like @Pattern;");
        //    builder.AddParameter("@Algorithm", algorithm.Id);
        //    builder.AddParameter("@Domain", TagDomain.String.Id);
        //    builder.AddParameter("@Pattern", pattern);
        //    return GetTags(builder);
        //}

        /*
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
        */

        #endregion

        public Gnosis.ITag GetById(long id)
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

        public IEnumerable<Gnosis.ITag> GetByTarget(Uri target)
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

        public IEnumerable<Gnosis.ITag> GetByTarget(Uri target, TagDomain domain)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            try
            {
                logger.Info("SQLiteTagRepository.GetByTarget(Uri, TagDomain)");

                var builder = new CommandBuilder("select * Tag where Target = @Target and Domain = @Domain;");
                builder.AddParameter("@Target", target.ToString());
                builder.AddParameter("@Domain", (int)domain);

                return GetTags(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetByTarget(Uri, TagDomain)", ex);
                throw;
            }
        }

        public IEnumerable<Gnosis.ITag> GetByTarget(Uri target, ITagType type)
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

        public IEnumerable<Gnosis.ITag> GetByAlgorithm(IAlgorithm algorithm, TagDomain domain, string pattern)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            try
            {
                logger.Info("SQLiteTagRepository.GetByAlgorithm(IAlgorithm, TagDomain, string)");

                var builder = new CommandBuilder("select * from Tag where Algorithm = @Algorithm and Domain = @Domain and Value like @Pattern;");
                builder.AddParameter("@Algorithm", algorithm.Id);
                builder.AddParameter("@Domain", (int)domain);
                builder.AddParameter("@Pattern", pattern);

                return GetTags(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetByAlgorithm(IAlgorithm, TagDomain, string)", ex);
                throw;
            }
        }

        public ITask<IEnumerable<Gnosis.ITag>> Search(IAlgorithm algorithm, string pattern)
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
                builder.AppendLine("create table if not exists Tag (Id integer primary key not null, Target text not null, Algorithm integer not null, Domain integer not null, Type integer not null, Value text not null, Data blob not null);");
                builder.AppendLine("create index if not exists Tag_Target on Tag (Target asc);");
                builder.AppendLine("create index if not exists Tag_Target_Domain on Tag (Target asc, Domain asc);");
                builder.AppendLine("create unique index if not exists Tag_Target_Algorithm_Type_Value on Tag (Target asc, Algorithm asc, Type asc, Value asc);");
                builder.AppendLine("create index if not exists Tag_Target_Type on Tag (Target asc, Type asc);");
                builder.AppendLine("create index if not exists Tag_Target_Type_Value on Tag (Target asc, Type asc, Value asc);");
                builder.AppendLine("create index if not exists Tag_Algorithm_Domain_Value on Tag (Algorithm asc, Domain asc, Value asc);");

                ExecuteNonQuery(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  Initialize", ex);
                throw;
            }
        }

        public void Save(IEnumerable<Gnosis.ITag> tags)
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
                    builder.AppendLine("replace into Tag (Id, Target, Algorithm, Domain, Type, Value, Data) values (@Id, @Target, @Algorithm, @Domain, @Type, @Value, @Data);");
                    builder.AddParameter("@Id", tag.Id > 0 ? (object)tag.Id : (object)DBNull.Value);
                    builder.AddParameter("@Target", tag.Target.ToString());
                    builder.AddParameter("@Algorithm", tag.Algorithm.Id);
                    builder.AddParameter("@Domain", (int)tag.Type.Domain);
                    builder.AddParameter("@Type", tag.Type.Id);
                    builder.AddParameter("@Value", tag.Value);
                    builder.AddParameter("@Data", tag.Data);

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

        public void Overwrite(Uri target, ITagType type, IEnumerable<Gnosis.ITag> tags)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (type == null)
                throw new ArgumentNullException("type");

            try
            {
                var builders = new List<ICommandBuilder>();

                var deleteBuilder = new CommandBuilder("delete from Tag where Target = @Target and Type = @Type;");
                deleteBuilder.AddParameter("@Target", target.ToString());
                deleteBuilder.AddParameter("@Type", type.Id);
                builders.Add(deleteBuilder);
                
                foreach (var tag in tags.Where(x => x.Target == target && x.Type == type))
                {
                    var insertBuilder = new CommandBuilder("insert into Tag (Target, Algorithm, Domain, Type, Value, Data) values (@Target, @Algorithm, @Domain, @Type, @Value, @Data);");
                    insertBuilder.AddParameter("@Target", tag.Target.ToString());
                    insertBuilder.AddParameter("@Algorithm", tag.Algorithm.Id);
                    insertBuilder.AddParameter("@Domain", (int)tag.Type.Domain);
                    insertBuilder.AddParameter("@Type", tag.Type.Id);
                    insertBuilder.AddParameter("@Value", tag.Value);
                    insertBuilder.AddParameter("@Data", tag.Data);

                    builders.Add(insertBuilder);
                }

                if (builders.Count < 2)
                    return;

                ExecuteTransaction(builders);
            }
            catch (Exception ex)
            {
                logger.Error("  Overwrite", ex);
                throw;
            }
        }
    }
}
