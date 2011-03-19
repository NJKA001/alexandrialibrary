using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Archon.Models;

namespace Gnosis.Archon.Repositories
{
    public class TrackRepository : RepositoryBase<ITrack>
    {
        public TrackRepository()
            : base("Alexandria.db", "Track")
        {
        }

        protected override string GetInitializeText()
        {
            var sql = new StringBuilder();

            sql.AppendLine("create table if not exists Track (");
            sql.AppendLine("Id TEXT PRIMARY KEY NOT NULL,");
            sql.AppendLine("Path TEXT UNIQUE NOT NULL,");
            sql.AppendLine("ImagePath TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("Title TEXT NOT NULL DEFAULT 'Untitled',");
            sql.AppendLine("TitleHash TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("TitleMetaphone TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("Artist TEXT NOT NULL DEFAULT 'Unknown Artist',");
            sql.AppendLine("ArtistHash TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("ArtistMetaphone TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("Album TEXT NOT NULL DEFAULT 'Unknown Album',");
            sql.AppendLine("AlbumHash TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("AlbumMetaphone TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("TrackNumber INTEGER NOT NULL DEFAULT 0,");
            sql.AppendLine("DiscNumber INTEGER NOT NULL DEFAULT 1,");
            sql.AppendLine("Genre TEXT NOT NULL DEFAULT 'Unknown Genre',");
            sql.AppendLine("ReleaseDate TEXT NOT NULL DEFAULT '2000-01-01T00:00:00'");
            sql.AppendLine(");");
            sql.AppendLine("create index if not exists Track_Title on Track (Title ASC);");
            sql.AppendLine("create index if not exists Track_TitleHash on Track (TitleHash ASC);");
            sql.AppendLine("create index if not exists Track_TitleMetaphone on Track (TitleMetaphone ASC);");
            sql.AppendLine("create index if not exists Track_Artist on Track (Artist ASC);");
            sql.AppendLine("create index if not exists Track_ArtistHash on Track (ArtistHash ASC);");
            sql.AppendLine("create index if not exists Track_ArtistMetaphone on Track (ArtistMetaphone ASC);");
            sql.AppendLine("create index if not exists Track_Album on Track (Album ASC);");
            sql.AppendLine("create index if not exists Track_AlbumHash on Track (AlbumHash ASC);");
            sql.AppendLine("create index if not exists Track_AlbumMetaphone on Track (AlbumMetaphone ASC);");
            sql.AppendLine("create index if not exists Track_ReleaseDate on Track (ReleaseDate ASC);");
            sql.AppendLine("create index if not exists Track_DefaultSortOrder on Track (Artist ASC, ReleaseDate ASC, DiscNumber ASC, Album ASC, TrackNumber ASC);");
            return sql.ToString();
        }

        protected override ITrack Get(IDataReader reader)
        {
            var idIndex = reader.GetOrdinal("Id");
            var pathIndex = reader.GetOrdinal("Path");
            var imagePathIndex = reader.GetOrdinal("ImagePath");
            var titleIndex = reader.GetOrdinal("Title");
            var titleHashIndex = reader.GetOrdinal("TitleHash");
            var titleMetaphoneIndex = reader.GetOrdinal("TitleMetaphone");
            var artistIndex = reader.GetOrdinal("Artist");
            var artistHashIndex = reader.GetOrdinal("ArtistHash");
            var artistMetaphoneIndex = reader.GetOrdinal("ArtistMetaphone");
            var albumIndex = reader.GetOrdinal("Album");
            var albumHashIndex = reader.GetOrdinal("AlbumHash");
            var albumMetaphoneIndex = reader.GetOrdinal("AlbumMetaphone");
            var trackNumberIndex = reader.GetOrdinal("TrackNumber");
            var discNumberIndex = reader.GetOrdinal("DiscNumber");
            var genreIndex = reader.GetOrdinal("Genre");
            var releaseDateIndex = reader.GetOrdinal("ReleaseDate");

            var id = new Guid(reader.GetString(idIndex));
            var track = new Track(id);
            //{
                track.Path = reader.GetString(pathIndex);
                track.ImagePath = reader.GetString(imagePathIndex);
                track.Title = reader.GetString(titleIndex);
                track.Artist = reader.GetString(artistIndex);
                track.Album = reader.GetString(albumIndex);
                track.TrackNumber = Convert.ToUInt32(reader.GetValue(trackNumberIndex));
                track.DiscNumber = Convert.ToUInt32(reader.GetValue(discNumberIndex));
                track.Genre = reader.GetString(genreIndex);
                track.ReleaseDate = DateTime.Parse(reader.GetString(releaseDateIndex));
            //};

            return track;
        }

        protected override IDbCommand GetSaveCommand(IDbConnection connection, ITrack track)
        {
            var command = connection.CreateCommand();

            var sql = new StringBuilder();
            sql.AppendLine("insert or replace into Track (Id, Path, ImagePath, Title, TitleHash, TitleMetaphone, Artist, ArtistHash, ArtistMetaphone, Album, AlbumHash, AlbumMetaphone, TrackNumber, DiscNumber, Genre, ReleaseDate)");
            sql.AppendLine(" values (@Id, @Path, @ImagePath, @Title, @TitleHash, @TitleMetaphone, @Artist, @ArtistHash, @ArtistMetaphone, @Album, @AlbumHash, @AlbumMetaphone, @TrackNumber, @DiscNumber, @Genre, @ReleaseDate);");
            command.CommandText = sql.ToString();

            AddParameter(command, "@Id", track.Id.ToString());
            AddParameter(command, "@Path", track.Path);
            AddParameter(command, "@ImagePath", track.ImagePath);
            AddParameter(command, "@Title", track.Title);
            AddParameter(command, "@TitleHash", track.TitleHash);
            AddParameter(command, "@TitleMetaphone", track.TitleMetaphone);
            AddParameter(command, "@Artist", track.Artist);
            AddParameter(command, "@ArtistHash", track.ArtistHash);
            AddParameter(command, "@ArtistMetaphone", track.ArtistMetaphone);
            AddParameter(command, "@Album", track.Album);
            AddParameter(command, "@AlbumHash", track.AlbumHash);
            AddParameter(command, "@AlbumMetaphone", track.AlbumMetaphone);
            AddParameter(command, "@TrackNumber", track.TrackNumber);
            AddParameter(command, "@DiscNumber", track.DiscNumber);
            AddParameter(command, "@Genre", track.Genre);
            AddParameter(command, "@ReleaseDate", track.ReleaseDate.ToString("s"));

            return command;
        }

        protected override IDbCommand GetDeleteCommand(IDbConnection connection, Guid id)
        {
            var command = connection.CreateCommand();
            command.CommandText = "delete from Track where Id = @Id;";
            AddParameter(command, "@Id", id.ToString());

            return command;
        }

        protected override IDbCommand GetSelectCommand(IDbConnection connection, IEnumerable<KeyValuePair<string, object>> criteria)
        {
            var command = connection.CreateCommand();

            var sql = new StringBuilder("select * from Track\n");

            if (criteria != null && criteria.Count() > 0)
            {
                sql.AppendLine("where");
                var prefix = string.Empty;
                foreach (var criterium in criteria)
                {
                    var parameterName = string.Format("@{0}", Guid.NewGuid().ToString().Replace("-", string.Empty));
                    AddParameter(command, parameterName, criterium.Value);

                    if (criterium.Value != null && criterium.Value.ToString().Contains('%'))
                        sql.AppendFormat("\n  {0}{1} like {2}", prefix, criterium.Key, parameterName);
                    else
                        sql.AppendFormat("\n  {0}{1} = {2}", prefix, criterium.Key, parameterName);
                    prefix = "or ";
                }
            }

            sql.AppendLine("\norder by ArtistHash, ReleaseDate, DiscNumber, AlbumHash, TrackNumber;");

            command.CommandText = sql.ToString();
            return command;
        }
    }
}
