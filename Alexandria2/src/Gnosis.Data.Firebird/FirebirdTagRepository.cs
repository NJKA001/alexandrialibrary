using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Data.Firebird
{
    public class FirebirdTagRepository
        : FirebirdRepositoryBase
    {
        public FirebirdTagRepository(ILogger logger, ITagTypeFactory typeFactory)
            : base(logger)
        {
            if (typeFactory == null)
                throw new ArgumentNullException("typeFactory");

            this.typeFactory = typeFactory;
        }

        private ITagTypeFactory typeFactory;

        public void Initialize()
        {
            try
            {
                CreateDatabase();

                var commandInfo = new List<Tuple<string, IEnumerable<KeyValuePair<string, object>>>>();

                var tableSql = new StringBuilder();
                tableSql.AppendLine("EXECUTE BLOCK AS BEGIN");
                tableSql.AppendLine("if (not exists(select 1 from rdb$relations where rdb$relation_name = 'TAG')) then");
                tableSql.AppendLine("execute statement 'create table Tag (Id BIGINT NOT NULL, Target VARCHAR(4000) NOT NULL, Algorithm INTEGER NOT NULL, Schema INTEGER NOT NULL, Domain INTEGER NOT NULL, Type INTEGER NOT NULL, Name VARCHAR(4000) NOT NULL, PRIMARY KEY (Id));';");
                tableSql.AppendLine("END");
                Execute(tableSql.ToString());

                var generatorSql = new StringBuilder();
                generatorSql.AppendLine("EXECUTE BLOCK AS BEGIN");
                generatorSql.AppendLine("if (not exists(select 1 from rdb$generators where rdb$generator_name = 'TAG_GENERATOR')) then");
                generatorSql.AppendLine("execute statement 'create generator Tag_Generator;';");
                //generatorSql.AppendLine("set generator Tag_Generator to 0;';");
                generatorSql.AppendLine("END");
                Execute(generatorSql.ToString());
                //commandInfo.Add(new Tuple<string, IEnumerable<KeyValuePair<string, object>>>(generatorSql.ToString(), null));

                var triggerSql = new StringBuilder();
                triggerSql.AppendLine("EXECUTE BLOCK AS BEGIN");
                triggerSql.AppendLine("if (not exists(select 1 from rdb$triggers where rdb$trigger_name = 'TAG_BEFORE_INSERT')) then");
                triggerSql.AppendLine("execute statement 'create trigger Tag_Before_Insert for Tag");
                triggerSql.AppendLine("active before insert position 0");
                triggerSql.AppendLine("as");
                triggerSql.AppendLine("  BEGIN");
                triggerSql.AppendLine("    if (NEW.ID is NULL) then NEW.ID = GEN_ID(Tag_Generator, 1);");
                triggerSql.AppendLine("  END';");
                triggerSql.AppendLine("END");
                Execute(triggerSql.ToString());
                //commandInfo.Add(new Tuple<string, IEnumerable<KeyValuePair<string, object>>>(triggerSql.ToString(), null));

                //Execute(commandInfo);
            }
            catch (Exception ex)
            {
                logger.Error("  Initialize", ex);
                throw;
            }
        }

        public IEnumerable<ITag> Search()
        {
            var tags = new List<ITag>();

            using (var connection = GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from Tag";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var target = reader.GetUri("Target");
                        var type = reader.GetInt32Lookup<ITagType>("Type", typeId => typeFactory.Create(typeId));
                        var name = reader.GetString("Name");
                        var value = type.Domain.GetValue(name);
                        var id = reader.GetInt64("Id");

                        tags.Add(new Tag(target, type, value, id));
                    }
                }
            }

            return tags;
        }

        public int Count()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select count(*) from Tag;";
                var result = command.ExecuteScalar();
                if (result == null)
                    return 0;

                System.Diagnostics.Debug.WriteLine("count=" + result.ToString());

                var count = 0;
                int.TryParse(result.ToString(), out count);
                return count;
            }
        }

        public void Save(IEnumerable<ITag> tags)
        {
            try
            {
                var commandInfo = new List<Tuple<string, IEnumerable<KeyValuePair<string, object>>>>();

                //var count = 0;
                foreach (var tag in tags)
                {
                    //count++;
                    var sql = new StringBuilder();
                    var parameters = new Dictionary<string, object>();
                    sql.AppendLine("insert into Tag (Target, Algorithm, Schema, Domain, Type, Name) values (@Target, @Algorithm, @Schema, @Domain, @Type, @Name)");
                    parameters.Add("@Target", string.Format("'{0}'", tag.Target.ToString())); //tag.Target.IsFile ? tag.Target.LocalPath : tag.Target.ToString()));
                    parameters.Add("@Algorithm", tag.Type.Schema.Algorithm.Id);
                    parameters.Add("@Schema", tag.Type.Schema.Id);
                    parameters.Add("@Domain", tag.Type.Domain.Id);
                    parameters.Add("@Type", tag.Type.Id);
                    parameters.Add("@Name", string.Format("'{0}'", tag.Name));
                    commandInfo.Add(new Tuple<string, IEnumerable<KeyValuePair<string, object>>>(sql.ToString(), parameters));
                    Execute(sql.ToString(), parameters);
                }

                //Execute(sql.ToString(), parameters);
                //Execute(commandInfo);
            }
            catch (Exception ex)
            {
                logger.Error("  Save", ex);
                throw;
            }
        }
    }
}
