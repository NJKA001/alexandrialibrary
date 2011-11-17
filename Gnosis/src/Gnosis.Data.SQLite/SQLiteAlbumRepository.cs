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
            var title = record.GetString("Title");
            var created = record.GetDateTime("Created");
            var creator = record.GetUri("Creator");
            var creatorName = record.GetString("CreatorName");
            var thumbnail = record.GetUri("Thumbnail");

            return new GnosisAlbum(title, created, creator, creatorName, thumbnail, location);
        }

        public void Initialize()
        {
            var builder = new CommandBuilder("create table if not exists Album (");
            builder.Append("Location text not null primary key, Title text not null, ");
            builder.Append("Created text not null, Creator text not null, ");
            builder.AppendLine("CreatorName text not null, Thumbnail text not null);");

            builder.AppendLine("create index if not exists Album_Title on Album (Title asc);");
            builder.AppendLine("create index if not exists Album_Creator on Album (Creator asc);");
            builder.AppendLine("create unique index if not exists Album_Creator_Title on Album (Creator asc, Title asc);");

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
                    var builder = new CommandBuilder("replace into Album (Location, Title, Created, Creator, CreatorName, Thumbnail) ");
                    builder.AppendLine("values (@Location, @Title, @Created, @Creator, @CreatorName, @Thumbnail);");
                    builder.AddParameter("@Location", album.Location.ToString());
                    builder.AddParameter("@Title", album.Title);
                    builder.AddParameter("@Created", album.Created.ToString("o"));
                    builder.AddParameter("@Creator", album.Creator.ToString());
                    builder.AddParameter("@CreatorName", album.CreatorName);

                    var thumbnail = album.Thumbnail != null ? album.Thumbnail.ToString() : string.Empty;
                    builder.AddParameter("@Thumbnail", thumbnail);

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

        public IAlbum GetByCreatorTitle(Uri creator, string title)
        {
            if (creator == null)
                throw new ArgumentNullException("creator");
            if (title == null)
                throw new ArgumentNullException("title");

            try
            {
                var builder = new CommandBuilder("select * from Album where Creator = @Creator and Title = @Title;");
                builder.AddParameter("@Creator", creator.ToString());
                builder.AddParameter("@Title", title);

                return GetAlbums(builder).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error("  GetByCreatorTitle", ex);
                throw;
            }
        }

        public IEnumerable<IAlbum> GetByCreator(Uri creator)
        {
            if (creator == null)
                throw new ArgumentNullException("creator");

            try
            {
                var builder = new CommandBuilder("select * from Album where Creator = @Creator order by Created;");
                builder.AddParameter("@Creator", creator.ToString());

                return GetAlbums(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetByCreator", ex);
                throw;
            }
        }

        public IEnumerable<IAlbum> GetByTitle(string title)
        {
            if (title == null)
                throw new ArgumentNullException("title");

            try
            {
                var builder = new CommandBuilder("select * from Album where Title like @Title order by Title;");
                builder.AddParameter("@Title", title);

                return GetAlbums(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetByTitle", ex);
                throw;
            }
        }
    }
}
