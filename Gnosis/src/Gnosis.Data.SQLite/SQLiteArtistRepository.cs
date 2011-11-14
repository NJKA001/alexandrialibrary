using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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
            var id = record.GetGuid("Id");
            var name = record.GetString("Name");
            var activeFrom = record.GetDateTime("ActiveFrom");
            var activeTo = record.GetDateTime("ActiveTo");
            var thumbnail = record.GetUri("Thumbnail");

            return new Artist(name, activeFrom, activeTo, thumbnail, id);
        }

        public void Initialize()
        {
            var builder = new CommandBuilder("create table if not exists Artist (");
            builder.Append("Id text not null primary key, Name text not null, ");
            builder.Append("ActiveFrom text not null, ActiveTo text not null, ");
            builder.AppendLine("Thumbnail text);");

            builder.AppendLine("create index if not exists Artist_Name on Artist (Name asc);");
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
                    var builder = new CommandBuilder("replace into Artist (Id, Name, ActiveFrom, ActiveTo, Thumbnail) ");
                    builder.AppendLine("values (@Id, @Name, @ActiveFrom, @ActiveTo, @Thumbnail);");
                    builder.AddParameter("@Id", artist.Id.ToString());
                    builder.AddParameter("@Name", artist.Name);
                    builder.AddParameter("@ActiveFrom", artist.ActiveFrom.ToString("s"));
                    builder.AddParameter("@ActiveTo", artist.ActiveTo.ToString("s"));
                    
                    var thumbnail = artist.Thumbnail != null ?
                        artist.Thumbnail.ToString()
                        : null;
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

        public void Delete(IEnumerable<Guid> artists)
        {
            if (artists == null)
                throw new ArgumentNullException("artists");

            try
            {
                var builders = new List<ICommandBuilder>();

                foreach (var id in artists)
                {
                    var builder = new CommandBuilder("delete from Artist where Id = @Id;");
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

        public IArtist GetById(Guid id)
        {
            try
            {
                var builder = new CommandBuilder("select * from Artist where Id = @Id;");
                builder.AddParameter("@Id", id.ToString());

                return GetArtists(builder).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error("  GetById", ex);
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
