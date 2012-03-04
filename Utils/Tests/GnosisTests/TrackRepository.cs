using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

using GnosisTests.Entities;

namespace GnosisTests
{
    class TrackRepository
    {
        private IDbConnection GetConnection()
        {
            return new SQLiteConnection("Data Source=Track.db");
        }

        public void Initialize()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "create table if not exists Track (Id integer primary key, Name text not null, Album integer not null, Artist integer not null, Disc integer not null, Number integer not null, Duration integer not null);";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Create(IEnumerable<Track> tracks)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                foreach (var track in tracks)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "insert into Track (Name, Album, Artist, Disc, Number, Duration) values (@Name, @Album, @Artist, @Disc, @Number, @Duration);";
                        command.Parameters.Add(new SQLiteParameter("@Name", track.Name));
                        command.Parameters.Add(new SQLiteParameter("@Album", track.Album));
                        command.Parameters.Add(new SQLiteParameter("@Artist", track.Artist));
                        command.Parameters.Add(new SQLiteParameter("@Disc", track.Disc));
                        command.Parameters.Add(new SQLiteParameter("@Number", track.Number));
                        command.Parameters.Add(new SQLiteParameter("@Duration", track.Duration));
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public IEnumerable<Track> GetAll()
        {
            var tracks = new List<Track>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select Id, Name, Album, Artist, Disc, Number, Duration from Track;";
                    command.CommandType = CommandType.Text;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var track = new Track();
                            track.Id = uint.Parse(reader.GetValue(0).ToString());
                            track.Name = reader.GetString(1);
                            track.Album = uint.Parse(reader.GetValue(2).ToString());
                            track.Artist = uint.Parse(reader.GetValue(3).ToString());
                            track.Disc = byte.Parse(reader.GetValue(4).ToString());
                            track.Number = byte.Parse(reader.GetValue(5).ToString());
                            track.Duration = ushort.Parse(reader.GetValue(6).ToString());

                            tracks.Add(track);
                        }
                    }
                }
            }

            return tracks;
        }
    }
}
