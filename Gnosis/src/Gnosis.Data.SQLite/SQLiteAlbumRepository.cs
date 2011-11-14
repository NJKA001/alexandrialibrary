using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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
            var id = record.GetGuid("Id");
            var title = record.GetString("Title");
            var released = record.GetDateTime("Released");
            var artist = record.GetGuid("Artist");
            var artistName = record.GetString("ArtistName");
            var thumbnail = record.GetUri("Thumbnail");

            return new Album(title, released, artist, artistName, thumbnail, id);
        }

        public void Initialize()
        {
            var builder = new CommandBuilder("create table if not exists Album (");
            builder.Append("Id text not null primary key, Title text not null, ");
            builder.Append("Released text not null, Artist text not null, ");
            builder.AppendLine("ArtistName text not null, Thumbnail text);");

            builder.AppendLine("create index if not exists Album_Title on Album (Title asc);");
            builder.AppendLine("create index if not exists Album_Artist on Album (Artist asc);");

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
                    var builder = new CommandBuilder("replace into Album (Id, Title, Released, Artist, ArtistName, Thumbnail) ");
                    builder.AppendLine("values (@Id, @Title, @Released, @Artist, @ArtistName, @Thumbnail);");
                    builder.AddParameter("@Id", album.Id.ToString());
                    builder.AddParameter("@Title", album.Title);
                    builder.AddParameter("@Released", album.Released.ToString("s"));
                    builder.AddParameter("@Artist", album.Artist.ToString());
                    builder.AddParameter("@ArtistName", album.ArtistName);

                    var thumbnail = album.Thumbnail != null ? album.Thumbnail.ToString() : (string)null;
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

        public void Delete(IEnumerable<Guid> albums)
        {
            if (albums == null)
                throw new ArgumentNullException("albums");

            try
            {
                var builders = new List<ICommandBuilder>();

                foreach (var id in albums)
                {
                    var builder = new CommandBuilder("delete from Album where Id = @Id;");
                    builder.AddParameter("@Id", id.ToString());

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

        public IAlbum GetById(Guid id)
        {
            try
            {
                var builder = new CommandBuilder("select * from Album where Id = @Id;");
                builder.AddParameter("@Id", id.ToString());

                return GetAlbums(builder).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error("  GetById", ex);
                throw;
            }
        }

        public IEnumerable<IAlbum> GetByArtist(Guid artist)
        {
            try
            {
                var builder = new CommandBuilder("select * from Album where Artist = @Artist order by Released;");
                builder.AddParameter("@Artist", artist.ToString());

                return GetAlbums(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetByArtist", ex);
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
