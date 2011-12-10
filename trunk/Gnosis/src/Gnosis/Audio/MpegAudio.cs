﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Algorithms;
using Gnosis.Application.Vendor;
using Gnosis.Tags;

using TagLib;

namespace Gnosis.Audio
{
    public class MpegAudio
        : AudioBase
    {
        private TagLib.File file;
        private TagLib.Id3v1.Tag id3v1Tag;
        private TagLib.Id3v2.Tag id3v2Tag;

        public MpegAudio(Uri location)
            : base(location, MediaType.AudioMpeg)
        {
        }

        private Uri GetJpgToUrl(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            var escaped = name.RemoveNonAlphaNumerics().Replace(" ", "_");

            try
            {
                return new Uri(string.Format("http://{0}.jpg.to", escaped), UriKind.Absolute);
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                return Guid.Empty.ToUrn();
            }
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

        public override IArtist GetArtist(ISecurityContext securityContext, IMediaItemRepository<ITrack> trackRepository, IMediaItemRepository<IArtist> artistRepository)
        {
            IArtist artist = null;
            var track = trackRepository.GetByTarget(Location).FirstOrDefault();
            if (track != null)
            {
                artist = artistRepository.GetByLocation(track.Creator);
                if (artist != null)
                    return artist;
            }
            
            if (id3v2Tag == null || id3v2Tag.JoinedPerformers == null)
                return GnosisArtist.Unknown;

            var artistName = id3v2Tag.JoinedPerformers;
            artist = artistRepository.GetByName(artistName).FirstOrDefault();
            if (artist != null)
                return artist;

            var thumbnail = GetJpgToUrl(artistName); 
            return new GnosisArtist(artistName, DateTime.MinValue, DateTime.MaxValue, Guid.Empty.ToUrn(), "Unknown Artist", Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, securityContext.CurrentUser.Location, securityContext.CurrentUser.Name, thumbnail, new byte[0]);
        }

        public override IAlbum GetAlbum(ISecurityContext securityContext, IMediaItemRepository<ITrack> trackRepository, IMediaItemRepository<IAlbum> albumRepository, IArtist artist)
        {
            IAlbum album = null;
            var track = trackRepository.GetByTarget(Location).FirstOrDefault();
            if (track != null)
            {
                album = albumRepository.GetByLocation(track.Catalog);
                if (album != null)
                    return album;
            }

            var albumTitle = "Unknown Album";
            //var albumTag = GetTags().Where(x => x.Type == Id3v2TagType.Album).FirstOrDefault();
            if (id3v2Tag != null && id3v2Tag.Album != null)
            {
                albumTitle = id3v2Tag.Album; //albumTag.Tuple.ToString();
                album = albumRepository.GetByCreatorAndName(artist.Location, albumTitle);
                if (album != null)
                    return album;
            }

            var thumbnail = GetJpgToUrl(albumTitle);
            return new GnosisAlbum(albumTitle, DateTime.MinValue, 0, artist.Location, artist.Name, Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, securityContext.CurrentUser.Location, securityContext.CurrentUser.Name, thumbnail, new byte[0]);
        }

        public override ITrack GetTrack(ISecurityContext securityContext, IMediaItemRepository<ITrack> trackRepository, IAudioStreamFactory audioStreamFactory, IArtist artist, IAlbum album)
        {
            var track = trackRepository.GetByTarget(Location).FirstOrDefault();
            //if (track != null)
                //return track;

            if (id3v2Tag == null)
            {
                return track != null ? track : new GnosisTrack("Unknown Track", DateTime.MinValue, DateTime.MaxValue, 0, TimeSpan.Zero, artist.Location, artist.Name, album.Location, album.Name, Location, Type, securityContext.CurrentUser.Location, securityContext.CurrentUser.Name, Guid.Empty.ToUrn(), new byte[0]);
            }

            var name = id3v2Tag.Title != null ? id3v2Tag.Title : "Unknown Track";
            var recordDate = id3v2Tag.RecordingDate > DateTime.MinValue ? id3v2Tag.RecordingDate : new DateTime((int)id3v1Tag.Year, 1, 1);
            var releaseDate = id3v2Tag.ReleaseDate > DateTime.MinValue ? id3v2Tag.ReleaseDate : new DateTime((int)id3v1Tag.Year, 1, 1);
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
            return new GnosisTrack(name, recordDate, releaseDate, number, duration, artist.Location, artist.Name, album.Location, album.Name, Location, Type, securityContext.CurrentUser.Location, securityContext.CurrentUser.Name, thumbnail, thumbnailData, trackId);
        }
    }
}
