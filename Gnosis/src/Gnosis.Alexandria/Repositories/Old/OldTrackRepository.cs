using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Repositories
{
    public class OldTrackRepository : OldRepositoryBase<ITrack>
    {
        public OldTrackRepository()
            : base("Alexandria.db", "Track", "ArtistHash, ReleaseDate, DiscNumber, AlbumHash, TrackNumber")
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
            sql.AppendLine("ReleaseDate TEXT NOT NULL DEFAULT '2000-01-01T00:00:00',");
            sql.AppendLine("Country TEXT NOT NULL DEFAULT 'unknown',");
            sql.AppendLine("Comment TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("Lyrics TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("Grouping TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("CachePath TEXT NOT NULL DEFAULT ''");
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
            sql.AppendLine("create index if not exists Track_Country on Track (Country ASC);");
            sql.AppendLine("create index if not exists Track_Comment on Track (Comment ASC);");
            sql.AppendLine("create index if not exists Track_Lyrics on Track (Lyrics ASC);");
            sql.AppendLine("create index if not exists Track_Grouping on Track (Grouping ASC);");
            sql.AppendLine("create index if not exists Track_CachePath on Track (CachePath);");
            sql.AppendLine("create index if not exists Track_DefaultSortOrder on Track (Artist ASC, ReleaseDate ASC, DiscNumber ASC, Album ASC, TrackNumber ASC);");
            return sql.ToString();
        }

        protected override ITrack GetRecord(IDataReader reader)
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
            var countryIndex = reader.GetOrdinal("Country");
            var commentIndex = reader.GetOrdinal("Comment");
            var lyricsIndex = reader.GetOrdinal("Lyrics");
            var groupingIndex = reader.GetOrdinal("Grouping");
            var cachePathIndex = reader.GetOrdinal("CachePath");

            var id = new Guid(reader.GetString(idIndex));
            var track = new Track(id)
            {
                Path = reader.GetString(pathIndex),
                ImagePath = reader.GetString(imagePathIndex),
                Title = reader.GetString(titleIndex),
                Artist = reader.GetString(artistIndex),
                Album = reader.GetString(albumIndex),
                TrackNumber = Convert.ToUInt32(reader.GetValue(trackNumberIndex)),
                DiscNumber = Convert.ToUInt32(reader.GetValue(discNumberIndex)),
                Genre = reader.GetString(genreIndex),
                ReleaseDate = DateTime.Parse(reader.GetString(releaseDateIndex)),
                Country = reader.GetString(countryIndex),
                Comment = reader.GetString(commentIndex),
                Lyrics = reader.GetString(lyricsIndex),
                Grouping = reader.GetString(groupingIndex),
                CachePath = reader.GetString(cachePathIndex)
            };

            return track;
        }

        protected override IDbCommand GetSaveCommand(IDbConnection connection, ITrack record)
        {
            var command = connection.CreateCommand();

            var sql = new StringBuilder();
            sql.AppendLine("insert or replace into Track (Id, Path, ImagePath, Title, TitleHash, TitleMetaphone, Artist, ArtistHash, ArtistMetaphone, Album, AlbumHash, AlbumMetaphone, TrackNumber, DiscNumber, Genre, ReleaseDate, Country, Comment, Lyrics, Grouping, CachePath)");
            sql.AppendLine(" values (@Id, @Path, @ImagePath, @Title, @TitleHash, @TitleMetaphone, @Artist, @ArtistHash, @ArtistMetaphone, @Album, @AlbumHash, @AlbumMetaphone, @TrackNumber, @DiscNumber, @Genre, @ReleaseDate, @Country, @Comment, @Lyrics, @Grouping, @CachePath);");
            command.CommandText = sql.ToString();

            AddParameter(command, "@Id", record.Id.ToString());
            AddParameter(command, "@Path", record.Path);
            AddParameter(command, "@ImagePath", record.ImagePath);
            AddParameter(command, "@Title", record.Title);
            AddParameter(command, "@TitleHash", record.TitleHash);
            AddParameter(command, "@TitleMetaphone", record.TitleMetaphone);
            AddParameter(command, "@Artist", record.Artist);
            AddParameter(command, "@ArtistHash", record.ArtistHash);
            AddParameter(command, "@ArtistMetaphone", record.ArtistMetaphone);
            AddParameter(command, "@Album", record.Album);
            AddParameter(command, "@AlbumHash", record.AlbumHash);
            AddParameter(command, "@AlbumMetaphone", record.AlbumMetaphone);
            AddParameter(command, "@TrackNumber", record.TrackNumber);
            AddParameter(command, "@DiscNumber", record.DiscNumber);
            AddParameter(command, "@Genre", record.Genre);
            AddParameter(command, "@ReleaseDate", record.ReleaseDate.ToString("s"));
            AddParameter(command, "@Country", record.Country);
            AddParameter(command, "@Comment", record.Comment);
            AddParameter(command, "@Lyrics", record.Lyrics);
            AddParameter(command, "@Grouping", record.Grouping);
            AddParameter(command, "@CachePath", record.CachePath);

            return command;
        }
    }
}
