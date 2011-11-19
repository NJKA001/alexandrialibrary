using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

namespace Gnosis.Data.SQLite
{
    public class SQLiteAlbumRepository
        : SQLiteRepositoryBase, IAlbumRepository
    {
        public SQLiteAlbumRepository(ILogger logger)
            : base(logger)
        {
        }

        public SQLiteAlbumRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, defaultConnection)
        {
        }

        private IEnumerable<IAlbum> GetAlbums(ICommandBuilder builder)
        {
            IDbConnection connection = null;
            var albums = new List<IAlbum>();

            try
            {
                connection = GetConnection();

                var command = builder.ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var album = ReadAlbum(reader);
                        albums.Add(album);
                    }
                }

                return albums;
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        private IAlbum ReadAlbum(IDataRecord record)
        {
            var location = record.GetUri("Location");
            var name = record.GetString("Name");
            var date = record.GetDateTime("Date");
            var creator = record.GetUri("Creator");
            var creatorName = record.GetString("CreatorName");
            var catalog = record.GetUri("Catalog");
            var catalogName = record.GetString("CatalogName");
            var target = record.GetUri("Target");
            var targetType = record.GetStringLookup<IMediaType>("TargetType", x => MediaType.Parse(x));
            var user = record.GetUri("User");
            var userName = record.GetString("UserName");
            var thumbnail = record.GetUri("Thumbnail");

            return new GnosisAlbum(name, date, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, location);
        }

        public void Initialize()
        {
            var builder = new CommandBuilder("create table if not exists Album (");
            builder.Append("Location text not null primary key, Name text not null, ");
            builder.Append("Date text not null, Creator text not null, ");
            builder.Append("CreatorName text not null, Catalog text not null, ");
            builder.Append("CatalogName text not null, Target text not null, ");
            builder.Append("TargetType text not null, User text not null, ");
            builder.AppendLine("UserName text not null, Thumbnail text not null);");

            builder.AppendLine("create index if not exists Album_Name on Album (Name asc);");
            builder.AppendLine("create index if not exists Album_Creator on Album (Creator asc);");
            builder.AppendLine("create unique index if not exists Album_Creator_Name on Album (Creator asc, Name asc);");

            ExecuteNonQuery(builder);
        }

        public void Save(IEnumerable<IAlbum> albums)
        {
            if (albums == null)
                throw new ArgumentNullException("albums");

            try
            {
                var builders = new List<ICommandBuilder>();

                foreach (var album in albums)
                {
                    var builder = new CommandBuilder("replace into Album (Location, Name, Date, Creator, CreatorName, Catalog, CatalogName, Target, TargetType, User, UserName, Thumbnail) ");
                    builder.AppendLine("values (@Location, @Name, @Date, @Creator, @CreatorName, @Catalog, @CatalogName, @Target, @TargetType, @User, @UserName, @Thumbnail);");
                    builder.AddParameter("@Location", album.Location.ToString());
                    builder.AddParameter("@Name", album.Name);
                    builder.AddParameter("@Date", album.Date.ToString("o"));
                    builder.AddParameter("@Creator", album.Creator.ToString());
                    builder.AddParameter("@CreatorName", album.CreatorName);
                    builder.AddParameter("@Catalog", album.Catalog.ToString());
                    builder.AddParameter("@CatalogName", album.CatalogName);
                    builder.AddParameter("@Target", album.Target.ToString());
                    builder.AddParameter("@TargetType", album.TargetType.ToString());
                    builder.AddParameter("@User", album.User.ToString());
                    builder.AddParameter("@UserName", album.UserName);
                    builder.AddParameter("@Thumbnail", album.Thumbnail.ToString());

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

        public void Delete(IEnumerable<Uri> albums)
        {
            if (albums == null)
                throw new ArgumentNullException("albums");

            try
            {
                var builders = new List<ICommandBuilder>();

                foreach (var location in albums)
                {
                    var builder = new CommandBuilder("delete from Album where Location = @Location;");
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

        public IAlbum GetByLocation(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            try
            {
                var builder = new CommandBuilder("select * from Album where Location = @Location;");
                builder.AddParameter("@Location", location.ToString());

                return GetAlbums(builder).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error("  GetByLocation", ex);
                throw;
            }
        }

        public IAlbum GetByCreatorAndName(Uri creator, string name)
        {
            if (creator == null)
                throw new ArgumentNullException("creator");
            if (name == null)
                throw new ArgumentNullException("name");

            try
            {
                var builder = new CommandBuilder("select * from Album where Creator = @Creator and Name = @Name;");
                builder.AddParameter("@Creator", creator.ToString());
                builder.AddParameter("@Name", name);

                return GetAlbums(builder).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error("  GetByCreatorAndName", ex);
                throw;
            }
        }

        public IEnumerable<IAlbum> GetByCreator(Uri creator)
        {
            if (creator == null)
                throw new ArgumentNullException("creator");

            try
            {
                var builder = new CommandBuilder("select * from Album where Creator = @Creator order by Date;");
                builder.AddParameter("@Creator", creator.ToString());

                return GetAlbums(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetByCreator", ex);
                throw;
            }
        }

        public IEnumerable<IAlbum> GetByName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            try
            {
                var builder = new CommandBuilder("select * from Album where Name like @Name order by Name;");
                builder.AddParameter("@Name", name);

                return GetAlbums(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetByName", ex);
                throw;
            }
        }
    }
}
