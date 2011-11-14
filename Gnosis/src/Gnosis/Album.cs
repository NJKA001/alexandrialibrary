using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class Album
        : IAlbum
    {
        public Album(string title, DateTime released, Guid artist, string artistName, Uri thumbnail)
            : this(title, released, artist, artistName, thumbnail, Guid.NewGuid())
        {
        }

        public Album(string title, DateTime released, Guid artist, string artistName, Uri thumbnail, Guid id)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            if (artistName == null)
                throw new ArgumentNullException("artistName");

            this.title = title;
            this.released = released;
            this.artist = artist;
            this.artistName = artistName;
            this.thumbnail = thumbnail;
            this.id = id;
        }

        private readonly Guid id;
        private readonly string title;
        private readonly DateTime released;
        private readonly Guid artist;
        private readonly string artistName;
        private readonly Uri thumbnail;

        public Guid Id
        {
            get { return id; }
        }

        public string Title
        {
            get { return title; }
        }

        public DateTime Released
        {
            get { return released; }
        }

        public Guid Artist
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
    }
}
