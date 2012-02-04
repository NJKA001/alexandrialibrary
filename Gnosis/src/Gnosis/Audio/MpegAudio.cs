using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Algorithms;
using Gnosis.Application.Vendor;
using Gnosis.Metadata;
using Gnosis.Tags;
using Gnosis.Tags.TagLib;

namespace Gnosis.Audio
{
    public class MpegAudio
        : AudioBase
    {
        public MpegAudio(Uri location, IMediaType mediaType)
            : base(location, mediaType)
        {
        }

        private Gnosis.Tags.TagLib.File file;
        private Gnosis.Tags.TagLib.Id3v1.Tag id3v1Tag;
        private Gnosis.Tags.TagLib.Id3v2.Tag id3v2Tag;

        //private Uri GetJpgToUrl(string name)
        //{
        //    if (name == null)
        //        throw new ArgumentNullException("name");

        //    var escaped = name.RemoveNonAlphaNumerics().Replace(" ", "_");

        //    try
        //    {
        //        return new Uri(string.Format("http://{0}.jpg.to", escaped), UriKind.Absolute);
        //    }
        //    catch (Exception ex)
        //    {
        //        var m = ex.Message;
        //        return Guid.Empty.ToUrn();
        //    }
        //}

        public override void Load()
        {
            if (Location.IsFile)
            {
                file = Gnosis.Tags.TagLib.File.Create(Location.LocalPath);
                id3v1Tag = file.GetTag(TagTypes.Id3v1) as Gnosis.Tags.TagLib.Id3v1.Tag;
                id3v2Tag = file.GetTag(TagTypes.Id3v2) as Gnosis.Tags.TagLib.Id3v2.Tag;

                //if (id3v1Tag == null || id3v1Tag.IsEmpty)
                //    System.Diagnostics.Debug.WriteLine("ID3v1 tag is null or empty");
                //else
                //{
                //    if (id3v1Tag.Album != null || id3v1Tag.AlbumArtists != null
                //        || id3v1Tag.Comment != null || id3v1Tag.Composers != null || id3v1Tag.Conductor != null
                //    || id3v1Tag.Copyright != null || id3v1Tag.Genres != null || id3v1Tag.Grouping != null)
                //        System.Diagnostics.Debug.WriteLine("ID3v1 has non null tags");
                //}

                //if (id3v2Tag == null || id3v1Tag.IsEmpty)
                //    System.Diagnostics.Debug.WriteLine("ID3v2 tag is null or empty");
                //else
                //{
                //    var x = id3v2Tag.Duration;
                //}
            }
        }

        public override IEnumerable<ITag> GetTags()
        {
            var tags = new List<ITag>();

            if (id3v2Tag != null)
            {
                if (id3v2Tag.Performers != null && id3v2Tag.Performers.Length > 0)
                {
                    tags.Add(new Gnosis.Tags.Tag(Location, TagType.Artist, id3v2Tag.JoinedPerformers));

                    if (id3v1Tag.Performers.Length > 1)
                    {
                        for (var i = 0; i < id3v1Tag.Performers.Length; i++)
                        {
                            tags.Add(new Gnosis.Tags.Tag(Location, TagType.Artist, id3v2Tag.Performers[i]));
                        }
                    }
                }
                if (id3v2Tag.Album != null)
                    tags.Add(new Gnosis.Tags.Tag(Location, TagType.Album, id3v2Tag.Album));
                if (id3v2Tag.Year > 0)
                    tags.Add(new Gnosis.Tags.Tag(Location, TagType.ReleaseTime, id3v2Tag.ReleaseDate.ToString("o")));
                if (id3v2Tag.Track > 0)
                    tags.Add(new Gnosis.Tags.Tag(Location, TagType.TrackNumber, id3v2Tag.Track.ToString()));
                if (id3v2Tag.TrackCount > 0)
                    tags.Add(new Gnosis.Tags.Tag(Location, TagType.TrackCount, id3v2Tag.TrackCount.ToString()));
            }
            else if (id3v1Tag != null)
            {
            }

            return tags;
        }

        public override IArtist GetArtist(ISecurityContext securityContext, IMediaFactory mediaFactory, IMediaItemRepository mediaItemRepository)
        {
            IArtist artist = null;
            var track = mediaItemRepository.GetByTarget<ITrack>(Location).FirstOrDefault();
            if (track != null)
            {
                artist = mediaItemRepository.GetByLocation<IArtist>(track.Creator);
                if (artist != null)
                    return artist;
            }

            if (id3v2Tag == null || id3v2Tag.JoinedPerformers == null)
                return new MediaItemBuilder<IArtist>(securityContext, mediaFactory).GetDefault();

            var artistName = id3v2Tag.JoinedPerformers;
            var summary = string.Empty;
            artist = mediaItemRepository.GetByName<IArtist>(artistName).FirstOrDefault();
            if (artist != null)
                return artist;

            var builder = new MediaItemBuilder<IArtist>(securityContext, mediaFactory)
                .Identity(artistName, summary);

            return builder.ToMediaItem();
        }

        public override IAlbum GetAlbum(ISecurityContext securityContext, IMediaFactory mediaFactory, IMediaItemRepository mediaItemRepository, IArtist artist)
        {
            IAlbum album = null;
            var track = mediaItemRepository.GetByTarget<ITrack>(Location).FirstOrDefault();
            if (track != null)
            {
                album = mediaItemRepository.GetByLocation<IAlbum>(track.Catalog);
                if (album != null)
                    return album;
            }

            var albumTitle = "Unknown Album";
            var summary = string.Empty;
            //var albumTag = GetTags().Where(x => x.Type == Id3v2TagType.Album).FirstOrDefault();
            if (id3v2Tag != null && id3v2Tag.Album != null)
            {
                albumTitle = id3v2Tag.Album; //albumTag.Tuple.ToString();
                album = mediaItemRepository.GetByCreatorAndName<IAlbum>(artist.Location, albumTitle);
                if (album != null)
                    return album;
            }

            var builder = new MediaItemBuilder<IAlbum>(securityContext, mediaFactory)
                .Identity(albumTitle, summary)
                .Creator(artist.Location, artist.Name);

            return builder.ToMediaItem();
        }

        public override ITrack GetTrack(ISecurityContext securityContext, IMediaFactory mediaFactory, IMediaItemRepository mediaItemRepository, IAudioStreamFactory audioStreamFactory, IArtist artist, IAlbum album)
        {
            var track = mediaItemRepository.GetByTarget<ITrack>(Location).FirstOrDefault();
            //if (track != null)
                //return track;

            if (id3v2Tag == null)
            {
                if (track != null)
                    return track;

                var builder = new MediaItemBuilder<ITrack>(securityContext, mediaFactory)
                    .Identity("Unknown", string.Empty)
                    .Creator(artist.Location, artist.Name)
                    .Catalog(album.Location, album.Name)
                    .Target(Location, Type.Name);

                return builder.ToMediaItem();
            }

            var name = id3v2Tag.Title != null ? id3v2Tag.Title : "Unknown Track";
            var summary = id3v2Tag.Lyrics ?? string.Empty;
            
            var recordDate = DateTime.MinValue;
            if (id3v2Tag != null && id3v2Tag.RecordingDate > DateTime.MinValue)
                recordDate = id3v2Tag.RecordingDate;
            else if (id3v1Tag != null && id3v1Tag.Year >= DateTime.MinValue.Year && id3v1Tag.Year <= DateTime.MaxValue.Year)
                recordDate = new DateTime((int)id3v1Tag.Year, 1, 1);

            var releaseDate = DateTime.MinValue;
            if (id3v2Tag != null && id3v2Tag.ReleaseDate > DateTime.MinValue)
                releaseDate = id3v2Tag.ReleaseDate;
            else if (id3v1Tag != null && id3v1Tag.Year >= DateTime.MinValue.Year && id3v1Tag.Year <= DateTime.MaxValue.Year)
                releaseDate = new DateTime((int)id3v1Tag.Year, 1, 1);

            var number = id3v2Tag.Track;
            var duration = id3v2Tag.Duration;
            if (duration == TimeSpan.Zero)
            {
                using (var audioStream = audioStreamFactory.CreateAudioStream(Location))
                {
                    if (audioStream != null)
                    {
                        duration = audioStream.Duration;
                    }
                }
            }

            var thumbnail = Guid.Empty.ToUrn();
            var thumbnailData = id3v2Tag.Pictures != null && id3v2Tag.Pictures.Length > 0 ? id3v2Tag.Pictures[0].Data.ToArray() : new byte[0];

            var trackId = track != null ? track.Location : Guid.NewGuid().ToUrn();

            var fullBuilder = new MediaItemBuilder<ITrack>(securityContext, mediaFactory)
                .Identity(name, summary, recordDate, releaseDate, number, trackId)
                .Size(duration)
                .Creator(artist.Location, artist.Name)
                .Catalog(album.Location, album.Name)
                .Target(Location, Type.Name)
                .Thumbnail(thumbnail, thumbnailData);

            return fullBuilder.ToMediaItem();
        }
    }
}
