using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

namespace Gnosis.Data.SQLite
{
    public class SQLiteArtistRepository
        : SQLiteRepositoryBase, IArtistRepository
    {
        public SQLiteArtistRepository(ILogger logger)
            : base(logger)
        {
        }

        public SQLiteArtistRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, defaultConnection)
        {
        }

        private IEnumerable<IArtist> GetArtists(ICommandBuilder builder)
        {
            IDbConnection connection = null;
            var artists = new List<IArtist>();

            try
            {
                connection = GetConnection();

                var command = builder.ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var artist = ReadArtist(reader);
                        artists.Add(artist);
                    }
                }

                return artists;
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        private IArtist ReadArtist(IDataRecord record)
        {
            var location = record.GetUri("Location");
            var name = record.GetString("Name");
            var fromDate = record.GetDateTime("FromDate");
            var toDate = record.GetDateTime("ToDate");
            var creator = record.GetUri("Creator");
            var creatorName = record.GetString("CreatorName");
            var catalog = record.GetUri("Catalog");
            var catalogName = record.GetString("CatalogName");
            var target = record.GetUri("Target");
            var targetType = record.GetStringLookup<IMediaType>("TargetType", x => MediaType.Parse(x));
            var user = record.GetUri("User");
            var userName = record.GetString("UserName");
            var thumbnail = record.GetUri("Thumbnail");

            return new GnosisArtist(name, fromDate, toDate, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, location);
        }

        public void Initialize()
        {
            var builder = new CommandBuilder("create table if not exists Artist (");
            builder.Append("Location text not null primary key, Name text not null, ");
            builder.Append("FromDate text not null, ToDate text not null, Creator text not null, ");
            builder.Append("CreatorName text not null, Catalog text not null, ");
            builder.Append("CatalogName text not null, Target text not null, ");
            builder.Append("TargetType text null, User text not null, UserName text not null, ");
            builder.AppendLine("Thumbnail text not null);");

            builder.AppendLine("create unique index if not exists Artist_Name on Artist (Name asc);");

            ExecuteNonQuery(builder);
        }

        public void Save(IEnumerable<IArtist> artists)
        {
            if (artists == null)
                throw new ArgumentNullException("artists");

            try
            {
                var builders = new List<ICommandBuilder>();

                foreach (var artist in artists)
                {
                    var builder = new CommandBuilder("replace into Artist (Location, Name, FromDate, ToDate, Creator, CreatorName, Catalog, CatalogName, Target, TargetType, User, UserName, Thumbnail) ");
                    builder.AppendLine("values (@Location, @Name, @FromDate, @ToDate, @Creator, @CreatorName, @Catalog, @CatalogName, @Target, @TargetType, @User, @UserName, @Thumbnail);");
                    builder.AddParameter("@Location", artist.Location.ToString());
                    builder.AddParameter("@Name", artist.Name);
                    builder.AddParameter("@FromDate", artist.FromDate.ToString("o"));
                    builder.AddParameter("@ToDate", artist.ToDate.ToString("o"));
                    builder.AddParameter("@Creator", artist.Creator.ToString());
                    builder.AddParameter("@CreatorName", artist.CreatorName);
                    builder.AddParameter("@Catalog", artist.Catalog.ToString());
                    builder.AddParameter("@CatalogName", artist.CatalogName);
                    builder.AddParameter("@Target", artist.Target.ToString());
                    builder.AddParameter("@TargetType", artist.TargetType.ToString());
                    builder.AddParameter("@User", artist.User.ToString());
                    builder.AddParameter("@UserName", artist.UserName);
                    builder.AddParameter("@Thumbnail", artist.Thumbnail.ToString());

                    builders.Add(builder);
                }

                if (builders.Count == 0)
                    return;

                ExecuteTransaction(builders);
            }
            catch (Exception ex)
            {
                logger.Error("  Save", ex);
                throw;
            }
        }

        public void Delete(IEnumerable<Uri> artists)
        {
            if (artists == null)
                throw new ArgumentNullException("artists");

            try
            {
                var builders = new List<ICommandBuilder>();

                foreach (var location in artists)
                {
                    var builder = new CommandBuilder("delete from Artist where Location = @Location;");
                    builder.AddParameter("@Location", location.ToString());

                    builders.Add(builder);
                }

                if (builders.Count == 0)
                    return;

                ExecuteTransaction(builders);
            }
            catch (Exception ex)
            {
                logger.Error("  Delete", ex);
                throw;
            }
        }

        public IArtist GetByLocation(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            try
            {
                var builder = new CommandBuilder("select * from Artist where Location = @Location;");
                builder.AddParameter("@Location", location.ToString());

                return GetArtists(builder).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error("  GetByLocation", ex);
                throw;
            }
        }

        public IEnumerable<IArtist> GetByName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            try
            {
                var builder = new CommandBuilder("select * from Artist where Name like @Name order by Name;");
                builder.AddParameter("@Name", name);

                return GetArtists(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetByName", ex);
                throw;
            }
        }
    }
}
