using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using System.Data.SQLite;

namespace Gnosis.Archon
{
    public class TrackRepository : ITrackRepository
    {
        public TrackRepository()
        {
            Initialize();
        }

        private static IDbConnection GetConnecion()
        {
            return new SQLiteConnection("Data Source=Alexandria.db;Version=3;");
        }

        private void Initialize()
        {
            using (var connection = GetConnecion())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = GetInitializeText();
                    command.ExecuteNonQuery();
                }
            }
        }

        private static string GetInitializeText()
        {
            var sql = new StringBuilder();

            sql.AppendLine("create table if not exists Track (");
            sql.AppendLine("Id TEXT PRIMARY KEY NOT NULL,");
            sql.AppendLine("Path TEXT UNIQUE NOT NULL,");
            sql.AppendLine("ImagePath TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("Title TEXT NOT NULL DEFAULT 'Untitled',");
            sql.AppendLine("Artist TEXT NOT NULL DEFAULT 'Unknown Artist',");
            sql.AppendLine("Album TEXT NOT NULL DEFAULT 'Unknown Album',");
            sql.AppendLine("TrackNumber INTEGER NOT NULL DEFAULT 0,");
            sql.AppendLine("DiscNumber INTEGER NOT NULL DEFAULT 1,");
            sql.AppendLine("Genre TEXT NOT NULL DEFAULT 'Unknown Genre',");
            sql.AppendLine("ReleaseDate TEXT NOT NULL DEFAULT '2000-01-01T00:00:00'");
            sql.AppendLine(");");
            sql.AppendLine("create index if not exists Track_Title on Track (Title ASC);");
            sql.AppendLine("create index if not exists Track_Artist on Track (Artist ASC);");
            sql.AppendLine("create index if not exists Track_Album on Track (Album ASC);");
            sql.AppendLine("create index if not exists Track_ReleaseDate on Track (ReleaseDate ASC);");
            sql.AppendLine("create index if not exists Track_DefaultSortOrder on Track (Artist ASC, ReleaseDate ASC, DiscNumber ASC, Album ASC, TrackNumber ASC);");
            return sql.ToString();
        }

        private static ITrack GetTrack(IDataReader reader)
        {
            var idIndex = reader.GetOrdinal("Id");
            var pathIndex = reader.GetOrdinal("Path");
            var imagePathIndex = reader.GetOrdinal("ImagePath");
            var titleIndex = reader.GetOrdinal("Title");
            var artistIndex = reader.GetOrdinal("Artist");
            var albumIndex = reader.GetOrdinal("Album");
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

        private static void AddParameter(IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        private static IDbCommand GetSaveCommand(IDbConnection connection, ITrack track)
        {
            var command = connection.CreateCommand();

            var sql = new StringBuilder();
            sql.AppendLine("insert or replace into Track (Id, Path, ImagePath, Title, Artist, Album, TrackNumber, DiscNumber, Genre, ReleaseDate)");
            sql.AppendLine(" values (@Id, @Path, @ImagePath, @Title, @Artist, @Album, @TrackNumber, @DiscNumber, @Genre, @ReleaseDate);");
            command.CommandText = sql.ToString();

            AddParameter(command, "@Id", track.Id.ToString());
            AddParameter(command, "@Path", track.Path);
            AddParameter(command, "@ImagePath", track.ImagePath);
            AddParameter(command, "@Title", track.Title);
            AddParameter(command, "@Artist", track.Artist);
            AddParameter(command, "@Album", track.Album);
            AddParameter(command, "@TrackNumber", track.TrackNumber);
            AddParameter(command, "@DiscNumber", track.DiscNumber);
            AddParameter(command, "@Genre", track.Genre);
            AddParameter(command, "@ReleaseDate", track.ReleaseDate.ToString("s"));

            return command;
        }

        public ITrack Get(Guid id)
        {
            using (var connection = GetConnecion())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select * from Track where Id = @Id;";
                    AddParameter(command, "@Id", id.ToString());
                    
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        return GetTrack(reader);
                    }
                }
            }
        }

        public void Save(ITrack track)
        {
            using (var connection = GetConnecion())
            {
                connection.Open();
                using (var command = GetSaveCommand(connection, track))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(Guid id)
        {
            using (var connection = GetConnecion())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "delete from Track where Id = @Id;";
                    AddParameter(command, "@Id", id.ToString());
                    
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<ITrack> Tracks()
        {
            var tracks = new List<ITrack>();

            using (var connection = GetConnecion())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select * from Track order by Artist, ReleaseDate, DiscNumber, Album, TrackNumber;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var track = GetTrack(reader);
                            tracks.Add(track);
                        }
                    }
                }
            }

            return tracks;
        }
    }
}
