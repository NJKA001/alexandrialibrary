using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class Track
        : ITrack
    {
        public Track(string title, uint number, TimeSpan duration, Uri artist, string artistName, Uri album, string albumTitle, Uri audioLocation, IMediaType audioType, Uri thumbnail)
            : this(title, number, duration, artist, artistName, album, albumTitle, audioLocation, audioType, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public Track(string title, uint number, TimeSpan duration, Uri artist, string artistName, Uri album, string albumTitle, Uri audioLocation, IMediaType audioType, Uri thumbnail, Uri location)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            if (artist == null)
                throw new ArgumentNullException("artist");
            if (artistName == null)
                throw new ArgumentNullException("artistName");
            if (album == null)
                throw new ArgumentNullException("album");
            if (albumTitle == null)
                throw new ArgumentNullException("albumTitle");
            if (audioLocation == null)
                throw new ArgumentNullException("audioLocation");
            if (audioType == null)
                throw new ArgumentNullException("audioType");
            if (location == null)
                throw new ArgumentNullException("location");

            this.title = title;
            this.number = number;
            this.duration = duration;
            this.artist = artist;
            this.artistName = artistName;
            this.album = album;
            this.albumTitle = albumTitle;
            this.audioLocation = audioLocation;
            this.audioType = audioType;
            this.thumbnail = thumbnail;
            this.location = location;
        }

        private readonly Uri location;
        private readonly string title;
        private readonly uint number;
        private readonly TimeSpan duration;
        private readonly Uri artist;
        private readonly string artistName;
        private readonly Uri album;
        private readonly string albumTitle;
        private readonly Uri audioLocation;
        private readonly IMediaType audioType;
        private readonly Uri thumbnail;

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return MediaType.ApplicationGnosisTrack; }
        }

        public string Title
        {
            get { return title; }
        }

        public uint Number
        {
            get { return number; }
        }

        public TimeSpan Duration
        {
            get { return duration; }
        }

        public Uri Artist
        {
            get { return artist; }
        }

        public string ArtistName
        {
            get { return artistName; }
        }

        public Uri Album
        {
            get { return album; }
        }

        public string AlbumTitle
        {
            get { return albumTitle; }
        }

        public Uri AudioLocation
        {
            get { return audioLocation; }
        }

        public IMediaType AudioType
        {
            get { return audioType; }
        }

        public Uri Thumbnail
        {
            get { return thumbnail; }
        }

        public void Load()
        {
        }

        public IEnumerable<ILink> GetLinks()
        {
            return Enumerable.Empty<ILink>();
        }

        public IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }
    }
}
