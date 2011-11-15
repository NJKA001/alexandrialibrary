using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class Artist
        : IArtist
    {
        public Artist(string name, DateTime activeFrom, DateTime activeTo, Uri thumbnail)
            : this(name, activeFrom, activeTo, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public Artist(string name, DateTime activeFrom, DateTime activeTo, Uri thumbnail, Uri location)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (location == null)
                throw new ArgumentNullException("location");

            this.name = name;
            this.activeFrom = activeFrom;
            this.activeTo = activeTo;
            this.thumbnail = thumbnail;
            this.location = location;
        }

        private readonly Uri location;
        private readonly string name;
        private readonly DateTime activeFrom;
        private readonly DateTime activeTo;
        private readonly Uri thumbnail;

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return MediaType.ApplicationGnosisArtist; }
        }

        public string Name
        {
            get { return name; }
        }

        public DateTime ActiveFrom
        {
            get { return activeFrom; }
        }

        public DateTime ActiveTo
        {
            get { return activeTo; }
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
