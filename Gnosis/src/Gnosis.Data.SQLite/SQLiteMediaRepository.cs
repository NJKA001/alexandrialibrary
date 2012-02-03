using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data.SQLite
{
    public class SQLiteMediaRepository
        : SQLiteRepositoryBase, IMediaRepository
    {
        public SQLiteMediaRepository(ILogger logger, IMediaFactory mediaFactory)
            : base(logger)
        {
            if (mediaFactory == null)
                throw new ArgumentNullException("mediaFactory");

            this.mediaFactory = mediaFactory;
        }

        public SQLiteMediaRepository(ILogger logger, IMediaFactory mediaFactory, IDbConnection defaultConnection)
            : base(logger, defaultConnection)
        {
            if (mediaFactory == null)
                throw new ArgumentNullException("mediaFactory");

            this.mediaFactory = mediaFactory;
        }

        private readonly IMediaFactory mediaFactory;

        private IEnumerable<IMedia> GetMedia(ICommandBuilder builder)
        {
            IDbConnection connection = null;
            var media = new List<IMedia>();

            try
            {
                connection = GetConnection();

                var command = builder.ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var medium = ReadMedium(reader);
                        media.Add(medium);
                    }
                }

                return media;
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        private IMedia ReadMedium(IDataRecord record)
        {
            Uri location = record.GetUri("Location");
            return mediaFactory.Create(location);
            //var type = record.GetStringLookup<IContentType>("Type", code => mediaFactory.GetByCode(code));
        }

        public IMedia Lookup(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            try
            {
                //logger.Info("SQLiteMediaRepository.Lookup(Uri)");

                var builder = new CommandBuilder("select * from Media where Location = @Location;");
                builder.AddParameter("@Location", location.ToString());

                return GetRecord(builder, record => ReadMedium(record));
            }
            catch (Exception ex)
            {
                logger.Error("  Lookup", ex);
                throw;
            }
        }

        public IEnumerable<IMedia> ByLocation(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            try
            {
                var builder = new CommandBuilder("select * from Media where Location like @Location;");
                builder.AddParameter("@Location", pattern);

                return GetMedia(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  SQLiteMediaRepository.ByLocation", ex);
                throw;
            }
        }

        //public IEnumerable<IMedia> All()
        //{
        //    try
        //    {
        //        logger.Info("SQLiteMediaRepository.All()");

        //        var builder = new CommandBuilder("select * from Media;");

        //        return GetRecords(builder, record => ReadMedia(record));
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("  All", ex);
        //        throw;
        //    }
        //}

        public void Initialize()
        {
            try
            {
                logger.Info("SQLiteMediaRepository.Initialize()");

                var builder = new CommandBuilder();

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

        //public void Delete(IMedia media)
        //{
        //    if (media == null)
        //        throw new ArgumentNullException("media");

        //    Delete(new List<IMedia> { media });
        //}

        public void Delete(IEnumerable<Uri> locations)
        {
            if (locations == null)
                throw new ArgumentNullException("locations");

            try
            {
                logger.Info("SQLiteMediaRepository.Delete(IEnumerable<IMedia>)");

                var builders = new List<ICommandBuilder>();

                foreach (var location in locations)
                {
                    var builder = new CommandBuilder();
                    builder.AppendLine("delete from Media where Location = @Location;");
                    builder.AddParameter("@Location", location.ToString());
                    builders.Add(builder);
                }

                if (builders.Count == 0)
                    return;

                ExecuteTransaction(builders);
            }
            catch (Exception ex)
            {
                logger.Error(" Delete(IEnumerable<IMedia>)", ex);
                throw;
            }
        }

        //public void Save(IMedia media)
        //{
        //    if (media == null)
        //        throw new ArgumentNullException("media");

        //    try
        //    {
        //        logger.Info("SQLiteMediaRepository.Save(IMedia)");

        //        var builder = new CommandBuilder();
        //        builder.Append("insert into Media (Location, Type) values (@Location, @Type);");
        //        builder.AddParameter("@Location", media.Location.ToString());
        //        builder.AddParameter("@Type", media.Type.ToString());

        //        ExecuteNonQuery(builder);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("  Save(IMedia)", ex);
        //        throw;
        //    }
        //}

        public void Save(IEnumerable<IMedia> media)
        {
            if (media == null)
                throw new ArgumentNullException("media");

            try
            {
                logger.Info("SQLiteMediaRepository.Save(IEnumerable<IMedia>)");

                var builders = new List<ICommandBuilder>();

                foreach (var medium in media)
                {
                    var builder = new CommandBuilder();
                    builder.AppendLine("replace into Media (Location, Type) values (@Location, @Type);");
                    builder.AddParameter("@Location", medium.Location.ToString());
                    builder.AddParameter("@Type", medium.Type.ToString());
                    builders.Add(builder);
                }

                if (builders.Count == 0)
                    return;

                ExecuteTransaction(builders);
            }
            catch (Exception ex)
            {
                logger.Error("  Save(IEnumerable<IMedia>)", ex);
                throw;
            }
        }
    }
}
