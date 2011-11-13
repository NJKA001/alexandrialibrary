using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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

        public void Initialize()
        {
            var builder = new CommandBuilder("create table if not exists Track (");
            builder.Append("Id text not null primary key, Title text not null, "); 
            builder.Append("Number integer not null, Duration integer not null, ");
            builder.Append("Artist text not null, ArtistName text not null, ");
            builder.Append("Album text not null, AlbumTitle text not null, ");
            builder.Append("AudioLocation text not null, AudioType text not null, ");
            builder.AppendLine("ThumbnailLocation text, ThumbnailType text);");

            builder.AppendLine("create index if not exists Track_Title on Track (Title asc);");
            builder.AppendLine("create index if not exists Track_Album on Track (Album asc);");

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
                    var builder = new CommandBuilder("replace into Track (Id, Title, Number, ");
                    builder.Append("Duration, Artist, ArtistName, Album, AlbumTitle, AudioLocation, ");
                    builder.Append("AudioType, ThumbnnailLocation, ThumbnailType) ");
                    builder.Append("values (@Id, @Title, @Number, @Duration, @Artist, ");
                    builder.Append("@ArtistName, @Album, @AlbumTitle, @AudioLocation, ");
                    builder.Append("@AudioType, @ThumbnailLocation @ThumbnailType);");
                    builder.AddParameter("@Id", track.Id.ToString());
                    builder.AddParameter("@Title", track.Title);
                    builder.AddParameter("@Number", track.Number);
                    builder.AddParameter("@Duration", track.Duration.TotalMilliseconds);
                    builder.AddParameter("@Artist", track.Artist.ToString());
                    builder.AddParameter("@ArtistName", track.ArtistName);
                    builder.AddParameter("@Album", track.Album.ToString());
                    builder.AddParameter("@AlbumTitle", track.AlbumTitle);
                    builder.AddParameter("@AudioLocation", track.AudioLocation.ToString());
                    builder.AddParameter("@AudioType", track.AudioType.ToString());
                    string thumbnailLocation = null;
                    string thumbnailType = null;
                    if (track.Thumbnail != null)
                    {
                        thumbnailLocation = track.Thumbnail.Location.ToString();
                        thumbnailType = track.Thumbnail.Type.ToString();
                    }
                    builder.AddParameter("@ThumbnailLocation", thumbnailLocation);
                    builder.AddParameter("@ThumbnailType", thumbnailType);
                    
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

        public void Delete(IEnumerable<Guid> tracks)
        {
            if (tracks == null)
                throw new ArgumentNullException("tracks");

            try
            {
                var builders = new List<ICommandBuilder>();

                if (builders.Count == 0)
                    return;

                foreach (var id in tracks)
                {
                    var builder = new CommandBuilder("delete from Track where Id = @Id;");
                    builder.AddParameter("@Id", id.ToString());

                    builders.Add(builder);
                }

                ExecuteTransaction(builders);
            }
            catch (Exception ex)
            {
                logger.Error("  Delete", ex);
                throw;
            }
        }

        public ITrack GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITrack> GetByAlbum(Guid album)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITrack> GetByTitle(string pattern)
        {
            throw new NotImplementedException();
        }
    }
}
