using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Tags.Id3;
using Gnosis.Tags.Id3.Id3v1;
using Gnosis.Tags.Id3.Id3v2;

using TagLib;

namespace Gnosis.Audio
{
    public class MpegAudio
        : AudioBase, IMpegAudio
    {
        private TagLib.File file;
        private TagLib.Id3v1.Tag id3v1Tag;
        private TagLib.Id3v2.Tag id3v2Tag;

        public MpegAudio(Uri location)
            : base(location, MediaType.AudioMpeg)
        {
        }

        public override void Load()
        {
            if (Location.IsFile)
            {
                file = TagLib.File.Create(Location.LocalPath);
                id3v1Tag = file.GetTag(TagTypes.Id3v1) as TagLib.Id3v1.Tag;
                id3v2Tag = file.GetTag(TagTypes.Id3v2) as TagLib.Id3v2.Tag;

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
                    tags.Add(new Gnosis.Tags.Tag(Location, Id3v2TagType.Artist, id3v2Tag.Performers));
                    tags.Add(new Gnosis.Tags.Tag(Location, Gnosis.Tags.TagType.DefaultStringArray, id3v2Tag.Performers[0].Split(' ')));
                }
                if (id3v2Tag.Album != null)
                    tags.Add(new Gnosis.Tags.Tag(Location, Id3v2TagType.Album, id3v2Tag.Album));
                if (id3v2Tag.Year > 0)
                    tags.Add(new Gnosis.Tags.Tag(Location, Id3v2TagType.ReleaseTime, new DateTime((int)id3v2Tag.Year, 1, 1)));
                if (id3v2Tag.Track > 0)
                    tags.Add(new Gnosis.Tags.Tag(Location, Id3v2TagType.TrackNumber, new uint[] { id3v2Tag.Track, id3v2Tag.TrackCount }));
            }
            else if (id3v1Tag != null)
            {
            }

            return tags;
        }

        public IEnumerable<IId3Tag> GetId3Tags()
        {
            throw new NotImplementedException();
        }

        public IArtist GetArtist(IMediaItemRepository<IArtist> artistRepository)
        {
            var artistTag = GetTags().Where(x => x.Type == Id3v2TagType.Artist).FirstOrDefault();
            if (artistTag == null)
                return GnosisArtist.Unknown;

            var artistName = artistTag.Tuple.ToString();
            var artist = artistRepository.GetByName(artistName).FirstOrDefault();
            if (artist != null)
                return artist;

            return new GnosisArtist(artistName, DateTime.MinValue, DateTime.MaxValue, Guid.Empty.ToUrn(), "Unknown Artist", Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, SecurityContext.CurrentUser.Location, SecurityContext.CurrentUser.Name, Guid.Empty.ToUrn());
        }

        public IAlbum GetAlbum(IMediaItemRepository<IAlbum> albumRepository, IArtist artist)
        {
            var albumTitle = "Unknown Album";
            var albumTag = GetTags().Where(x => x.Type == Id3v2TagType.Album).FirstOrDefault();
            if (albumTag != null)
            {
                albumTitle = albumTag.Tuple.ToString();
                var album = albumRepository.GetByCreatorAndName(artist.Location, albumTitle);
                if (album != null)
                    return album;
            }

            return new GnosisAlbum(albumTitle, DateTime.MinValue, 0, artist.Location, artist.Name, Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, SecurityContext.CurrentUser.Location, SecurityContext.CurrentUser.Name, Guid.Empty.ToUrn());
        }

        public ITrack GetTrack(IMediaItemRepository<ITrack> trackRepository, IArtist artist, IAlbum album)
        {
            var track = trackRepository.GetByTarget(Location).FirstOrDefault();
            if (track != null)
                return track;

            var name = "Unknown Track";
            var recordDate = DateTime.MinValue;
            var releaseDate = DateTime.MaxValue;
            uint number = 0;
            var duration = TimeSpan.Zero;
            var thumbnail = Guid.Empty.ToUrn();

            var tags = GetTags();
            var titleTag = tags.Where(x => x.Type == Id3v2TagType.Title).FirstOrDefault();
            var recordTag = tags.Where(x => x.Type == Id3v2TagType.RecordingTime).FirstOrDefault();
            var releasedTag = tags.Where(x => x.Type == Id3v2TagType.ReleaseTime).FirstOrDefault();

            return new GnosisTrack(name, recordDate, releaseDate, number, duration, artist.Location, artist.Name, album.Location, album.Name, Location, Type, SecurityContext.CurrentUser.Location, SecurityContext.CurrentUser.Name, thumbnail);
        }

        public void SetTag(IId3Tag tag)
        {
            throw new NotImplementedException();
        }

        public void RemoveTag(IId3Tag tag)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
