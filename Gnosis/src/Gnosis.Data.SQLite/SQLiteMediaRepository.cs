using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Data.SQLite
{
    public class SQLiteMediaRepository
        : SQLiteRepositoryBase
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
            Uri location = null;
            try
            {
                location = record.GetUri("Location");
                //var l = record.GetString("Location");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("URL=" + record.GetString("Location"));
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            //Uri location = new Uri("http://cnn.com");
            var type = record.GetStringLookup<IMediaType>("Type", typeName => MediaType.Parse(typeName));
            return factory.Create(location, type);
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
                logger.Info("SQLiteMediaRepository.Initialize");

                var builder = new SimpleCommandBuilder();

                builder.AppendLine("create table if not exists Media (Location text not null primary key, Type text not null);");
                builder.AppendLine("create index if not exists Media_Type on Media (Type asc);");

                ExecuteNonQuery(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  SQLiteMediaRepository.Initialize", ex);
            }
        }

        public IMedia Lookup(Uri location)
        {
            try
            {
                logger.Info("SQLiteMediaRepository.Lookup");

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

        public void Save(IMedia media)
        {
            if (media == null)
                throw new ArgumentNullException("media");

            try
            {
                logger.Info("SQLiteMediaRepository.Save(IMedia)");

                var builder = new SimpleCommandBuilder();
                builder.Append("insert into Media (Location, Type) values (@Location, @Type);");
                builder.AddUnquotedParameter("@Location", media.Location.ToString());
                builder.AddUnquotedParameter("@Type", media.Type.ToString());

                ExecuteNonQuery(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  Save(IMedia)", ex);
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
                    builder.AddUnquotedParameter(string.Format("@Type{0}", count), medium.Type.ToString());
                }

                if (count == 0)
                    return;

                ExecuteTransaction(builder);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("  SQLiteMediaRepository.Save(IEnumerable<IMedia>) Failed: " + ex.Message);
                logger.Error("  Save(IEnumerable<IMedia>)", ex);
                throw;
            }
        }
    }
}
