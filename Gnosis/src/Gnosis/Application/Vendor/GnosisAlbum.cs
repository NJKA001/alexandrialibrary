using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisAlbum
        : IAlbum
    {
        public GnosisAlbum(string title, DateTime created, Uri creator, string creatorName, Uri thumbnail)
            : this(title, created, creator, creatorName, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisAlbum(string title, DateTime created, Uri creator, string creatorName, Uri thumbnail, Uri location)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            if (creator == null)
                throw new ArgumentNullException("creator");
            if (creatorName == null)
                throw new ArgumentNullException("creatorName");
            if (location == null)
                throw new ArgumentNullException("location");

            this.title = title;
            this.created = created;
            this.creator = creator;
            this.creatorName = creatorName;
            this.thumbnail = thumbnail;
            this.location = location;
        }

        private readonly Uri location;
        private readonly string title;
        private readonly DateTime created;
        private readonly Uri creator;
        private readonly string creatorName;
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

        public DateTime Created
        {
            get { return created; }
        }

        public Uri Creator
        {
            get { return creator; }
        }

        public string CreatorName
        {
            get { return creatorName; }
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

        public static readonly IAlbum Unknown = new GnosisAlbum("Unknown", DateTime.MinValue, GnosisArtist.Unknown.Location, GnosisArtist.Unknown.Name, null, Guid.Empty.ToUrn());
    }
}
