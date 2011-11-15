using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class Album
        : IAlbum
    {
        public Album(string title, DateTime released, Uri artist, string artistName, Uri thumbnail)
            : this(title, released, artist, artistName, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public Album(string title, DateTime released, Uri artist, string artistName, Uri thumbnail, Uri location)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            if (artist == null)
                throw new ArgumentNullException("artist");
            if (artistName == null)
                throw new ArgumentNullException("artistName");
            if (location == null)
                throw new ArgumentNullException("location");

            this.title = title;
            this.released = released;
            this.artist = artist;
            this.artistName = artistName;
            this.thumbnail = thumbnail;
            this.location = location;
        }

        private readonly Uri location;
        private readonly string title;
        private readonly DateTime released;
        private readonly Uri artist;
        private readonly string artistName;
        private readonly Uri thumbnail;

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return MediaType.ApplicationGnosisAlbum; }
        }

        public string Title
        {
            get { return title; }
        }

        public DateTime Released
        {
            get { return released; }
        }

        public Uri Artist
        {
            get { return artist; }
        }

        public string ArtistName
        {
            get { return artistName; }
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
