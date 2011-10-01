using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Data.SQLite
{
    public class SQLiteMediaRepository
        : SQLiteRepositoryBase, IMediaRepository
    {
        public SQLiteMediaRepository(ILogger logger)
            : base(logger, connectionString)
        {
        }

        public SQLiteMediaRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, connectionString, defaultConnection)
        {
        }

        private const string connectionString = "Data Source=Media.db;Version=3;";
        private readonly IMediaFactory factory = new MediaFactory();

        private IMedia ReadMedia(IDataRecord record)
        {
            Uri location = record.GetUri("Location");
            var type = record.GetStringLookup<IMediaType>("Type", typeName => MediaType.Parse(typeName));
            return factory.Create(location, type);
        }

        public IMedia Lookup(Uri location)
        {
            try
            {
                logger.Info("SQLiteMediaRepository.Lookup(Uri)");

                var builder = new SimpleCommandBuilder("select * from Media where Location = @Location;");
                builder.AddUnquotedParameter("@Location", location.ToString());

                return GetRecord(builder, record => ReadMedia(record));
            }
            catch (Exception ex)
            {
                logger.Error("  Lookup", ex);
                throw;
            }
        }

        public IEnumerable<IMedia> All()
        {
            try
            {
                logger.Info("SQLiteMediaRepository.All()");

                var builder = new SimpleCommandBuilder("select * from Media;");

                return GetRecords(builder, record => ReadMedia(record));
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
                logger.Info("SQLiteMediaRepository.Initialize()");

                var builder = new SimpleCommandBuilder();

                builder.AppendLine("create table if not exists Media (Location text not null primary key, Type text not null);");
                builder.AppendLine("create index if not exists Media_Type on Media (Type asc);");

                ExecuteNonQuery(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  Initialize", ex);
                throw;
            }
        }

        public void Delete(IMedia media)
        {
            if (media == null)
                throw new ArgumentNullException("media");

            try
            {
                logger.Info("SQLiteMediaRepository.Delete(IMedia)");

                var builder = new SimpleCommandBuilder();
                builder.AppendLine("delete from Media where Location = @Location;");
                builder.AddQuotedParameter("@Location", media.Location.ToString());

                ExecuteNonQuery(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  Delete(IMedia)", ex);
                throw;
            }
        }

        public void Delete(IEnumerable<IMedia> media)
        {
            if (media == null)
                throw new ArgumentNullException("media");

            try
            {
                logger.Info("SQLiteMediaRepository.Delete(IEnumerable<IMedia>)");

                var builder = new SimpleCommandBuilder();

                var count = 0;
                foreach (var medium in media)
                {
                    count++;
                    builder.AppendFormatLine("delete from Media where Location = @Location{0};", count);
                    builder.AddQuotedParameter(string.Format("@Location{0}", count), medium.Location.ToString());
                }

                if (count == 0)
                    return;

                ExecuteTransaction(builder);
            }
            catch (Exception ex)
            {
                logger.Error(" Delete(IEnumerable<IMedia>)", ex);
                throw;
            }
        }

        public void Save(IMedia media)
        {
            if (media == null)
                throw new ArgumentNullException("media");

            try
            {
                logger.Info("SQLiteMediaRepository.Save(IMedia)");

                var builder = new SimpleCommandBuilder();
                builder.Append("insert into Media (Location, Type) values (@Location, @Type);");
                builder.AddQuotedParameter("@Location", media.Location.ToString());
                builder.AddQuotedParameter("@Type", media.Type.ToString());

                ExecuteNonQuery(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  Save(IMedia)", ex);
                throw;
            }
        }

        public void Save(IEnumerable<IMedia> media)
        {
            if (media == null)
                throw new ArgumentNullException("media");

            try
            {
                logger.Info("SQLiteMediaRepository.Save(IEnumerable<IMedia>)");

                var builder = new SimpleCommandBuilder();

                var count = 0;
                foreach (var medium in media)
                {
                    count++;
                    builder.AppendFormatLine("insert into Media (Location, Type) values (@Location{0}, @Type{0});", count);
                    builder.AddQuotedParameter(string.Format("@Location{0}", count), medium.Location.ToString());
                    builder.AddQuotedParameter(string.Format("@Type{0}", count), medium.Type.ToString());
                }

                if (count == 0)
                    return;

                ExecuteTransaction(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  Save(IEnumerable<IMedia>)", ex);
                throw;
            }
        }
    }
}
