using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class Track
        : ITrack
    {
        public Track(string title, uint number, TimeSpan duration, Guid artist, string artistName, Guid album, string albumTitle, Uri audioLocation, IMediaType audioType, Uri thumbnail)
            : this(title, number, duration, artist, artistName, album, albumTitle, audioLocation, audioType, thumbnail, Guid.NewGuid())
        {
        }

        public Track(string title, uint number, TimeSpan duration, Guid artist, string artistName, Guid album, string albumTitle, Uri audioLocation, IMediaType audioType, Uri thumbnail, Guid id)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            if (artistName == null)
                throw new ArgumentNullException("artistName");
            if (albumTitle == null)
                throw new ArgumentNullException("albumTitle");
            if (audioLocation == null)
                throw new ArgumentNullException("audioLocation");
            if (audioType == null)
                throw new ArgumentNullException("audioType");

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
            this.id = id;
        }

        private readonly Guid id;
        private readonly string title;
        private readonly uint number;
        private readonly TimeSpan duration;
        private readonly Guid artist;
        private readonly string artistName;
        private readonly Guid album;
        private readonly string albumTitle;
        private readonly Uri audioLocation;
        private readonly IMediaType audioType;
        private readonly Uri thumbnail;

        public Guid Id
        {
            get { return id; }
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

        public Guid Artist
        {
            get { return artist; }
        }

        public string ArtistName
        {
            get { return artistName; }
        }

        public Guid Album
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
    }
}
