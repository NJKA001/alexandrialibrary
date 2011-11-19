using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

namespace Gnosis.Data.SQLite
{
    public class SQLiteTrackRepository
        : SQLiteRepositoryBase, ITrackRepository
    {
        public SQLiteTrackRepository(ILogger logger)
            : base(logger)
        {
        }

        public SQLiteTrackRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, defaultConnection)
        {
        }

        private IEnumerable<ITrack> GetTracks(ICommandBuilder builder)
        {
            IDbConnection connection = null;
            var tracks = new List<ITrack>();

            try
            {
                connection = GetConnection();

                var command = builder.ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var track = ReadTrack(reader);
                        tracks.Add(track);
                    }
                }

                return tracks;
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        private ITrack ReadTrack(IDataRecord record)
        {
            var location = record.GetUri("Location");
            var title = record.GetString("Title");
            var number = record.GetUInt32("Number");
            var duration = TimeSpan.FromMilliseconds((double)record.GetInt32("Duration"));
            var creator = record.GetUri("Creator");
            var creatorName = record.GetString("CreatorName");
            var album = record.GetUri("Album");
            var albumTitle = record.GetString("AlbumTitle");
            var target = record.GetUri("Target");
            var targetType = record.GetStringLookup<IMediaType>("TargetType", typeName => MediaType.Parse(typeName));
            var thumbnail = record.GetUri("Thumbnail");

            return new GnosisTrack(title, number, duration, creator, creatorName, album, albumTitle, target, targetType, thumbnail, location);
        }

        public void Initialize()
        {
            var builder = new CommandBuilder("create table if not exists Track (");
            builder.Append("Location text not null primary key, Title text not null, "); 
            builder.Append("Number integer not null, Duration integer not null, ");
            builder.Append("Creator text not null, CreatorName text not null, ");
            builder.Append("Album text not null, AlbumTitle text not null, ");
            builder.Append("Target text not null, TargetType text not null, ");
            builder.AppendLine("Thumbnail text not null);");

            builder.AppendLine("create index if not exists Track_Title on Track (Title asc);");
            builder.AppendLine("create index if not exists Track_Creator on Track (Creator asc);");
            builder.AppendLine("create index if not exists Track_Album on Track (Album asc);");
            builder.AppendLine("create index if not exists Track_Target on Track (Target asc);");

            ExecuteNonQuery(builder);
        }

        public void Save(IEnumerable<ITrack> tracks)
        {
            if (tracks == null)
                throw new ArgumentNullException("tracks");

            try
            {

                var builders = new List<ICommandBuilder>();

                foreach (var track in tracks)
                {
                    var builder = new CommandBuilder("replace into Track (Location, Title, Number, ");
                    builder.Append("Duration, Creator, CreatorName, Album, AlbumTitle, Target, ");
                    builder.Append("TargetType, Thumbnail) values ");
                    builder.Append("(@Location, @Title, @Number, @Duration, @Creator, ");
                    builder.Append("@CreatorName, @Album, @AlbumTitle, @Target, ");
                    builder.AppendLine("@TargetType, @Thumbnail);");
                    builder.AddParameter("@Location", track.Location.ToString());
                    builder.AddParameter("@Title", track.Title);
                    builder.AddParameter("@Number", track.Number);
                    builder.AddParameter("@Duration", track.Duration.TotalMilliseconds);
                    builder.AddParameter("@Creator", track.Creator.ToString());
                    builder.AddParameter("@CreatorName", track.CreatorName);
                    builder.AddParameter("@Album", track.Album.ToString());
                    builder.AddParameter("@AlbumTitle", track.AlbumTitle);
                    builder.AddParameter("@Target", track.Target.ToString());
                    builder.AddParameter("@TargetType", track.TargetType.ToString());

                    var thumbnail = track.Thumbnail != null ? track.Thumbnail.ToString() : string.Empty;
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

        public void Delete(IEnumerable<Uri> tracks)
        {
            if (tracks == null)
                throw new ArgumentNullException("tracks");

            try
            {
                var builders = new List<ICommandBuilder>();

                foreach (var location in tracks)
                {
                    var builder = new CommandBuilder("delete from Track where Location = @Location;");
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

        public ITrack GetByLocation(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            try
            {
                var builder = new CommandBuilder("select * from Track where Location = @Location;");
                builder.AddParameter("@Location", location.ToString());

                return GetTracks(builder).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error("  GetByLocation", ex);
                throw;
            }
        }

        public ITrack GetByTarget(Uri target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            try
            {
                var builder = new CommandBuilder("select * from Track where Target = @Target;");
                builder.AddParameter("@Target", target.ToString());

                return GetTracks(builder).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error("  GetByTarget", ex);
                throw;
            }
        }

        public IEnumerable<ITrack> GetByAlbum(Uri album)
        {
            if (album == null)
                throw new ArgumentNullException("album");

            try
            {
                var builder = new CommandBuilder("select * from Track where Album = @Album order by Number;");
                builder.AddParameter("@Album", album.ToString());

                return GetTracks(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetByAlbum", ex);
                throw;
            }
        }

        public IEnumerable<ITrack> GetByTitle(string title)
        {
            if (title == null)
                throw new ArgumentNullException("title");

            try
            {
                var builder = new CommandBuilder("select * from Track where Title like @Title order by Title;");
                builder.AddParameter("@Title", title);

                return GetTracks(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  GetByTitle", ex);
                throw;
            }
        }
    }
}
